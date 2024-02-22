using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[AddComponentMenu("")]
public class PoolingManager : MonoBehaviour
{
    private Dictionary<string, ObjectPool> poolDict = new Dictionary<string, ObjectPool>();

    private static PoolingManager mInstance = null;

    public static PoolingManager Instance
    {
        get
        {
            if (mInstance == null)
            {
                GameObject go = new GameObject("PoolingManager", typeof(PoolingManager));
                go.transform.localPosition = new Vector3(999f, 999f, 999f);
                mInstance = go.GetComponent<PoolingManager>();

                if (Application.isPlaying)
                {
                    DontDestroyOnLoad(mInstance.gameObject);
                }
            }
            return mInstance;
        }
    }

    private void Awake()
    {
        InitPool("Bullet", 100, PoolInflationType.INCREMENT);
        InitPool("Explosion", 5, PoolInflationType.INCREMENT);
        InitPool("HardEnemy", 5, PoolInflationType.INCREMENT);
        InitPool("EasyEnemy", 5, PoolInflationType.INCREMENT);
    }

    //We can use this to load prefabs in advance, avoiding calling resources.load
    public void InitPool(GameObject prefab, int size, PoolInflationType type = PoolInflationType.INCREMENT)
    {
        string poolName = prefab.name;
        if (!poolDict.ContainsKey(poolName))
        {
            poolDict[poolName] = new ObjectPool(poolName, prefab, gameObject, size, type);
        }
    }

    public void InitPool(string poolName, int size, PoolInflationType type = PoolInflationType.DOUBLE)
    {
        if (poolDict.ContainsKey(poolName))
        {
            return;
        }
        else
        {
            GameObject pb = Resources.Load<GameObject>(poolName);
            if (pb == null)
            {
                Debug.LogError("[PoolingManager] Invalid prefab name for pooling :" + poolName);
                return;
            }
            poolDict[poolName] = new ObjectPool(poolName, pb, gameObject, size, type);
        }
    }

    // Returns an available object from the pool 
    // OR null in case the pool does not have any object available & can grow size is false.
    public GameObject GetObjectFromPool(string poolName, bool autoActive = true, int autoCreate = 0)
    {
        GameObject result = null;

        if (!poolDict.ContainsKey(poolName) && autoCreate > 0)
        {
            InitPool(poolName, autoCreate, PoolInflationType.INCREMENT);
        }

        if (poolDict.ContainsKey(poolName))
        {
            ObjectPool pool = poolDict[poolName];
            result = pool.NextAvailableObject(autoActive);
            //scenario when no available object is found in pool
#if UNITY_EDITOR
            if (result == null)
            {
                Debug.LogWarning("[PoolingManager]:No object available in " + poolName);
            }
#endif
        }
#if UNITY_EDITOR
        else
        {
            Debug.LogError("[PoolingManager]:Invalid pool name specified: " + poolName);
        }
#endif
        return result;
    }

    // Return obj to the pool
    public void ReturnObjectToPool(GameObject go)
    {
        PoolObject po = go.GetComponent<PoolObject>();
        if (po == null)
        {
#if UNITY_EDITOR
            Debug.LogWarning("Specified object is not a pooled instance: " + go.name);
#endif
        }
        else
        {
            ObjectPool pool = null;
            if (poolDict.TryGetValue(po.poolName, out pool))
            {
                pool.ReturnObjectToPool(po);
            }
#if UNITY_EDITOR
            else
            {
                Debug.LogWarning("No pool available with name: " + po.poolName);
            }
#endif
        }
    }

    // Return obj transform to the pool
    public void ReturnTransformToPool(Transform t)
    {
        if (t == null)
        {
#if UNITY_EDITOR
            Debug.LogError("[PoolingManager] try to return a null transform to pool!");
#endif
            return;
        }
        ReturnObjectToPool(t.gameObject);
    }
}

