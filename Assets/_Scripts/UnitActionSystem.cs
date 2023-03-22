using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts;
using _Scripts.test;
using UnityEngine;

public class UnitActionSystem : MonoBehaviour
{
    [SerializeField] private Unit selectedUnit;
    [SerializeField] private LayerMask unitsLayerMask;
    


    private void Update()
    {
        if (TryHandleUnitSelection()) return;
        
            
        if (Input.GetMouseButtonDown(0))
        {
            selectedUnit.Move(MouseWorld.GetPosition());
        }
    }


    private bool TryHandleUnitSelection()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, unitsLayerMask))
        {
            if (raycastHit.transform.TryGetComponent<Unit>(out Unit unit))
            {
                selectedUnit = unit;
                return true;
            }
        }
        return false;
    }
}
