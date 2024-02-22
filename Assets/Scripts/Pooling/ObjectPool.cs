using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
[AddComponentMenu("")]
public class PoolObject : MonoBehaviour
{
    public string poolName;
    public bool isPooled;
}

public enum PoolInflationType
{
    /// When a dynamic pool inflates, add one to the pool.
    INCREMENT,
    /// When a dynamic pool inflates, double the size of the pool
    DOUBLE
}


public class ObjectPool
{
    private Stack<PoolObject> availableObjStack = new Stack<PoolObject>();

    //the root obj for unused obj
    private GameObject rootObj;
    private PoolInflationType inflationType;
    private string poolName;
    private int objectsInUse = 0;

    public ObjectPool(string poolName, GameObject poolObjectPrefab, GameObject rootPoolObj, int initialCount, PoolInflationType type)
    {
        if (poolObjectPrefab == null)
        {
            return;
        }
        this.poolName = poolName;
        this.inflationType = type;
        this.rootObj = new GameObject(poolName + "Pool");
        this.rootObj.transform.SetParent(rootPoolObj.transform, false);

        // In case the origin one is Destroyed, we should keep at least one
        GameObject go = GameObject.Instantiate(poolObjectPrefab);
        PoolObject po = go.GetComponent<PoolObject>();
        if (po == null)
        {
            po = go.AddComponent<PoolObject>();
        }
        po.poolName = poolName;
        AddObjectToPool(po);

        //populate the pool
        populatePool(Mathf.Max(initialCount, 1));
    }

    //o(1)
    private void AddObjectToPool(PoolObject po)
    {
        //add to pool
        po.gameObject.SetActive(false);
        po.gameObject.name = poolName;
        availableObjStack.Push(po);
        po.isPooled = true;
        po.gameObject.transform.SetParent(rootObj.transform, false);
    }

    private void populatePool(int initialCount)
    {
        for (int index = 0; index < initialCount; index++)
        {
            PoolObject po = GameObject.Instantiate(availableObjStack.Peek());
            AddObjectToPool(po);
        }
    }

    //o(1)
    public GameObject NextAvailableObject(bool autoActive)
    {
        PoolObject po = null;
        if (availableObjStack.Count > 1)
        {
            po = availableObjStack.Pop();
        }
        else
        {
            int increaseSize = 0;
            if (inflationType == PoolInflationType.INCREMENT)
            {
                increaseSize = 1;
            }
            else if (inflationType == PoolInflationType.DOUBLE)
            {
                increaseSize = availableObjStack.Count + Mathf.Max(objectsInUse, 0);
            }
            if (increaseSize > 0)
            {
                populatePool(increaseSize);
                po = availableObjStack.Pop();
            }
        }

        GameObject result = null;
        if (po != null)
        {
            objectsInUse++;
            po.isPooled = false;
            result = po.gameObject;
            po.gameObject.transform.SetParent(null);
            if (autoActive)
            {
                result.SetActive(true);
            }
        }

        return result;
    }

    //o(1)
    public void ReturnObjectToPool(PoolObject po)
    {
        if (poolName.Equals(po.poolName))
        {
            objectsInUse--;
            if (!po.isPooled)
            {
                AddObjectToPool(po);
            }
        }
        else
        {
            Debug.LogError(string.Format("Trying to add object to incorrect pool {0} {1}", po.poolName, poolName));
        }
    }
}

