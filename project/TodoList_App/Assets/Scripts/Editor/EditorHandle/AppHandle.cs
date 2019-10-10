using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AppHandle
{
   [MenuItem("AppHandle/AddItemInPerDayPanel")]
   private static void AddItemInPerDayPanel()
   {
        for(int i = 0; i < 10; i++)
        {
            ItemManager.Ins.CreateItem("ABABABABABABABAAABABABABABABBBBBABBABAABABABAABAB", "10:00-12:00");
        }
    }
      
}
