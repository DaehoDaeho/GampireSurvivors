using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    private Rigidbody2D rigidBody;

    private Vector2 inputVec;   // 입력받은 방향을 저장할 변수.

    private void Awake()
    {
        moveSpeed = 5.0f;
        rigidBody = GetComponent<Rigidbody2D>();
    }

    /// <summary>
    /// New Input System의 Move 액션으로부터 메시지를 받는 이벤트 함수.
    /// </summary>
    /// <param name="value">입력 정보를 담고 있는 클래스</param>
    void OnMove(InputValue value)
    {
        // 입력 값을 Vector2 타입으로 읽어와서 저장.
        inputVec = value.Get<Vector2>();
        Debug.Log("입력 방향 : " + inputVec);
    }

    private void FixedUpdate()
    {
        if(rigidBody != null)
        {
            // 다음에 이동할 방향 벡터 계산.
            // 입력값 * 이동 속도 * 고정 프레임 시간.
            Vector2 nextVec = inputVec * moveSpeed * Time.fixedDeltaTime;

            // 현재 위치에 이동량을 더해서 이동.
            rigidBody.MovePosition(rigidBody.position + nextVec);
        }
    }
}
