using System;
using _Scripts.Grid;
using UnityEngine;

namespace _Scripts.Unit
{
    public class Unit : MonoBehaviour
    {
        [SerializeField] private Animator unitAnimator;
        
        private Vector3 targetPosition;
        private GridPosition gridPosition;


        private void Awake()
        {
            targetPosition = this.transform.position;
        }


        private void Start()
        {
            gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
            LevelGrid.Instance.SetUnitAtGridPosition(gridPosition, this);
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
            
            
            GridPosition newgridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
            if (newgridPosition != gridPosition)
            {
                // Unit changed Grid Position
                
                LevelGrid.Instance.UnitMovedGridPosition(this, gridPosition, newgridPosition);
                gridPosition = newgridPosition;
            }
        }


        public void Move(Vector3 targetPosition)
        {
            this.targetPosition = targetPosition;
        }
    }
}
