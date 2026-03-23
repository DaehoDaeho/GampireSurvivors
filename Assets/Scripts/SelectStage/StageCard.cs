using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class StageCard : MonoBehaviour
{
    [SerializeField]
    private TMP_Text textButton;

    [SerializeField]
    private Image imageLock;

    [SerializeField]
    private Image buttonStage;

    StageData stageData = null;
    
    public void SetStageData(StageData data)
    {
        stageData = data;

        textButton.text = data.stageName;
    }

    public void SetStageState(StageState state)
    {
        switch(state)
        {
            case StageState.Cleared:
                {
                    imageLock.gameObject.SetActive(false);
                    buttonStage.color = Color.yellow;
                }
                break;

            case StageState.CanEnter:
                {
                    imageLock.gameObject.SetActive(false);
                    buttonStage.color = Color.green;
                }
                break;

            case StageState.Locked:
                {
                    imageLock.gameObject.SetActive(true);
                    buttonStage.color = Color.white;
                }
                break;
        }
    }

    public void OnClick()
    {
        StageScrollview.instance.SetSelectedStageData(stageData);
    }
}
