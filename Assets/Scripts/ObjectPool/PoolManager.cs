using UnityEngine;
using System.Collections.Generic;
using UnityEditor.Animations;   // Quque 자료구조를 사용하기 위한 네임스페이스.

public enum PoolID : int
{
    None = 0,
    Projectile,
    Enemy,
    RangedEnemy,
    DamageText,
    ExpGem,
    EnemyProjectile,
    HitEffect
}

[System.Serializable]
public class PoolInfo
{
    public PoolID id;
    public GameObject prefab;
    public int count;
}

public class PoolManager : MonoBehaviour
{
    public static PoolManager instance;

    [SerializeField]
    private List<PoolInfo> poolInfos;

    private Dictionary<PoolID, Queue<GameObject>> poolDictionary = new Dictionary<PoolID, Queue<GameObject>>();

    private void Awake()
    {
        instance = this;

        for(int i=0; i<poolInfos.Count; ++i)
        {
            Queue<GameObject> objectQueue = new Queue<GameObject>();

            for(int j=0; j < poolInfos[i].count; ++j)
            {
                GameObject obj = Instantiate(poolInfos[i].prefab, transform);
                obj.SetActive(false);
                objectQueue.Enqueue(obj);
            }

            poolDictionary.Add(poolInfos[i].id, objectQueue);
        }
    }

    public GameObject GetObject(PoolID id)
    {
        // id가 Dictionary에 포함되어 있는지 체크.
        if(poolDictionary.ContainsKey(id) == false)
        {
            return null;
        }

        if (poolDictionary[id].Count > 0)
        {
            GameObject obj = poolDictionary[id].Dequeue();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            GameObject prefab = GetPrefabInfo(id);
            if(prefab != null)
            {
                GameObject obj = Instantiate(prefab, transform);
                obj.SetActive(true);
                return obj;
            }
        }

        return null;
    }

    public GameObject GetPrefabInfo(PoolID id)
    {
        for(int i=0; i<poolInfos.Count; ++i)
        {
            if (poolInfos[i].id == id)
            {
                return poolInfos[i].prefab;
            }
        }

        return null;
    }

    public void ReturnObject(PoolID id, GameObject obj)
    {
        obj.SetActive(false);
        poolDictionary[id].Enqueue(obj);
    }
}
