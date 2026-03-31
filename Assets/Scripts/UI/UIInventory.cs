using UnityEngine;
using System.Collections.Generic;

public class UIInventory : MonoBehaviour
{
    [SerializeField]
    private GameObject cardPrefab;

    [SerializeField]
    private Transform content;

    [SerializeField]
    private GameObject invenPanel;

    [SerializeField]
    private WeaponDatabase weaponDatabase;

    private List<InventoryCard> cards = new List<InventoryCard>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetInventoryVisible(false);
    }

    public void SetInventoryVisible(bool visible)
    {
        invenPanel.SetActive(visible);
    }

    public void SetInventoryData()
    {
        foreach(InventoryCard cardData in cards)
        {
            Destroy(cardData.gameObject);
        }

        cards.Clear();

        foreach(MyItemInfo info in DataManager.currentData.items)
        {
            GameObject go = Instantiate(cardPrefab, content);
            if(go != null)
            {
                InventoryCard card = go.GetComponent<InventoryCard>();
                if(card != null)
                {
                    WeaponData data = weaponDatabase.GetWeaponData(info.id);
                    card.SetData(data, info.invenIndex);
                    cards.Add(card);
                }
            }
        }
    }

    public void OnClickExit()
    {
        SetInventoryVisible(false);
    }
}
