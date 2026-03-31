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
    }

    public void OnClickCard()
    {
        // 장착 기능을 추가.
    }
}
