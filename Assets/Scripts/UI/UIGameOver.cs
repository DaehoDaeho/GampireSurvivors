using UnityEngine;
using TMPro;

public class UIGameOver : UIBase
{
    [SerializeField]
    private TMP_Text textMessage;

    public override void OpenUI()
    {
        base.OpenUI();

        if(textMessage != null)
        {
            textMessage.text = "You Are Dead!!";
        }
    }

    public override void CloseUI()
    {
        base.CloseUI();

        if(textMessage != null)
        {
            // 빈 문자열로 채운다.
            textMessage.text = string.Empty;
        }
    }
}
