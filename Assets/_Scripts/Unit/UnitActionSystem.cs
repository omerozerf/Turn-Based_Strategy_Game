using System;
using _Scripts.Grid;
using UnityEngine;

namespace _Scripts.Unit
{
    public class UnitActionSystem : MonoBehaviour
    {
        public static UnitActionSystem Instance { get; private set; }


        public event EventHandler OnSelectedUnitChanged;
    
    
        [SerializeField] private Unit selectedUnit;
        [SerializeField] private LayerMask unitsLayerMask;


    
        private void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("There is more than UnitActionSystem!" + transform + " - " + Instance);
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }
    


        private void Update()
        { 
            if (Input.GetMouseButtonDown(0))
            {
                if (TryHandleUnitSelection()) return;

                GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetPosition());

                if (selectedUnit.GetMoveAction().IsValidActionGridPosition(mouseGridPosition))
                {
                    selectedUnit.GetMoveAction().Move(mouseGridPosition);
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                selectedUnit.GetSpinAction().Spin();
            }
        }


        private bool TryHandleUnitSelection()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, unitsLayerMask))
            {
                if (raycastHit.transform.TryGetComponent<Unit>(out Unit unit))
                {
                    SetSelectedUnit(unit);
                    return true;
                }
            }
            return false;
        }


        private void SetSelectedUnit(Unit unit)
        {
            selectedUnit = unit;
            OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);
        }


        public Unit GetSelectedUnit()
        {
            return selectedUnit;
        }
    }
}
