using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemManager : MonoBehaviour
{
    private ItemManager()
    {
    }

    private static ItemManager ins;

    public static ItemManager Ins
    {
        get
        {
            if (ins == null)
            {
                ins = GameObject.Find("ItemManager").GetComponent<ItemManager>();
#if UNITY_EDITOR
                if (ins.FinishCloneAndGoToAdd == null)
                {
                    GameObject.Find("ItemBG").GetComponent<LayOutManager>().Init();
                }
#endif
            }
            return ins;
        }
    }

    public Action<Transform> FinishCloneAndGoToAdd;

    public void CreateItem(string content, string timeDeadLine = null)
    {
        GameObject itemPrefab = ItemCreateFactory.CloneItemPrefab();
        GameObject itemObject = Instantiate(itemPrefab);
        itemObject.transform.Find("itemContent").GetComponent<Text>().text = content;
        itemObject.transform.Find("sepLine/timeRange").GetComponent<Text>().text = timeDeadLine;
        FinishCloneAndGoToAdd?.Invoke(itemObject.transform);
    }
}