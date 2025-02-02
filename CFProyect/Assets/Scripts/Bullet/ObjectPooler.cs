using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObjectPooler : MonoBehaviour
{
    public GameObject prefab;
    public int poolSize = 10;

    private List<GameObject> pool;

    void Start()
    {
        InitializePool();
    }

    private void InitializePool()
    {
        pool = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            CreateNewObj();
        }
    }

    public GameObject GetPoolObject()
    {
        foreach (GameObject obj in pool)
        {
            if (!obj.activeInHierarchy)
            {
                return obj;
            }
        }
        return CreateNewObj();
    }

    private GameObject CreateNewObj()
    {
        GameObject obj = Instantiate(prefab, transform);
        obj.SetActive(false);
        pool.Add(obj);
        return obj;
    }

    public void SetLinearDampingForAllBullets(float newLinearDamping)
{
    if (pool == null || pool.Count == 0)
    {
        Debug.LogWarning("La pool no está inicializada o está vacía.");
        return;
    }

    foreach (GameObject obj in pool)
    {
        if (obj == null) continue;

        Bullet bullet = obj.GetComponent<Bullet>();
        if (bullet != null)
        {
            bullet.SetLinearDamping(newLinearDamping);
        }
        else
        {
            Debug.LogWarning($"El objeto {obj.name} en la pool no tiene un componente Bullet.");
        }
    }
}

}
