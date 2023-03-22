using System;
using UnityEngine;

namespace _Scripts.test
{
    public class Unit : MonoBehaviour
    {
        [SerializeField] private Animator unitAnimator;
        
        private Vector3 targetPosition;


        private void Awake()
        {
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


        public void Move(Vector3 targetPosition)
        {
            this.targetPosition = targetPosition;
        }
    }
}
