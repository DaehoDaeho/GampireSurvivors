using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    public static LobbyManager instance;

    [SerializeField]
    private StageScrollview stageScrollview;

    [SerializeField]
    private UIShop uiShop;

    [SerializeField]
    private UIInventory uiInventory;

    [SerializeField]
    private UIBuyPopup uiBuyPopup;

    [SerializeField]
    private UIMessagePopup uiMessagePopup;

    private ItemShopData selectedShopData;
    private WeaponData selectedWeaponData;

    private void Awake()
    {
        instance = this;

        DataManager.Load();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stageScrollview.SetSelectStagePanelVisible(false);
        uiShop.SetShopVisible(false);
    }

    public void OnClickBattle()
    {
        stageScrollview.SetSelectStagePanelVisible(true);
        stageScrollview.SetSelectStagePanelData();
    }

    public void OnClickShop()
    {
        uiShop.SetShopVisible(true);
        uiShop.SetShopData();
        uiShop.UpdateGoldText();
    }

    public void OnClickExit()
    {
        // Application.Quit() - 프로그램을 종료하는 함수.
        // 빌드에서만 작동.
        // 실행환경이 유니티 엔진인지 빌드인지 체크해서 각각 다르게 종료처리를 해야한다.
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void SetSelectedShopData(ItemShopData shopData, WeaponData weaponData)
    {
        selectedShopData = shopData;
        selectedWeaponData = weaponData;

        uiBuyPopup.SetBuyPopupVisible(true);
        uiBuyPopup.SetBuyMessage(selectedWeaponData, shopData.price);
    }

    public void OpenMessagePopup()
    {
        uiMessagePopup.SetMessagePopup(true);
    }

    public void SetMessage(string message)
    {
        uiMessagePopup.SetMessage(message);
    }

    public void ProcessBuyItem()
    {
        MyItemInfo info = new MyItemInfo();
        info.id = selectedWeaponData.id;
        info.invenIndex = DataManager.currentData.items.Count;
        DataManager.AddItem(info);
        DataManager.SubtractGold(selectedShopData.price);
        uiShop.UpdateGoldText();
    }

    public void OnClickInventory()
    {
        uiInventory.SetInventoryVisible(true);
        uiInventory.SetInventoryData();
    }
}
