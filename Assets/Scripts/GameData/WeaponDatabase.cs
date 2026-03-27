using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class WeaponData
{
    public int id;
    public string name;
    public string desc;
    public Sprite thumbnail;
    public float damage;
    public ProjectileType type;
}

[CreateAssetMenu(fileName = "WeaponDatabase", menuName = "ScriptableObjects/WeaponDatabse")]
public class WeaponDatabase : ScriptableObject
{
    public List<WeaponData> weapons = new List<WeaponData>();

    public WeaponData GetWeaponData(int id)
    {
        foreach(WeaponData data in weapons)
        {
            if(data.id == id)
            {
                return data;
            }
        }

        return null;
    }
}
