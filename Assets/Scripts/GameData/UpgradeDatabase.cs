using UnityEngine;
using System.Collections.Generic;

public enum UpgradeType
{
    PlayerMoveSpeed = 0,
    ProjectileDamage = 1,
    ProjectileMoveSpeed = 2
}

[System.Serializable]
public class UpgradeData
{
    public UpgradeType upgradeType;
    public Sprite thumbnail;
    public string description;
    public float upgradeValue;
}

[CreateAssetMenu(fileName = "UpgradeDatabase", menuName = "ScriptableObjects/UpgradeDatabase")]
public class UpgradeDatabase : ScriptableObject
{
    public List<UpgradeData> upgradeDataList;

    public List<UpgradeData> GetCopyDataList()
    {
        List<UpgradeData> upgradeDatas = new List<UpgradeData>();
        for(int i=0; i<upgradeDataList.Count; ++i)
        {
            UpgradeData upgradeData = new UpgradeData();
            upgradeData.upgradeType = upgradeDataList[i].upgradeType;
            upgradeData.thumbnail = upgradeDataList[i].thumbnail;
            upgradeData.description = upgradeDataList[i].description;
            upgradeData.upgradeValue = upgradeDataList[i].upgradeValue;
            upgradeDatas.Add(upgradeData);
        }

        return upgradeDatas;
    }
}
