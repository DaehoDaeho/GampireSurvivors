using UnityEngine;
using System.Collections.Generic;

public enum RewardType
{
    Gold = 0,
    Weapon
}

[System.Serializable]
public class RewardData
{
    public RewardType type;
    public int value;
    public Sprite thumbnail;
}

[System.Serializable]
public class StageData
{
    public int stageNumber;
    public string stageName;
    public string stageDesc;
    public RewardData reward;
    public WaveDatabase waveDatabase;
}

[CreateAssetMenu(fileName = "StageDatabase", menuName = "ScriptableObjects/StageDatabase")]
public class StageDatabase : ScriptableObject
{
    public List<StageData> stages = new List<StageData>();

    public StageData GetStageData(int stageNumber)
    {
        foreach(StageData data in stages)
        {
            if(data.stageNumber == stageNumber)
            {
                return data;
            }
        }

        return null;
    }

    public int GetStageCount()
    {
        return stages.Count;
    }
}
