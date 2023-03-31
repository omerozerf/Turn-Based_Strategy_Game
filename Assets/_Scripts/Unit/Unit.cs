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


        private void Awake()
        {
            moveAction = GetComponent<MoveAction>();
            spinAction = GetComponent<SpinAction>();
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
    }
}
