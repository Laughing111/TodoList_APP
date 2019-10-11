using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private GameObject btnConfirm;
    private GameObject btnCancle;
    private InputField inputField;
    private void Awake()
    {
        canvasGroup = transform.GetComponent<CanvasGroup>();
        inputField = transform.Find("BG/InputField").GetComponent<InputField>();
        btnConfirm = transform.Find("BG/btn/btnConfirm").gameObject;
        btnCancle = transform.Find("BG/btn/btnCancle").gameObject;
        Init();
    }
    private void Init()
    {
        canvasGroup.alpha = 0;
        btnConfirm.RegisterEventTrigger().AddClickEvent(ConfirmInput);
        btnCancle.RegisterEventTrigger().AddClickEvent(cancleInput);
    }

    private void cancleInput(PointerEventData eventData)
    {
        inputField.text = "";
        OpenOrCloseCanvas(false);
    }

    private void ConfirmInput(PointerEventData eventData)
    {
        if (!string.IsNullOrEmpty(inputField.text))
        {
            //去创建一个Item
            CanvasManager.Ins.ItemManager.CreateItem(inputField.text);
            inputField.text = "";
            OpenOrCloseCanvas(false);
        }
    }
    public void OpenOrCloseCanvas(bool isOpen)
    {
        if (isOpen)
        {
            canvasGroup.alpha = 1;
        }
        else
        {
            canvasGroup.alpha = 0;
        }
    }
}
