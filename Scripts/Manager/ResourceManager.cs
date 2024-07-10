using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ResourceManager
{
    public static GameObject SpawnGameObject(string path)
    {
        var prefab = Resources.Load<GameObject>(path);
        if (prefab == null)
        {
            Debug.LogError($"프리팹 로드에 실패했습니다. {path}");
            return null;
        }

        return PoolManager.SpawnObject(prefab);
    }

    public static void ReleaseGameObject(this GameObject go)
    {
        PoolManager.ReleaseObject(go);
    }
}