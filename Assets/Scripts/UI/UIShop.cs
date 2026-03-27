using UnityEngine;
using System.Collections.Generic;

public class UIShop : MonoBehaviour
{
    [SerializeField]
    private GameObject shopPanel;

    [SerializeField]
    private Transform content;

    [SerializeField]
    private GameObject shopCardPrefab;

    [SerializeField]
    private ItemShopDatabase itemShopDatabase;

    [SerializeField]
    private WeaponDatabase weaponDatabase;

    private List<ShopCard> cardList = new List<ShopCard>();
    
    public void SetShopVisible(bool visible)
    {
        shopPanel.SetActive(visible);
    }

    public void SetShopData()
    {
        foreach(ItemShopData data in itemShopDatabase.items)
        {
            GameObject go = Instantiate(shopCardPrefab, content);
            ShopCard shopCard = go.GetComponent<ShopCard>();
            WeaponData weaponData = weaponDatabase.GetWeaponData(data.itemID);
            shopCard.SetData(weaponData, data);
            cardList.Add(shopCard);
        }
    }
}
