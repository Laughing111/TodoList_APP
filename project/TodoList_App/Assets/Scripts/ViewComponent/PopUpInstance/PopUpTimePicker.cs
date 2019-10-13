using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(CanvasGroup))]
public class PopUpTimePicker : PopUpComponent
{
    private GameObject btnConfirm;
    public override void Init()
    {
        base.Init();
        btnConfirm = transform.Find("btnConfirm").gameObject;
        btnConfirm.RegisterEventTrigger().AddClickEvent(ConfirmTime);
    }

    private void ConfirmTime(PointerEventData eventData)
    {
        CanvasManager.Ins.InputManager.SetTime("10:00");
        PopUpManager.Ins.DisActivePopUpCanvas().HidePopUp(popUptype);
    }
}
