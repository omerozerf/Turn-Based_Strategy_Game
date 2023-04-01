using System.Collections;
using System.Collections.Generic;
using _Scripts.Unit;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionButtonUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private Button button;


    public void SetBaseAction(BaseAction baseAction)
    {
        textMeshPro.text = baseAction.GetActionName().ToUpper();
        
        button.onClick.AddListener((() => 
                UnitActionSystem.Instance.SetSelectedAction(baseAction)
            ));
    }
}