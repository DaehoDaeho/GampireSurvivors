using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;

    private Animator animator;

    private Rigidbody2D rigidBody;

    private SpriteRenderer spriteRenderer;

    public Vector2 inputVec;   // 입력받은 방향을 저장할 변수.

    private void Awake()
    {
        moveSpeed = 5.0f;
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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

    void LateUpdate()
    {
        // 방향 전환.
        if (inputVec.x != 0.0f)
        {
            spriteRenderer.flipX = inputVec.x < 0.0f;   // 조건이 만족하면 true, 아니면 false를 대입.
        }

        if(animator != null)
        {
            // 벡터의 길이의 제곱.
            // inputVec.magnitude ; 벡터의 정확한 길이. 반드시 길이가 필요한 경우에만 쓴다.
            float move = inputVec.sqrMagnitude; // 벡터의 크기의 제곱을 알아오는 코드.
                                                // '입력 유무'나 '크기 비교'를 할 때 사용.

            animator.SetFloat("Speed", move);
        }
    }
}
