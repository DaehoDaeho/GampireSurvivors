using UnityEngine;

/// <summary>
/// 게임의 전반적인 상태(점수, 시간, 종료 여부)를 한 곳에서 관리하는 관리자 클래스.
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] private int gameScore;
    [SerializeField] private float playTime;
    [SerializeField] private bool isGameOver;
    [SerializeField] private int targetScore;

    public static GameManager Instance;

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
        if (isGameOver == false)
        {
            UpdateGameTime();
        }

        if (Input.GetKeyDown(KeyCode.O) == true)
        {
            UIManager.Instance.OpenUI(UIType.GameOver);
        }

        if (Input.GetKeyDown(KeyCode.P) == true)
        {
            UIManager.Instance.CloseUI(UIType.GameOver);
        }
    }

    void UpdateGameTime()
    {
        playTime += Time.deltaTime; // 이전 프레임에서 현재 프레임까지 오는데 걸린 시간.
                                    // 환경이 달라도 동일한 시간을 측정하게 해주는 표준 기능.
                                    // 프레임에 상관없이 일정한 속도로 움직이거나 시간을 처리할 때 사용.

        if(gameScore >= targetScore)
        {
            EndGame();
        }
    }

    public void AddScore(int amount)
    {
        gameScore += amount;
    }

    void EndGame()
    {
        isGameOver = true;
    }
}
