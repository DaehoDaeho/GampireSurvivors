using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

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
    private Image imageHpBar;

    [SerializeField]
    private Image imageExpBar;

    [SerializeField]
    private TMP_Text textLevel;

    [SerializeField]
    private UIBossHP bossHP;

    [SerializeField]
    private TMP_Text textCount;

    private void Awake()
    {
        Instance = this;

        bossHP.gameObject.SetActive(false);
        textCount.gameObject.SetActive(false);
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

    public void UpdateHpBar(float percent)
    {
        imageHpBar.fillAmount = percent;
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

    public void StartCount()
    {
        StartCoroutine(StartCountRoutine());
    }

    IEnumerator StartCountRoutine()
    {
        textCount.gameObject.SetActive(true);
        Time.timeScale = 0.0f;

        for(int i=3; i>= 0; --i)
        {
            textCount.text = i.ToString();
            yield return new WaitForSecondsRealtime(1.0f);
        }

        textCount.gameObject.SetActive(false);
        Time.timeScale = 1.0f;
        GameManager.Instance.InitTimer();
    }

    public void ShowRemainTime(int remainTime)
    {
        textCount.gameObject.SetActive(true);
        textCount.text = remainTime.ToString();
    }
}
