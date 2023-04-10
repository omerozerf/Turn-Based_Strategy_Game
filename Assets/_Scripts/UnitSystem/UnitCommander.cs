using System;
using CommandSystem;
using EmreBeratKR.ServiceLocator;
using GridSystem;
using UnityEngine;

namespace UnitSystem
{
    public class UnitCommander : ServiceBehaviour
    {
        public static event Action<SelectedUnitChangedArgs> OnSelectedUnitChanged;
        public struct SelectedUnitChangedArgs
        {
            public Unit unit;
        }

        public static event Action<SelectedCommandChangedArgs> OnSelectedCommandChanged;
        public struct SelectedCommandChangedArgs
        {
            public BaseCommand oldCommand;
            public BaseCommand newCommand;
        }

        public static event Action<CommandExecutedArgs> OnCommandExecuted;
        public struct CommandExecutedArgs
        {
            public BaseCommand command;
            public CommandArgs args;
        }

        public static event Action<BusyChangedArgs> OnBusyChanged;
        public struct BusyChangedArgs
        {
            public bool isBusy;
        }

        public static event Action<FailedToExecuteCommandArgs> OnFailedToExecuteCommand;
        public struct FailedToExecuteCommandArgs
        {
            public CommandStatus status;
            public GridPosition gridPosition;
            public Vector3 worldPosition;
        }


        private BaseCommand m_SelectedCommand;
        private Unit m_SelectedUnit;
        private bool m_IsMyTurn;
        private bool m_IsBusy;


        private void Awake()
        {
            GameInput.OnLeftMouseButtonDown += GameInput_OnMouseLeftButtonDown;
            
            TurnManager.OnTurnChanged += TurnManager_OnTurnChanged;
            
            Unit.OnAnyUnitDead += OnAnyUnitDead;
        }

        private void OnDestroy()
        {
            GameInput.OnLeftMouseButtonDown -= GameInput_OnMouseLeftButtonDown;
            
            TurnManager.OnTurnChanged -= TurnManager_OnTurnChanged;
            
            Unit.OnAnyUnitDead -= OnAnyUnitDead;
        }


        private void GameInput_OnMouseLeftButtonDown()
        {
            if (m_IsBusy) return;
            
            if (!m_IsMyTurn) return;

            if (GameInput.IsMouseOverUI()) return;

            if (TrySelectUnit()) return;
        
            TryExecuteCommand(m_SelectedCommand);
        }
        
        private void TurnManager_OnTurnChanged(TurnManager.TurnChangedArgs args)
        {
            m_IsMyTurn = args.team == TeamType.Player;
        }
        
        private void OnAnyUnitDead(Unit unit)
        {
            if (unit == m_SelectedUnit)
            {
                ClearSelectedUnit();
            }
        }


        private bool TrySelectUnit()
        {
            var selection = GameInput.GetMouseSelection<Unit>();

            if (!selection) return false;

            if (selection == m_SelectedUnit) return false;

            if (!selection.IsInsideTeam(TeamType.Player)) return false;
        
            SelectUnit(selection);

            return true;
        }
    
        private void SelectUnit(Unit unit)
        {
            m_SelectedUnit = unit;
            OnSelectedUnitChanged?.Invoke(new SelectedUnitChangedArgs
            {
                unit = unit
            });
            
            if (!unit) return;
            
            SetSelectedCommand(unit.GetDefaultCommand());
        }

        private void ClearSelectedUnit()
        {
            SelectUnit(null);
            SetSelectedCommand(null);
        }

        private void SetBusy(bool value)
        {
            m_IsBusy = value;
            OnBusyChanged?.Invoke(new BusyChangedArgs
            {
                isBusy = value
            });
        }
        
        private bool TryExecuteCommand(BaseCommand command)
        {
            var mousePosition = GameInput.GetMouseWorldPosition();
        
            if (!mousePosition.HasValue) return false;
            
            var mouseGridPosition = LevelGrid.GetGridPosition(mousePosition.Value);

            if (!command)
            {
                OnFailedToExecuteCommand?.Invoke(new FailedToExecuteCommandArgs
                {
                    status = CommandStatus.NotSelected,
                    gridPosition = mouseGridPosition,
                    worldPosition = LevelGrid.GetWorldPosition(mouseGridPosition)
                });
                return false;
            }
            
            var isValidGridPosition = command
                .IsValidGridPosition(mouseGridPosition);

            if (!isValidGridPosition)
            {
                var commandStatus = command.GetStatusByGridPosition(mouseGridPosition);
                OnFailedToExecuteCommand?.Invoke(new FailedToExecuteCommandArgs
                {
                    status = commandStatus,
                    gridPosition = mouseGridPosition,
                    worldPosition = LevelGrid.GetWorldPosition(mouseGridPosition)
                });
                return false;
            }
            
            if (!command.Unit.TryUseCommandPoint(command)) return false;
            
            ExecuteCommand(command, new CommandArgs
            {
                gridPosition = mouseGridPosition,
                gridObject = LevelGrid.GetGridObject(mouseGridPosition),
                unit = LevelGrid.GetUnitAtGridPosition(mouseGridPosition),
                interactable = LevelGrid.GetInteractableAtGridPosition(mouseGridPosition)
            });
            
            return true;
        }

        private void ExecuteCommand(BaseCommand command, CommandArgs args)
        {
            SetBusy(true);
            
            command.Execute(args, () => SetBusy(false));
            OnCommandExecuted?.Invoke(new CommandExecutedArgs
            {
                command = command,
                args = args
            });
        }
        
        
        public static void SetSelectedCommand(BaseCommand command)
        {
            var instance = GetInstance();
            
            if (command == instance.m_SelectedCommand) return;

            var oldCommand = instance.m_SelectedCommand;
            instance.m_SelectedCommand = command;
            OnSelectedCommandChanged?.Invoke(new SelectedCommandChangedArgs
            {
                oldCommand = oldCommand,
                newCommand = command
            });
        }

        public static Unit GetSelectedUnit()
        {
            return GetInstance().m_SelectedUnit;
        }

        public static BaseCommand GetSelectedCommand()
        {
            return GetInstance().m_SelectedCommand;
        }
        
        public static bool IsBusy()
        {
            return GetInstance().m_IsBusy;
        }


        private static UnitCommander GetInstance()
        {
            return ServiceLocator.Get<UnitCommander>();
        }
    }
}