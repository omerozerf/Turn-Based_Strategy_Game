using System;
using _Scripts.Grid;
using UnityEngine;

namespace _Scripts.Unit
{
    public class Unit : MonoBehaviour
    {
        public static event EventHandler OnAnyActionPointsChanged;


        [SerializeField] private bool isEnemy;
        
        
        
        private const int ACTION_POINTS_MAX = 2;
        
        
        private GridPosition gridPosition;
        private MoveAction moveAction;
        private SpinAction spinAction;
        private BaseAction[] baseActionArray;
        private int actionPoints = ACTION_POINTS_MAX;


        private void Awake()
        {
            moveAction = GetComponent<MoveAction>();
            spinAction = GetComponent<SpinAction>();
            baseActionArray = GetComponents<BaseAction>();
        }


        private void Start()
        {
            gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
            LevelGrid.Instance.AddUnitAtGridPosition(gridPosition, this);
            
            TurnSystem.Instance.OnTurnChanged += TurnSystem_OnTurnChanged;
        }

        
        private void TurnSystem_OnTurnChanged(object sender, EventArgs e)
        {
            if (( IsEnemy() && !TurnSystem.Instance.IsPlayerTurn()) ||
                (!IsEnemy() && TurnSystem.Instance.IsPlayerTurn()))
            {
                actionPoints = ACTION_POINTS_MAX;
            
                OnAnyActionPointsChanged?.Invoke(this, EventArgs.Empty);
            }
        }


        private void Update()
        {
            GridPosition newgridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
            if (newgridPosition != gridPosition)
            {
                // Unit changed Grid Position
                
                LevelGrid.Instance.UnitMovedGridPosition(this, gridPosition, newgridPosition);
                gridPosition = newgridPosition;
            }
        }


        public MoveAction GetMoveAction()
        {
            return moveAction;
        }
        
        
        public SpinAction GetSpinAction()
        {
            return spinAction;
        }


        public GridPosition GetGridPosition()
        {
            return gridPosition;
        }


        public Vector3 GetWorldPosition()
        {
            return transform.position;
        }


        public BaseAction[] GetBaseActionArray()
        {
            return baseActionArray;
        }


        public bool TrySpendActionPointsToTakeAction(BaseAction baseAction)
        {
            if (CanSpendActionPointsToTakeAction(baseAction))
            {
                SpendActionPonits(baseAction.GetActionPointsCost());
                return true;
            }
            else return false;
        }


        public bool CanSpendActionPointsToTakeAction(BaseAction baseAction)
        {
            if (actionPoints >= baseAction.GetActionPointsCost())
            {
                return true;
            }
            else return false;
        }


        private void SpendActionPonits(int amount)
        {
            actionPoints -= amount;
            
            OnAnyActionPointsChanged?.Invoke(this, EventArgs.Empty);
        }


        public int GetActionPoints()
        {
            return actionPoints;
        }


        public bool IsEnemy()
        {
            return isEnemy;
        }

        public void Damage()
        {
            Debug.Log(transform + "damaged!");
        }
    }
}
