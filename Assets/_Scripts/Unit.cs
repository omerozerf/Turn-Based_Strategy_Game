using System;
using UnityEngine;

namespace _Scripts.test
{
    public class Unit : MonoBehaviour
    {
        private Vector3 targetPosition;


        private void Update()
        {
            float stoppingDistance = .1f;
            if (Vector3.Distance(transform.position, targetPosition) > stoppingDistance)
            {
                Vector3 moveDirection = (targetPosition - transform.position).normalized;
                float moveSpeed = 4f;
                transform.position += moveDirection * (Time.deltaTime * moveSpeed);
            }


            if (Input.GetKeyDown(KeyCode.T))
            {
                Move(new Vector3(4, 0, 4));
            }
        }


        private void Move(Vector3 targetPosition)
        {
            this.targetPosition = targetPosition;
        }
    }
}
