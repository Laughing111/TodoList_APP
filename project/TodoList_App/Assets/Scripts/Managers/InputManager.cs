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
    private GameObject btnSetDeadLine;
    private GameObject btnCancle;
    private InputField inputField;
    private Text deadLineTime;
    private void Awake()
    {
        canvasGroup = transform.GetComponent<CanvasGroup>();
        inputField = transform.Find("BG/InputField").GetComponent<InputField>();
        btnConfirm = transform.Find("BG/btn/btnConfirm").gameObject;
        btnCancle = transform.Find("BG/btn/btnCancle").gameObject;
        btnSetDeadLine = transform.Find("BG/btn/btnDeadLine").gameObject;
        deadLineTime = transform.Find("BG/DeadLineTime").GetComponent<Text>();
        Init();
    }
    private void Init()
    {
        canvasGroup.alpha = 0;
        btnConfirm.RegisterEventTrigger().AddClickEvent(ConfirmInput);
        btnCancle.RegisterEventTrigger().AddClickEvent(cancleInput);
        btnSetDeadLine.RegisterEventTrigger().AddClickEvent(SetDeadLine);
    }

    private void SetDeadLine(PointerEventData eventData)
    {
        PopUpManager.Ins.ActivePopUpCanvas().ShowPopUp(PopUpType.TimePicker);
    }

    public void SetTime(string timeText)
    {
        deadLineTime.text = timeText;
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
            CanvasManager.Ins.ItemManager.CreateItem(inputField.text,deadLineTime.text);
            inputField.text = "";
            deadLineTime.text = "";
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
