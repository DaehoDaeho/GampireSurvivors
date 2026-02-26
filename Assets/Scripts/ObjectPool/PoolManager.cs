using UnityEngine;
using System.Collections.Generic;   // Quque 자료구조를 사용하기 위한 네임스페이스.

public class PoolManager : MonoBehaviour
{
    public static PoolManager instance;

    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private GameObject projectilePrefab;

    [SerializeField]
    private int enemyCount = 50;

    [SerializeField]
    private int projectileCount = 50;

    private Queue<GameObject> enemyQueue = new Queue<GameObject>();
    private Queue<GameObject> projectileQueue = new Queue<GameObject>();

    private void Awake()
    {
        instance = this;

        for(int i=0; i<enemyCount; ++i)
        {
            // 적 객체를 생성해서 PoolManager 오브젝트의 자식으로 배치.
            GameObject go = Instantiate(enemyPrefab, transform);
            go.SetActive(false);
            enemyQueue.Enqueue(go); // 큐에 추가.
        }

        for (int i = 0; i < projectileCount; ++i)
        {
            // 총알 객체를 생성해서 PoolManager 오브젝트의 자식으로 배치.
            GameObject go = Instantiate(projectilePrefab, transform);
            go.SetActive(false);
            projectileQueue.Enqueue(go); // 큐에 추가.
        }
    }

    /// <summary>
    /// 큐에서 사용가능한 적 오브젝트를 반환하거나 새로 생성해서 반환.
    /// </summary>
    /// <returns>적 오브젝트</returns>
    public GameObject GetEnemy()
    {
        if(enemyQueue.Count > 0)
        {
            // 큐에서 데이터를 꺼낸다.
            GameObject enemy = enemyQueue.Dequeue();
            enemy.SetActive(true);
            return enemy;
        }
        
        GameObject enemy2 = Instantiate(enemyPrefab, transform);
        enemy2.SetActive(true);
        return enemy2;
    }

    public void ReturnEnemy(GameObject enemy)
    {
        enemy.SetActive(false);
        enemyQueue.Enqueue(enemy);
    }

    // 총알을 큐에서 꺼내오고 반환하는 함수 추가.
    public GameObject GetProjectile()
    {
        if(projectileQueue.Count > 0)
        {
            GameObject projectile = projectileQueue.Dequeue();
            projectile.SetActive(true);
            return projectile;
        }

        GameObject projectile2 = Instantiate(projectilePrefab, transform);
        projectile2.SetActive(true);
        return projectile2;
    }

    public void ReturnProjectile(GameObject projectile)
    {
        projectile.SetActive(false);
        projectileQueue.Enqueue(projectile);
    }
}
