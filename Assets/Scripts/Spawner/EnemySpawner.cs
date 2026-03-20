using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static EnemySpawner instance;

    [SerializeField]
    private WaveDatabase waveData;

    [SerializeField]
    private int currentWaveIndex;

    [SerializeField]
    private float timer;

    [SerializeField]
    private GameObject bossPrefab;

    private bool bossSpawnedInCurrentWave = false;

    private bool canSpawn = false;  // 스폰을 시작해도 되는지 여부.

    private void Awake()
    {
        instance = this;        
    }

    private void Update()
    {
        if(canSpawn == false)
        {
            return;
        }

        // 웨이브 넘기기 처리와 몬스터 스폰 처리.
        float gameTime = GameManager.Instance.GetPlayTime();

        // 현재 웨이브가 마지막 웨이브가 아니면.
        if(currentWaveIndex < waveData.waves.Count - 1)
        {
            // 다음 웨이브로 넘어갈 시간이 됐는지 체크.
            if(gameTime >= waveData.waves[currentWaveIndex+1].startTime)
            {
                currentWaveIndex++;
                bossSpawnedInCurrentWave = false;
                timer = 0.0f;
            }
        }

        SpawnEnemy();
    }

    void SpawnEnemy()
    {
        timer += Time.deltaTime;
        WaveData currentWave = waveData.waves[currentWaveIndex];

        if(currentWave.isBossWave == true)
        {
            if(bossSpawnedInCurrentWave == false)
            {
                bossSpawnedInCurrentWave = true;
                SpawnBoss();
            }
        }
        else
        {
            if (timer >= currentWave.spawnInterval)
            {
                // 랜덤하게 일반 적/원거리 적을 생성.
                // 확륙은 1 : 1
                GameObject enemy = null;
                PoolID[] pools = { PoolID.Enemy, PoolID.RangedEnemy, PoolID.SuicideEnemy };
                //PoolID[] pools = { PoolID.SuicideEnemy };
                int number = Random.Range(0, pools.Length);    // 0부터 배열의 길이-1 사이의 숫자를 무작위로 뽑는다.

                enemy = PoolManager.instance.GetObject(pools[number]);

                // 플레이어의 중심으로부터 15미터 반경 밖의 한 점을 랜덤하게 설정해서 적의 생성위치를 지정.
                Vector2 randomDir = Random.insideUnitCircle.normalized;
                enemy.transform.position = GameManager.Instance.player.transform.position + (Vector3)(randomDir * 15.0f);

                timer = 0.0f;
            }
        }
    }

    void SpawnBoss()
    {
        GameObject boss = Instantiate(bossPrefab);
        Vector2 randomDir = Random.insideUnitCircle.normalized;
        boss.transform.position = GameManager.Instance.player.transform.position + (Vector3)(randomDir * 15.0f);

        BossIntroManager.instance.PlayBossIntro(boss.transform.position);
    }

    public void SetWaveData(WaveDatabase waveDatabase)
    {
        waveData = waveDatabase;

        currentWaveIndex = 0;
        timer = 0.0f;
        canSpawn = true;
    }
}
