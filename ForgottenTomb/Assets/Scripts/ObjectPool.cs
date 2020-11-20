using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    #region Editor Variables

    [SerializeField]
    private GameObject prefab;

    #endregion

    #region Runtime Variables

    private Queue<GameObject> availableObjects = new Queue<GameObject>();

    #endregion

    #region Functions

    //private void Start()
    //{
    //    if (prefab != null)
    //    {
    //        print("Growing pool, as we were initialized with an inspector prefab");
    //        GrowPool();
    //    }
    //}

    public void CreateObjectPoolWithPrefab(GameObject _prefab, int initialObjectCount = 10)
    {
        prefab = _prefab;
        print("Growing pool, as we were initialized by a script with a prefab");
        GrowPool(initialObjectCount);
        this.gameObject.name = prefab.name + " Object Pool";
    }

    private void GrowPool(int initialCount = 10)
    {
        if (prefab == null)
        {
            print("No prefab yet specified");
            return;
        }

        for (int i = 0; i < initialCount; i++)
        {
            var instanceToAdd = Instantiate(prefab);
            instanceToAdd.transform.SetParent(transform);
            AddToPool(instanceToAdd);
        }
    }

    public void AddToPool(GameObject instance)
    {
        instance.SetActive(false);
        availableObjects.Enqueue(instance);
    }

    public GameObject GetFromPool()
    {
        if (availableObjects.Count == 0)
        {
            print("Pool was all used up, so adding more to the pool");
            GrowPool();
        }
        //print("activating an instance");

        var instance = availableObjects.Dequeue();
        instance.SetActive(true);
        return instance;
    }

    #endregion


}
