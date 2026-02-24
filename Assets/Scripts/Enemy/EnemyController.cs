using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private Transform targetPlayer;

    [SerializeField]
    private Rigidbody2D rigidBody;

    [SerializeField]
    private Enemy enemyStat;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private float attackRange = 1.5f;

    [SerializeField]
    private bool isAttacking = false;

    [SerializeField]
    private Animator animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if(GameManager.Instance != null)
        {
            if(GameManager.Instance.player != null)
            {
                targetPlayer = GameManager.Instance.player.transform;
            }
        }
    }

    private void Update()
    {
        UpdateAnimation();
    }

    void UpdateAnimation()
    {
        if (animator != null)
        {
            animator.SetBool("Move", !isAttacking);
        }
    }

    /// <summary>
    /// 이동을 멈춘다.
    /// </summary>
    void StopMovement()
    {
        // 물리적인 속도를 0으로 만들어서 관성에 의해 미끄러지는 것을 방지한다.
        rigidBody.linearVelocity = Vector2.zero;

        // 추후 공격 관련 기능을 여기서 구현.
    }

    /// <summary>
    /// 플레이어를 향해 이동하는 함수.
    /// </summary>
    void MoveTowardPlayer()
    {
        // 방향 벡터 계산 (목표 위치 - 내 위치)
        Vector2 direction = targetPlayer.position - transform.position;

        // 벡터 정규화 (길이를 1로 만든다.)
        Vector2 normalizedDirection = direction.normalized;

        Vector2 nextPosition = rigidBody.position + (normalizedDirection * enemyStat.GetMoveSpeed() * Time.fixedDeltaTime);

        rigidBody.MovePosition(nextPosition);
    }

    /// <summary>
    /// 거리를 계산하고 이동할지 멈출지 결정하는 함수.
    /// </summary>
    void CheckDistanceAndAct()
    {
        // 플레이어와 적 사이의 실제 거리를 계산한다.
        float distanceToPlayer = Vector2.Distance(targetPlayer.position, transform.position);

        // 거리가 공격 사거리보다 멀면 플레이어에게 다가간다.
        if(distanceToPlayer > attackRange)
        {
            isAttacking = false;
            MoveTowardPlayer();
        }
        else if(distanceToPlayer <= attackRange)    // 거리가 공격 사거리 안으로 들어왔다면 공격을 준비한다.
        {
            isAttacking = true;
            StopMovement();
        }
    }

    /// <summary>
    /// 물리 기반 이동 및 상태 체크.
    /// </summary>
    private void FixedUpdate()
    {
        if(targetPlayer != null)
        {
            if(enemyStat.GetIsDead() == false)
            {
                CheckDistanceAndAct();
            }
        }
    }

    /// <summary>
    /// 이동 후 스프라이트의 방향 전환.
    /// </summary>
    private void LateUpdate()
    {
        if(targetPlayer != null)
        {
            if(targetPlayer.position.x < transform.position.x)
            {
                spriteRenderer.flipX = true;
            }
            else if(targetPlayer.position.x > transform.position.x)
            {
                spriteRenderer.flipX = false;
            }
        }
    }
}
