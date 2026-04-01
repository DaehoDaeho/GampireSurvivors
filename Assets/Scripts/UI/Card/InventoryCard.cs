using UnityEngine;
using TMPro;

public class InventoryCard : MonoBehaviour
{
    [SerializeField]
    private TMP_Text textName;

    [SerializeField]
    private GameObject equipText;

    WeaponData itemInfo;
    int invenIndex;

    public void SetData(WeaponData info, int index)
    {
        itemInfo = info;
        invenIndex = index;

        textName.text = itemInfo.name;

        RefreshEquipState();
    }

    public void RefreshEquipState()
    {
        if (IsEquipped() == true)
        {
            equipText.SetActive(true);
        }
        else
        {
            equipText.SetActive(false);
        }
    }

    bool IsEquipped()
    {
        if(CompareWeaponID() == true && CompareInvenIndex() == true)
        {
            return true;
        }

        return false;
    }

    bool CompareWeaponID()
    {
        if(itemInfo.id == DataManager.currentData.equippedWeapon.id)
        {
            return true;
        }

        return false;
    }

    bool CompareInvenIndex()
    {
        if(invenIndex == DataManager.currentData.equippedWeapon.invenIndex)
        {
            return true;
        }

        return false;
    }

    public void OnClickCard()
    {
        // 장착 기능을 추가.
        LobbyManager.instance.SetSelectedInvenItemInfo(itemInfo.id, invenIndex);
    }
}
