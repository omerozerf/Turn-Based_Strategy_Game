using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts;
using _Scripts.Grid;
using _Scripts.Unit;
using UnityEngine;

public class Testing : MonoBehaviour
{
    [SerializeField] private Unit unit;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            GridSystemVisual.Instance.HideAllGridPosition();
            GridSystemVisual.Instance.ShowGridPositionList(
                unit.GetMoveAction().GetValidActionGridPositionList());
        }
    }
}
