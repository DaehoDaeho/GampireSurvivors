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
    private GameObject expGemPrefab;

    [SerializeField]
    private GameObject damageTextPrefab;

    [SerializeField]
    private GameObject rangedEnemyPrefab;

    [SerializeField]
    private int enemyCount = 50;

    [SerializeField]
    private int projectileCount = 50;

    [SerializeField]
    private int expGemCount = 50;

    [SerializeField]
    private int damageTextCount = 100;

    [SerializeField]
    private int rangedEnemyCount = 50;

    private Queue<GameObject> enemyQueue = new Queue<GameObject>();
    private Queue<GameObject> projectileQueue = new Queue<GameObject>();
    private Queue<GameObject> expGemQueue = new Queue<GameObject>();
    private Queue<GameObject> damageTextQueue = new Queue<GameObject>();
    private Queue<GameObject> rangedEnemyQueue = new Queue<GameObject>();

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

        for (int i = 0; i < expGemCount; ++i)
        {
            // 경험치 구슬 객체를 생성해서 PoolManager 오브젝트의 자식으로 배치.
            GameObject go = Instantiate(expGemPrefab, transform);
            go.SetActive(false);
            expGemQueue.Enqueue(go); // 큐에 추가.
        }

        for (int i = 0; i < damageTextCount; ++i)
        {
            // 데미지 텍스트 객체를 생성해서 PoolManager 오브젝트의 자식으로 배치.
            GameObject go = Instantiate(damageTextPrefab, transform);
            go.SetActive(false);
            damageTextQueue.Enqueue(go); // 큐에 추가.
        }

        for (int i = 0; i < rangedEnemyCount; ++i)
        {
            // 원거리 적 객체를 생성해서 PoolManager 오브젝트의 자식으로 배치.
            GameObject go = Instantiate(rangedEnemyPrefab, transform);
            go.SetActive(false);
            rangedEnemyQueue.Enqueue(go); // 큐에 추가.
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

    public GameObject GetExpGem()
    {
        if (expGemQueue.Count > 0)
        {
            GameObject expGem = expGemQueue.Dequeue();
            expGem.SetActive(true);
            return expGem;
        }

        GameObject expGem2 = Instantiate(expGemPrefab, transform);
        expGem2.SetActive(true);
        return expGem2;
    }

    public void ReturnExpGem(GameObject expGem)
    {
        expGem.SetActive(false);
        expGemQueue.Enqueue(expGem);
    }

    public GameObject GetDamageText()
    {
        if (damageTextQueue.Count > 0)
        {
            GameObject damageText = damageTextQueue.Dequeue();
            damageText.SetActive(true);
            return damageText;
        }

        GameObject damageText2 = Instantiate(damageTextPrefab, transform);
        damageText2.SetActive(true);
        return damageText2;
    }

    public void ReturnDamageText(GameObject damageText)
    {
        damageText.SetActive(false);
        damageTextQueue.Enqueue(damageText);
    }

    public GameObject GetRangedEnemy()
    {
        if (rangedEnemyQueue.Count > 0)
        {
            GameObject renagedEnemy = rangedEnemyQueue.Dequeue();
            renagedEnemy.SetActive(true);
            return renagedEnemy;
        }

        GameObject renagedEnemy2 = Instantiate(rangedEnemyPrefab, transform);
        renagedEnemy2.SetActive(true);
        return renagedEnemy2;
    }

    public void ReturnRangedEnemy(GameObject rangedEnemy)
    {
        rangedEnemy.SetActive(false);
        rangedEnemyQueue.Enqueue(rangedEnemy);
    }
}
