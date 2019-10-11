using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayOutManager : MonoBehaviour
{
    private UIScrollView uIScrollView;
    private void Awake()
    {
        Init();
    }
    public void Init()
    {
        uIScrollView = transform.GetComponent<UIScrollView>();
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
        Debug.Log("Add,And now have :"+childCount);
        SetLayOut(childItem,SetLayoutType);
    }

    private void SetLayOut(Transform childItem,LayOutType layOutType)
    {
        Rect itemRect = childItem.GetComponent<RectTransform>().rect;
        if (layOutType == LayOutType.Vertical)
        {
            childItem.transform.SetLocalPos(y: childItem.transform.localPosition.y+(-1f)* itemRect.height * (childCount - 1));
        }
        else
        {
            childItem.transform.SetLocalPos(y: childItem.transform.localPosition.x + (1f) * itemRect.width * (childCount - 1));
        }
        uIScrollView.AddItem(itemRect);
    }
}