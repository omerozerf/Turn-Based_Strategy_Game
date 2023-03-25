using System;
using UnityEngine;

namespace _Scripts.Unit
{
    public class UnitSelectedVisual : MonoBehaviour
    {
        [SerializeField] private _Scripts.Unit.Unit unit;

        private MeshRenderer meshRenderer;


        private void Awake()
        {
            meshRenderer = GetComponent<MeshRenderer>();
        }


        private void Start()
        {
            UnitActionSystem.Instance.OnSelectedUnitChanged += UnitActionSystem_OnSelectedUnitChanged;
            
            UpdateVisual();
        }
        

        private void UnitActionSystem_OnSelectedUnitChanged(object sender, EventArgs e)
        {
            UpdateVisual();
        }


        private void UpdateVisual()
        {
            if (UnitActionSystem.Instance.GetSelectedUnit() == unit)
            {
                meshRenderer.enabled = true;
            }
            else
            {
                meshRenderer.enabled = false;
            }
        }
    }
}
