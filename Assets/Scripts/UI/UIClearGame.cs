using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Data.Common;
using UnityEngine.UI;

public class UIClearGame : UIBase
{
    [SerializeField]
    private TMP_Text textMessage;

    [SerializeField]
    private TMP_Text textReward;

    [SerializeField]
    private Image imageReward;

    [SerializeField]
    private GameObject buttonNext;

    [SerializeField]
    private Sprite defaultRewardThumbnail;
    [SerializeField]
    private int defaultRewardCount = 30;

    private StageData stageData = null;

    public override void OpenUI()
    {
        base.OpenUI();

        // 클리어한 스테이지를 저장하고 스테이지 번호를 하나 증가시키고 씬을 다시 로딩.
        //PlayerPrefs.SetInt("ClearStageNumber", GlobalGameData.currentStageIndex);
        //PlayerPrefs.Save();

        if(GlobalGameData.currentStageIndex > DataManager.GetClearedStage())
        {
            DataManager.SetCleardStage(GlobalGameData.currentStageIndex);
        }

        if (StageManager.instance.IsLastStage(GlobalGameData.currentStageIndex) == true)
        {
            textMessage.text = "Game Clear!!!";
            buttonNext.SetActive(false);
        }

        // 보상 출력과 지급 처리.
        stageData = GameManager.Instance.GetStageData(GlobalGameData.currentStageIndex);
        ShowRewardData();
        GainReward();
    }

    void ShowRewardData()
    {
        if(stageData == null)
        {
            imageReward.sprite = null;
            textReward.text = string.Empty;

            return;
        }

        // 이미 클리어한 스테이지일 경우 기본 보상 정보 출력.
        if (GlobalGameData.currentStageIndex <= DataManager.GetClearedStage())
        {
            imageReward.sprite = defaultRewardThumbnail;
            textReward.text = defaultRewardCount.ToString();
            return;
        }

        imageReward.sprite = stageData.reward.thumbnail;
        switch(stageData.reward.type)
        {
            case RewardType.Gold:
                {
                    textReward.text = stageData.reward.value.ToString();
                }
                break;

            case RewardType.Weapon:
                {
                    WeaponData weapon = GameManager.Instance.GetWeaponData(stageData.reward.value);
                    if(weapon != null)
                    {
                        textReward.text = weapon.name;
                    }
                }
                break;
        }
    }

    void GainReward()
    {
        if(stageData == null)
        {
            return;
        }

        // 이미 클리어한 스테이지일 경우 기본 보상만 지급.
        if (GlobalGameData.currentStageIndex <= DataManager.GetClearedStage())
        {
            DataManager.AddGold(defaultRewardCount);
            return;
        }

        switch (stageData.reward.type)
        {
            case RewardType.Gold:
                {
                    DataManager.AddGold(stageData.reward.value);
                }
                break;

            case RewardType.Weapon:
                {
                    MyItemInfo item = new MyItemInfo();
                    item.id = stageData.reward.value;
                    item.invenIndex = DataManager.GetItemCount();
                    DataManager.AddItem(item);
                }
                break;
        }
    }

    public void OnClickNextStage()
    {
        Time.timeScale = 1.0f;        

        GlobalGameData.currentStageIndex++;
        //GlobalGameData.needNextStage = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnClickGotoLobby()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("LobbyScene");
    }
}
