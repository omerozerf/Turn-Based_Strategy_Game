using System.Collections.Generic;
using _Scripts.Grid;
using UnityEngine;

namespace _Scripts
{
    public class MoveAction : MonoBehaviour
    {
        [SerializeField] private Animator unitAnimator;
        [SerializeField] private int maxMoveDistance = 4;

        private Unit.Unit unit;
        private Vector3 targetPosition;
    
    
        private void Awake()
        {
            unit = GetComponent<Unit.Unit>();
            targetPosition = this.transform.position;
        }

    
        private void Update()
        {
            float stoppingDistance = .1f;
            if (Vector3.Distance(transform.position, targetPosition) > stoppingDistance)
            {
                Vector3 moveDirection = (targetPosition - transform.position).normalized;
                float moveSpeed = 4f;
                transform.position += moveDirection * (Time.deltaTime * moveSpeed);

                float rotateSpeed = 10f;
                transform.forward = Vector3.Lerp(transform.forward, moveDirection, rotateSpeed * Time.deltaTime);
                
                unitAnimator.SetBool("IsWalking", true);
            }
            else
            {
                unitAnimator.SetBool("IsWalking", false);

            }
        }


        public void Move(GridPosition gridPosition)
        {
            this.targetPosition = LevelGrid.Instance.GetWorldPosition(gridPosition);
        }

        
        public bool IsValidActionGridPosition(GridPosition gridPosition)
        {
            List<GridPosition> validGridPositionList = GetValidActionGridPositionList();
            return validGridPositionList.Contains(gridPosition);
        }
        
        
        public List<GridPosition> GetValidActionGridPositionList()
        {
            List<GridPosition> validGridPositionList = new List<GridPosition>();

            GridPosition unitGridPosition = unit.GetGridPosition();

            for (int x = -maxMoveDistance; x <= maxMoveDistance; x++)
            {
                for (int z = -maxMoveDistance; z <= maxMoveDistance; z++)
                {
                    GridPosition offsetGridPosition = new GridPosition(x, z);
                    GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                    if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition))
                    {
                        continue;
                    }

                    if (unitGridPosition == testGridPosition)
                    {
                        // Same Grid Position where the unit is already at
                        continue;
                    }

                    if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition))  
                    {
                        // Grid position already occupied with another unit
                        continue;
                    }
                    
                    
                    validGridPositionList.Add(testGridPosition);
                }
            }
            
            return validGridPositionList;
        }
    }
}
