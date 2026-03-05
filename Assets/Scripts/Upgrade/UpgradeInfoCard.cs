using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeInfoCard : MonoBehaviour
{
    [SerializeField]
    private Image upgradeIcon;

    [SerializeField]
    private TMP_Text textCount;

    private UpgradeData data;
    private int upgradeCount;

    public void SetData(UpgradeData upgradeData)
    {
        data = upgradeData;
        SetIcon(data.thumbnail);
        AddUpgradeCount();
    }

    public void SetIcon(Sprite sprite)
    {
        upgradeIcon.sprite = sprite;
    }

    public void AddUpgradeCount()
    {
        ++upgradeCount;
        textCount.text = " x " + upgradeCount.ToString();
    }

    public UpgradeType GetUpgradeType()
    {
        return data.upgradeType;
    }
}
