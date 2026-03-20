using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class StageData
{
    public int stageNumber;
    public string stageName;
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
