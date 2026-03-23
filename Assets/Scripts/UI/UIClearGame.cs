using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class UIClearGame : UIBase
{
    [SerializeField]
    private TMP_Text textMessage;

    [SerializeField]
    private GameObject buttonNext;

    public override void OpenUI()
    {
        base.OpenUI();

        if(StageManager.instance.IsLastStage(GlobalGameData.currentStageIndex) == true)
        {
            textMessage.text = "Game Clear!!!";
            buttonNext.SetActive(false);
        }
    }

    public void OnClickNextStage()
    {
        Time.timeScale = 1.0f;

        // 클리어한 스테이지를 저장하고 스테이지 번호를 하나 증가시키고 씬을 다시 로딩.
        PlayerPrefs.SetInt("ClearStageNumber", GlobalGameData.currentStageIndex);
        PlayerPrefs.Save();

        GlobalGameData.currentStageIndex++;
        //GlobalGameData.needNextStage = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
