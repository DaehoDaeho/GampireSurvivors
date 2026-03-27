using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class ItemShopData
{
    public int shopID;
    public int itemID;
    public int price;
}

[CreateAssetMenu(fileName = "ItemShopDatabase", menuName = "ScriptableObjects/ItemShopDatabase")]
public class ItemShopDatabase : ScriptableObject
{
    public List<ItemShopData> items = new List<ItemShopData>();

    public ItemShopData GetShopItemData(int id)
    {
        foreach(ItemShopData data in items)
        {
            if(data.shopID == id)
            {
                return data;
            }
        }

        return null;
    }
}
