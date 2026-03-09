using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private WaveDatabase waveData;

    [SerializeField]
    private int currentWaveIndex;

    [SerializeField]
    private float timer;

    private void Awake()
    {
        currentWaveIndex = 0;
        timer = 0.0f;
    }

    private void Update()
    {
        // 웨이브 넘기기 처리와 몬스터 스폰 처리.
        float gameTime = GameManager.Instance.GetPlayTime();

        // 현재 웨이브가 마지막 웨이브가 아니면.
        if(currentWaveIndex < waveData.waves.Count - 1)
        {
            // 다음 웨이브로 넘어갈 시간이 됐는지 체크.
            if(gameTime >= waveData.waves[currentWaveIndex+1].startTime)
            {
                currentWaveIndex++;
            }
        }

        SpawnEnemy();
    }

    void SpawnEnemy()
    {
        timer += Time.deltaTime;
        WaveData currentWave = waveData.waves[currentWaveIndex];

        if(timer >= currentWave.spawnInterval)
        {
            GameObject enemy = PoolManager.instance.GetEnemy();

            // 플레이어의 중심으로부터 15미터 반경 밖의 한 점을 랜덤하게 설정해서 적의 생성위치를 지정.
            Vector2 randomDir = Random.insideUnitCircle.normalized;
            enemy.transform.position = GameManager.Instance.player.transform.position + (Vector3)(randomDir * 15.0f);

            timer = 0.0f;
        }
    }
}
