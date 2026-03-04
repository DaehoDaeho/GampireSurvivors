using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class UIUpgrade : UIBase
{
    [SerializeField]
    private UpgradeDatabase upgradeDatabase;

    [SerializeField]
    private Image[] imageThumbnail;

    [SerializeField]
    private TMP_Text[] textDescription;

    private List<UpgradeData> upgradeDatas = new List<UpgradeData>();

    public override void OpenUI()
    {
        base.OpenUI();

        ShowUpgradeList();
    }

    void ShowUpgradeList()
    {
        List<UpgradeData> datas = upgradeDatabase.GetCopyDataList();

        for(int i=0; i<imageThumbnail.Length; ++i)
        {
            int index = Random.Range(0, datas.Count);
            imageThumbnail[i].sprite = datas[index].thumbnail;
            textDescription[i].text = datas[index].description;
            upgradeDatas.Add(datas[index]);
            datas.RemoveAt(index);
        }

        Time.timeScale = 0.0f;
    }

    public void OnClickUpgradeButton(int index)
    {
        switch(upgradeDatas[index].upgradeType)
        {
            case UpgradeType.PlayerMoveSpeed:
                {
                    GameManager.Instance.player.SetUpgradeMoveSpeed(upgradeDatas[index].upgradeValue);
                }
                break;

            case UpgradeType.ProjectileDamage:
                {
                    GameManager.Instance.autoShooter.SetUpgradeDamage(upgradeDatas[index].upgradeValue);
                }
                break;

            case UpgradeType.ProjectileMoveSpeed:
                {
                    GameManager.Instance.autoShooter.SetUpgradeMoveSpeed(upgradeDatas[index].upgradeValue);
                }
                break;
        }

        UIManager.Instance.CloseUI(UIType.Upgrade);
        Time.timeScale = 1.0f;
    }
}
