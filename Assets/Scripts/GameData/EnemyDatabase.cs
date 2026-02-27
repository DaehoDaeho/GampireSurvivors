using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class EnemyData
{
    public int enemyID;
    public string enemyName;
    public float maxHealth;
    public float moveSpeed;
    public int dropEXP;
}

[CreateAssetMenu(fileName = "EnemyDatabase", menuName = "ScriptableObjects/EnemyDatabase")]
public class EnemyDatabase : ScriptableObject
{
    public List<EnemyData> enemyDataList;

    public EnemyData GetEnemyData(int targetID)
    {
        for(int i=0; i<enemyDataList.Count; ++i)
        {
            if (enemyDataList[i].enemyID == targetID)
            {
                return enemyDataList[i];
            }
        }

        return null;
    }
}
