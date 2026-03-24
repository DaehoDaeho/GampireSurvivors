using UnityEngine;

public class LobbyManager : MonoBehaviour
{
    public static LobbyManager instance;

    [SerializeField]
    private StageScrollview stageScrollview;

    private void Awake()
    {
        instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        stageScrollview.SetSelectStagePanelVisible(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnClickBattle()
    {
        stageScrollview.SetSelectStagePanelVisible(true);
        stageScrollview.SetSelectStagePanelData();
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
}
