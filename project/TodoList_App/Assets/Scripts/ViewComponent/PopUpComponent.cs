using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpComponent : MonoBehaviour
{

    public PopUpType popUptype;
    private CanvasGroup canvasGroup;
    public void Awake()
    {
        canvasGroup = transform.GetComponent<CanvasGroup>();
        Init();
    }

    public virtual void  Init()
    {
        ClosePopUp();
        PopUpManager.Ins.RegisterPopUp(this, popUptype);
    }

    public void ShowPopUp()
    {
        canvasGroup.alpha = 1;
    }

    public void ClosePopUp()
    {
        canvasGroup.alpha = 0;
    }
}
