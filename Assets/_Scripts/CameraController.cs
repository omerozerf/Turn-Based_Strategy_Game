using System;
using Cinemachine;
using UnityEngine;

namespace _Scripts
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;

        private const float MIN_FOLLOW_Y_OFFSET = 2f;
        private const float MAX_FOLLOW_Y_OFFSET = 12f;

        private Vector3 targetFolloOffset;
        private CinemachineTransposer cinemachineTransposer;


        private void Start()
        {
            cinemachineTransposer = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineTransposer>();
            targetFolloOffset = cinemachineTransposer.m_FollowOffset;
        }


        private void Update()
        {
            HandleMovement();
            HandleRotation();
            HandleZoom();
        }


        private void HandleMovement()
        {
            Vector3 inputMoveDir = new Vector3(0, 0, 0);
            if (Input.GetKey(KeyCode.W))
            {
                inputMoveDir.z = +1f;
            }
            if (Input.GetKey(KeyCode.S))
            {
                inputMoveDir.z = -1f;
            }
            if (Input.GetKey(KeyCode.A))
            {
                inputMoveDir.x = -1f;
            }
            if (Input.GetKey(KeyCode.D))
            {
                inputMoveDir.x = +1f;
            }

            float moveSpeed = 10f;
            
            Vector3 moveVector = transform.forward * inputMoveDir.z + transform.right * inputMoveDir.x;
            transform.position += moveVector * (moveSpeed * Time.deltaTime);
        }


        private void HandleRotation()
        {
            Vector3 rotationVector = new Vector3(0, 0, 0);
            if (Input.GetKey(KeyCode.Q))
            {
                rotationVector.y = -1f;
            }
            if (Input.GetKey(KeyCode.E))
            {
                rotationVector.y = +1f;
            }

            float rotationSpeed = 100f;

            transform.eulerAngles += rotationVector * (rotationSpeed * Time.deltaTime);
        }


        private void HandleZoom()
        {
            float zoomAmount = 1f;
            
            if (Input.mouseScrollDelta.y > 0)
            {
                targetFolloOffset.y -= zoomAmount;
            }
            if (Input.mouseScrollDelta.y < 0)
            {
                targetFolloOffset.y += zoomAmount;
            }

            targetFolloOffset.y = Mathf.Clamp(targetFolloOffset.y, MIN_FOLLOW_Y_OFFSET, MAX_FOLLOW_Y_OFFSET);
            float zoomSpeed = 5f;
            cinemachineTransposer.m_FollowOffset =
                Vector3.Lerp(cinemachineTransposer.m_FollowOffset, targetFolloOffset, Time.deltaTime * zoomSpeed);
        }
    }
}
