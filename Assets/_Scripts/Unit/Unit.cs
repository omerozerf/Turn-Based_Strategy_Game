using System;
using _Scripts.Grid;
using UnityEngine;

namespace _Scripts.Unit
{
    public class Unit : MonoBehaviour
    {
        private GridPosition gridPosition;
        private MoveAction moveAction;
        private SpinAction spinAction;
        private BaseAction[] baseActionArray;
        private int actionPoints = 2;


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
        }


        public int GetActionPoints()
        {
            return actionPoints;
        }
    }
}
