using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopCard : MonoBehaviour
{
    [SerializeField]
    private TMP_Text textName;

    [SerializeField]
    private TMP_Text textDesc;

    [SerializeField]
    private TMP_Text textPrice;

    [SerializeField]
    private Image imageThumbnail;

    private WeaponData data;
    private ItemShopData shopData;
    private int price;

    public void SetData(WeaponData weaponData, ItemShopData itemShopData)
    {
        data = weaponData;
        shopData = itemShopData;
        price = shopData.price;

        textName.text = data.name;
        textDesc.text = data.desc;
        imageThumbnail.sprite = weaponData.thumbnail;
        textPrice.text = price.ToString();
    }

    public void OnClickBuy()
    {
        LobbyManager.instance.SetSelectedShopData(shopData, data);
    }
}
