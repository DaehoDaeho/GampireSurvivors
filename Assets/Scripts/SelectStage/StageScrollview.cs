using UnityEngine;
using TMPro;

public enum StageState
{
    Cleared = 0,
    CanEnter = 1,
    Locked = 2
}

public class StageScrollview : MonoBehaviour
{
    public static StageScrollview instance;

    [SerializeField]
    private GameObject stageCard;

    [SerializeField]
    private GameObject content;

    [SerializeField]
    private StageDatabase stageDatabase;

    [SerializeField]
    private TMP_Text textStageDesc;

    private StageData SelectedStageData = null;

    private void Awake()
    {
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        int clearStageNum = PlayerPrefs.GetInt("ClearStageNumber", 0);

        for(int i=0; i<stageDatabase.stages.Count; ++i)
        {
            GameObject obj = Instantiate(stageCard, content.transform);
            StageCard card = obj.GetComponent<StageCard>();
            card.SetStageData(stageDatabase.stages[i]);

            // 이미 클리어 한 스테이지인지 체크.
            if (stageDatabase.stages[i].stageNumber <= clearStageNum)
            {
                card.SetStageState(StageState.Cleared);
            }
            else if (stageDatabase.stages[i].stageNumber == clearStageNum + 1)  // 클리어한 다음 스테이지인지 체크.
            {
                card.SetStageState(StageState.CanEnter);
            }
            else
            {
                card.SetStageState(StageState.Locked);
            }
        }
    }

    public void SetSelectedStageData(StageData data)
    {
        SelectedStageData = data;
        textStageDesc.text = data.stageDesc;
    }
}
