using UnityEngine;
using System.Collections.Generic;
using System;

[Serializable]
public class ProjectileData
{
    public int projectileID;
    public float damage;
    public float moveSpeed;
}

[CreateAssetMenu(fileName = "ProjectileDatabase", menuName = "ScriptableObjects/ProjectileDatabase")]
public class ProjectileDatabase : ScriptableObject
{
    public List<ProjectileData> projectileDataList;

    public ProjectileData GetProjectileData(int projectileID)
    {
        for(int i=0; i<projectileDataList.Count; ++i)
        {
            if (projectileDataList[i].projectileID == projectileID)
            {
                return projectileDataList[i];
            }
        }

        return null;
    }
}
