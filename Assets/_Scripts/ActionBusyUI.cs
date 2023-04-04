using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Unit;
using UnityEngine;

public class ActionBusyUI : MonoBehaviour
{
    private void Start()
    {
        UnitActionSystem.Instance.OnBusyChanged += UnitActionSystem_OnOnBusyChanged;
        Hide();
    }

    private void UnitActionSystem_OnOnBusyChanged(object sender, bool isBusy)
    {
        if (isBusy) Show();
        
        else Hide();
    }


    private void Show()
    {
        gameObject.SetActive(true);
    }


    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
