using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject enemyPrefab;

    [SerializeField]
    private float spawnInterval = 1.0f;

    [SerializeField]
    private float spawnRadius = 15.0f;

    private float currentTime = 0.0f;

    // Update is called once per frame
    void Update()
    {
        currentTime += Time.deltaTime;

        if(currentTime >= spawnInterval)
        {
            SpawnEnemy();
            currentTime = 0.0f;
        }
    }

    void SpawnEnemy()
    {
        if(GameManager.Instance == null || GameManager.Instance.player == null)
        {
            return;
        }

        // 방향 설정.
        // 반지름이 1인 원 내부에서 랜덤한 좌표값을 반환하고 그 좌표값을 정규화한다.
        Vector2 randomDirection = Random.insideUnitCircle.normalized;

        // 최종 스폰 위치 계산 = 플레이어 위치 + (무작위 방향 * 스폰 반지름)
        Vector3 playerPos = GameManager.Instance.player.transform.position;
        Vector3 spawnPosition = playerPos + (Vector3)(randomDirection * spawnRadius);

        // 계산된 위치에 적 프리팹을 생성.
        // 추후에 오브젝트 풀링을 사용하는 방식으로 개선해 볼 것.
        //Instantiate(enemyPrefab, spawnPosition, Quaternion.identity);
        if(PoolManager.instance != null)
        {
            GameObject enemy = PoolManager.instance.GetEnemy();
            enemy.transform.position = spawnPosition;
            enemy.transform.rotation = Quaternion.identity;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
