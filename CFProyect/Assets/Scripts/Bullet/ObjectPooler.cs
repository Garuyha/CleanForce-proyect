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

    // Método para configurar LinearDamping en cada bala de la pool
    public void SetLinearDampingForAllBullets(float newLinearDamping)
    {
        foreach (GameObject obj in pool)
        {
            Bullet bullet = obj.GetComponent<Bullet>();
            if (bullet != null)
            {
                bullet.SetLinearDamping(newLinearDamping);
            }
        }
    }
}
