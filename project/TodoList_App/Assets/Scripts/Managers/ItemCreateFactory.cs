using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCreateFactory:MonoBehaviour
{
    public static GameObject CloneItemPrefab()
    {
        GameObject gameObject = ResourcesManager.Load<GameObject>("Item");
       
        return gameObject;
    }
}

public class ResourcesManager
{
    private static Dictionary<string, Object> resourcesCachePool;

    public static T Load<T>(string path) where T : Object
    {
        T result = null;
        if (resourcesCachePool == null)
        {
            resourcesCachePool = new Dictionary<string, Object>();
        }
        else
        {
            if (resourcesCachePool.ContainsKey(path))
            {
                result = resourcesCachePool[path] as T;
                return result;
            }
        }

        result = Resources.Load<T>(path);
        resourcesCachePool.Add(path, result);
        return result;
    }
}