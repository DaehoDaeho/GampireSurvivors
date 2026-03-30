using UnityEngine;
using TMPro;

public class UIMessagePopup : MonoBehaviour
{
    [SerializeField]
    private GameObject panelMessagePopup;

    [SerializeField]
    private TMP_Text textMessage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SetMessagePopup(false);
    }

    public void SetMessagePopup(bool visible)
    {
        panelMessagePopup.SetActive(visible);
    }

    public void SetMessage(string message)
    {
        textMessage.text = message;
    }

    public void OnClickOK()
    {
        SetMessagePopup(false);
    }
}
