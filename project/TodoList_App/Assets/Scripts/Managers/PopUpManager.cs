using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public enum PopUpType
{
    TimePicker
}

[RequireComponent(typeof(CanvasGroup))]
public class PopUpManager:MonoBehaviour
{

    private PopUpManager() { }
    private static PopUpManager ins;
    public static PopUpManager Ins
    {
        get
        {
            ins = ins ?? Object.FindObjectOfType<PopUpManager>();
            return ins;
        }
    }

    private void Awake()
    {
        canvasGroup = transform.GetComponent<CanvasGroup>();
        DisActivePopUpCanvas();
    }

    private CanvasGroup canvasGroup;

    public Dictionary<PopUpType, PopUpComponent> popUpPool=new Dictionary<PopUpType, PopUpComponent>();

    public void RegisterPopUp(PopUpComponent popUpComponent,PopUpType popUpType)
    {
        if (!popUpPool.ContainsKey(popUpType))
        {
            popUpPool.Add(popUpType, popUpComponent);
        }
        else
        {
            Debug.LogErrorFormat("弹出框类型重复：{0}", popUpType);
        }
    }
    public void ShowPopUp(PopUpType popUpType)
    {
        if (!popUpPool.ContainsKey(popUpType))
        {
            Debug.LogErrorFormat("找不到指定的弹出框：{0}", popUpType.ToString());
        }
        else
        {
            popUpPool[popUpType].ShowPopUp();
        }
    }

    public PopUpManager ActivePopUpCanvas()
    {
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
        return Ins;
    }

    public PopUpManager DisActivePopUpCanvas()
    {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        return Ins;
    }

    public void HidePopUp(PopUpType popUpType)
    {
        if (!popUpPool.ContainsKey(popUpType))
        {
            Debug.LogErrorFormat("找不到指定的弹出框：{0}", popUpType.ToString());
        }
        else
        {
            popUpPool[popUpType].ClosePopUp();
        }
    }
}
