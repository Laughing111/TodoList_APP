using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    public BGManager BGManager;
    public ItemManager ItemManager;
    public InputManager InputManager;
    

    private static CanvasManager ins;
    public static CanvasManager Ins
    {
        get
        {
            if (ins == null)
            {
                ins = Object.FindObjectOfType<CanvasManager>();
            }
            return ins;
        }
    }

    public void OpenOrCloseInputCanvas(bool isOpen)
    {
        InputManager.OpenOrCloseCanvas(isOpen);
    }
}
