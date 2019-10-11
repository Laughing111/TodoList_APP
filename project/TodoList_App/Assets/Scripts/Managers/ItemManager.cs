using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    private GameObject itemBG;
    private void Awake()
    {
       itemBG = transform.Find("ScrollRect/ItemBG").gameObject;
       layOutManager = itemBG.GetComponent<LayOutManager>();
        itemBG.GetComponent<UIScrollView>().AddtheRebackFinishEvent(ScrollViewEndEditeItem);
    }

    /// <summary>
    /// 滑动结束后，切换至输入界面
    /// </summary>
    private void ScrollViewEndEditeItem()
    {
        Debug.Log("切换至输入Item页面");
        CanvasManager.Ins.OpenOrCloseInputCanvas(true);
    }

    private LayOutManager layOutManager;

    public void CreateItem(string content, string timeDeadLine = null)
    {
        GameObject itemPrefab = ItemCreateFactory.CloneItemPrefab();
        GameObject itemObject = Instantiate(itemPrefab);
        itemObject.transform.Find("itemContent").GetComponent<Text>().text = content;
        itemObject.transform.Find("sepLine/timeRange").GetComponent<Text>().text = timeDeadLine;
        layOutManager.AddChildItem(itemObject.transform);
    }
}