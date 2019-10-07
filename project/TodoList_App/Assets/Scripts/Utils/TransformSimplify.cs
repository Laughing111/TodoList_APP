using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformSimplify
{
   public static void SetLocalPos(this Transform trans,float x = 0, float y = 0, float z = 0)
   {
      Vector3 pos = new Vector3(x,y,z);
      trans.localPosition = pos;
   }
}
