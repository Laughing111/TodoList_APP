using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayOutManager : MonoBehaviour
{
    private void Awake()
    {
        Init();
    }

    public void Init()
    {
        ItemManager.Ins.FinishCloneAndGoToAdd += AddChildItem;
    }

    public enum LayOutType
    {
        Horizontal,
        Vertical
    }

    public LayOutType SetLayoutType;

    public int childCount=0;

    public void AddChildItem(Transform childItem)
    {
        childItem.SetParent(transform, false);
        childCount++;
        SetLayOut(childItem,SetLayoutType);
    }

    private void SetLayOut(Transform childItem,LayOutType layOutType)
    {
        if (layOutType == LayOutType.Vertical)
        {
            childItem.transform.SetLocalPos(y: childItem.transform.localPosition.y+(-220) * (childCount - 1));
        }
        else
        {
            
        }
        
    }
}