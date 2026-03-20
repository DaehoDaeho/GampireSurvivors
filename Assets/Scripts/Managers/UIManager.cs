using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum UIType
{
    GameOver = 0,
    Upgrade = 1,
    ClearGame = 2
}

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private UIBase currentOpenedUI = null;

    [SerializeField]
    private UIBase[] uis;

    [SerializeField]
    private Image imageExpBar;

    [SerializeField]
    private TMP_Text textLevel;

    [SerializeField]
    private UIBossHP bossHP;

    private void Awake()
    {
        Instance = this;

        bossHP.gameObject.SetActive(false);
    }

    public void OpenUI(UIType type)
    {
        uis[(int)type].OpenUI();
        currentOpenedUI = uis[(int)type];
    }

    public void CloseUI(UIType type)
    {
        if(currentOpenedUI != null && currentOpenedUI.IsOpened() == true)
        {
            currentOpenedUI.CloseUI();
            currentOpenedUI = null;
        }
    }

    public void UpdateExpBar(float percent)
    {
        imageExpBar.fillAmount = percent;
    }

    public void UpdateLevel(int level)
    {
        textLevel.text = "LV " + level.ToString();
    }

    public void SetBossHPVisible(bool visible)
    {
        bossHP.gameObject.SetActive(visible);
    }

    public void UpdateBossHP(float percent)
    {
        bossHP.UpdateHP(percent);
    }
}
