using UnityEngine;
using TMPro;

public class StageManager : MonoBehaviour
{
    public static StageManager instance;

    [SerializeField]
    private StageDatabase stageDatabase;

    [SerializeField]
    private int currentStageIndex = 1;

    [SerializeField]
    private GameObject stageClearPanel;

    [SerializeField]
    private TMP_Text textCurrentStage;

    private void Awake()
    {
        instance = this;

        if(stageClearPanel != null)
        {
            stageClearPanel.SetActive(false);
        }
    }

    private void Start()
    {
        currentStageIndex = GlobalGameData.currentStageIndex;
        LoadStage(currentStageIndex);
    }

    public void LoadStage(int index)
    {
        if(index > stageDatabase.GetStageCount())
        {
            Debug.Log("모든 스테이지를 클리어 했습니다.");
            return;
        }

        StageData stageData = stageDatabase.GetStageData(index);
        if(stageData != null)
        {
            // 웨이브 데이터 등을 세팅.
            EnemySpawner.instance.SetWaveData(stageData.waveDatabase);
            textCurrentStage.text = "Stage " + stageData.stageNumber.ToString();
        }
    }

    public bool IsLastStage(int index)
    {
        if (index == stageDatabase.GetStageCount())
        {
            return true;
        }

        return false;
    }
}
