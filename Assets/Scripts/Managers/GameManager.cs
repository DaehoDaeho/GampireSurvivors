using UnityEngine;

/// <summary>
/// 게임의 전반적인 상태(점수, 시간, 종료 여부)를 한 곳에서 관리하는 관리자 클래스.
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] private int gameScore;
    [SerializeField] private float playLimitTime = 60.0f;
    [SerializeField] private float playTime;
    [SerializeField] private bool isGameOver;
    [SerializeField] private int targetScore;
    [SerializeField] private EnemyDatabase enemyDatabase;
    [SerializeField] private WeaponDatabase projectileDatabase;
    [SerializeField] private StageDatabase stageDatabase;

    public static GameManager Instance;

    public Player playerObj;
    public PlayerController player;
    public AutoShooter autoShooter;
    public BladeOrbitManager bladeOrbitManager;

    [SerializeField] private UpgradeInfo upgradeInfo;

    private void Awake()
    {
        Instance = this;
        Time.timeScale = 1.0f;

        //if(GlobalGameData.needNextStage == true)
        //{
        //    GlobalGameData.currentStageIndex++;
        //    GlobalGameData.needNextStage = false;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        if (isGameOver == false)
        {
            UpdateGameTime();
        }

        if (Input.GetKeyDown(KeyCode.O) == true)
        {
            UIManager.Instance.OpenUI(UIType.GameOver);
        }

        if (Input.GetKeyDown(KeyCode.P) == true)
        {
            UIManager.Instance.CloseUI(UIType.GameOver);
        }
    }

    void UpdateGameTime()
    {   
        playTime += Time.deltaTime; // 이전 프레임에서 현재 프레임까지 오는데 걸린 시간.
                                    // 환경이 달라도 동일한 시간을 측정하게 해주는 표준 기능.
                                    // 프레임에 상관없이 일정한 속도로 움직이거나 시간을 처리할 때 사용.

        if(playTime >= playLimitTime)
        {
            isGameOver = true;
            // 스테이지 클리어 처리.
            UIManager.Instance.OpenUI(UIType.ClearGame);
            Time.timeScale = 0.0f;
        }
    }

    public void AddScore(int amount)
    {
        gameScore += amount;
    }

    void EndGame()
    {
        isGameOver = true;
    }

    public EnemyData GetEnemyData(int targetID)
    {
        if(enemyDatabase != null)
        {
            // EnemyData enemyData = enemyDatabase.GetEnemyData(targetID);
            // return enemyData;
            return enemyDatabase.GetEnemyData(targetID);
        }

        return null;
    }

    public WeaponData GetWeaponData(int targetID)
    {
        if (projectileDatabase != null)
        {
            return projectileDatabase.GetWeaponData(targetID);
        }

        return null;
    }

    public void UpdateUpgradeInfo(UpgradeData data)
    {
        if(upgradeInfo != null)
        {
            upgradeInfo.UpdateUpgradeInfo(data);
        }
    }

    public float GetPlayTime()
    {
        return playTime;
    }

    public StageData GetStageData(int id)
    {
        return stageDatabase.GetStageData(id);
    }
}
