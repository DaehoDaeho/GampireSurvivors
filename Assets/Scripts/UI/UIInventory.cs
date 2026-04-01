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
    private GameObject equipButton;

    [SerializeField]
    private WeaponDatabase weaponDatabase;

    private List<InventoryCard> cards = new List<InventoryCard>();

    private MyItemInfo selectedItemInfo = new MyItemInfo();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetInventoryVisible(false);
        SetEquipButtonVisible(false);
    }

    public void SetInventoryVisible(bool visible)
    {
        invenPanel.SetActive(visible);
    }

    public void SetEquipButtonVisible(bool visible)
    {
        equipButton.SetActive(visible);
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

    public void SetSelectedInvenItemInfo(int id, int index)
    {
        selectedItemInfo.id = id;
        selectedItemInfo.invenIndex = index;
    }

    public MyItemInfo GetSelectedItemInfo()
    {
        return selectedItemInfo;
    }

    public void OnClickExit()
    {
        SetInventoryVisible(false);
        SetEquipButtonVisible(false);
    }

    public void OnClickEquip()
    {
        DataManager.ChangeEquippedWeapon(selectedItemInfo);
        RefreshEquipState();
    }

    void RefreshEquipState()
    {
        foreach(InventoryCard card in cards)
        {
            card.RefreshEquipState();
        }
    }
}
