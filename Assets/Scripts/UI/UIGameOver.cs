using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class UIGameOver : UIBase
{
    [SerializeField]
    private TMP_Text textMessage;

    public override void OpenUI()
    {
        base.OpenUI();

        Time.timeScale = 0.0f;

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

    public void OnClickRestart()
    {
        Time.timeScale = 1.0f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnClickGotoLobby()
    {
        Time.timeScale = 1.0f;

        SceneManager.LoadScene("LobbyScene");
    }
}
