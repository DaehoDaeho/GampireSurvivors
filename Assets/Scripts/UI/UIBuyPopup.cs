using UnityEngine;
using TMPro;

public class UIBuyPopup : MonoBehaviour
{
    [SerializeField]
    private GameObject panelBuyPopup;

    [SerializeField]
    private TMP_Text textMessage;

    private int itemPrice = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetBuyPopupVisible(false);
    }

    public void SetBuyPopupVisible(bool visible)
    {
        panelBuyPopup.SetActive(visible);
    }

    public void SetBuyMessage(WeaponData weaponData, int price)
    {
        itemPrice = price;
        textMessage.text = weaponData.name + "을(를) " + price.ToString() + "에 구입하시겠습니까?";
    }

    public void OnClickOK()
    {
        // 구매 처리를 하기 전에 충분한 돈을 가지고 있는지 체크해서 돈이 부족하면 메시지 팝업 출력.
        if(DataManager.GetGold() < itemPrice)
        {
            // 재화 부족 메시지를 출력하는 팝업 출력.
            LobbyManager.instance.OpenMessagePopup();
            LobbyManager.instance.SetMessage("돈 없어!!!");
            SetBuyPopupVisible(false);
        }
        else
        {
            // 구매 처리. 구매 완료 팝업 출력 필요.
            LobbyManager.instance.ProcessBuyItem();
            LobbyManager.instance.OpenMessagePopup();
            LobbyManager.instance.SetMessage("구매 완료!!!");
            SetBuyPopupVisible(false);
        }
    }

    public void OnClickCancel()
    {
        SetBuyPopupVisible(false);
    }
}
