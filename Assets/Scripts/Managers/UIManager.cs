using UnityEngine;

public enum UIType
{
    GameOver = 0
}

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    private UIBase currentOpenedUI = null;

    [SerializeField]
    private UIBase[] uis;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenUI(UIType type)
    {
        uis[(int)type].OpenUI();
        currentOpenedUI = uis[(int)type];
    }

    public void CloseUI(UIType type)
    {
        if(currentOpenedUI != null && currentOpenedUI.IsOpened() == true)
        {
            currentOpenedUI.CloseUI();
            currentOpenedUI = null;
        }
    }
}
