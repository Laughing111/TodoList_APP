using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AppHandle
{
   [MenuItem("AppHandle/AddItemInPerDayPanel")]
   private static void AddItemInPerDayPanel()
   {
      ItemManager.Ins.CreateItem("学习AB框架","10:00-12:00");
   }
}
