using System;
using UnityEngine;

namespace _Scripts
{
    public class MouseWorld : MonoBehaviour
    {
        private static MouseWorld Instance;
        
        
        [SerializeField] private LayerMask mousePlaneLayerMask;


        private void Awake()
        {
            Instance = this;
        }


        private void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
            Debug.Log(Physics.Raycast(ray,out RaycastHit raycastHit, float.MaxValue, mousePlaneLayerMask));
            transform.position = raycastHit.point;
        }
        
        
        public static Vector3 GetPosition()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); 
            Physics.Raycast(ray,out RaycastHit raycastHit, float.MaxValue, Instance.mousePlaneLayerMask);
            return raycastHit.point;
        }
    }
}
