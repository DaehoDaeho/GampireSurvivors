using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

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
    private GameObject selectStagePanel;

    [SerializeField]
    private GameObject stageCard;

    [SerializeField]
    private GameObject content;

    [SerializeField]
    private StageDatabase stageDatabase;

    [SerializeField]
    private TMP_Text textStageDesc;

    [SerializeField]
    private GameObject buttonStart;

    [SerializeField]
    private GameObject infoRoot;

    private StageData SelectedStageData = null;

    private List<StageCard> listCards = new List<StageCard>();

    private void Awake()
    {
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        infoRoot.SetActive(false);
    }

    public void SetSelectedStageData(StageData data)
    {
        SelectedStageData = data;
        textStageDesc.text = data.stageDesc;

        infoRoot.SetActive(true);
        ProcessStartButton();
    }

    void ProcessStartButton()
    {
        // 현재 선택된 항목이 없을 경우 처리.
        if(SelectedStageData == null)
        {
            buttonStart.SetActive(false);
            return;
        }

        int clearStageNum = PlayerPrefs.GetInt("ClearStageNumber", 0);

        if (SelectedStageData.stageNumber <= clearStageNum || SelectedStageData.stageNumber == clearStageNum + 1)
        {
            buttonStart.SetActive(true);
        }        
        else
        {
            buttonStart.SetActive(false);
        }
    }

    public void OnClickStart()
    {
        GlobalGameData.currentStageIndex = SelectedStageData.stageNumber;
        SceneManager.LoadScene("SampleScene");
    }

    public void SetSelectStagePanelVisible(bool visible)
    {
        selectStagePanel.SetActive(visible);
    }

    public void SetSelectStagePanelData()
    {
        int clearStageNum = PlayerPrefs.GetInt("ClearStageNumber", 0);

        for (int i = 0; i < stageDatabase.stages.Count; ++i)
        {
            GameObject obj = Instantiate(stageCard, content.transform);
            StageCard card = obj.GetComponent<StageCard>();
            card.SetStageData(stageDatabase.stages[i]);
            listCards.Add(card);

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

    public void OnClickExit()
    {
        SetSelectStagePanelVisible(false);

        foreach(StageCard card in listCards)
        {
            Destroy(card.gameObject);
        }

        listCards.Clear();
    }
}
