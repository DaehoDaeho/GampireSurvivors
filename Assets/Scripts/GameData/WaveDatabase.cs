using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class WaveData
{
    public string waveName;
    public float startTime;
    public float spawnInterval;
}

[CreateAssetMenu(fileName = "WaveDatabase", menuName = "ScriptableObjects/WaveDatabase")]
public class WaveDatabase : ScriptableObject
{
    public List<WaveData> waves;
}
