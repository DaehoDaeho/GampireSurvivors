using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public enum BossPhase
{
    Phase1,
    Phase2,
    Phase3
}

public class BossEnemy : Enemy
{
    [SerializeField]
    private BossPhase currentPhase = BossPhase.Phase1;

    [SerializeField]
    private float[] phaseHealthRatio;

    [SerializeField]
    private Color[] colorOnPhase;

    [SerializeField]
    private BossShooter bossShooter;

    [SerializeField]
    private float dashDamage = 90.0f;

    private bool isAttacking = false;
    private Coroutine coroutine;

    private bool shootAttack = true;
    
    void Start()
    {
        spriteRenderer.color = colorOnPhase[(int)currentPhase];
    }

    protected override void Update()
    {
        if(isAttacking == true)
        {
            return;
        }

        attackTimer += Time.deltaTime;
        if(attackTimer >= attackInterval)
        {
            float distance = Vector2.Distance(transform.position, GameManager.Instance.player.transform.position);
            if (distance <= enemyController.GetAttackRange())
            {
                if (coroutine != null)
                {
                    StopCoroutine(coroutine);
                }

                coroutine = StartCoroutine(BossAttack());
            }
            else
            {
                attackTimer = 0.0f;
            }
        }
    }

    IEnumerator BossAttack()
    {
        isAttacking = true;

        // 기를 모으는 기능.
        yield return StartCoroutine(ChargeEnergy());

        if(shootAttack == true)
        {
            yield return StartCoroutine(ShootProjectils());
        }
        else
        {
            yield return StartCoroutine(Dash());
        }
    }

    IEnumerator ChargeEnergy()
    {
        Vector3 originScale = transform.localScale;
        float timer = 0.0f;
        float chargeTime = 2.0f;

        while(timer < chargeTime)
        {
            timer += Time.deltaTime;
            spriteRenderer.color = Color.Lerp(Color.white, Color.yellow, Mathf.PingPong(timer * 15.0f, 1.0f));
            transform.localScale = originScale * (1.0f + Mathf.Sin(timer * 20.0f) * 0.1f);  // 몸을 떨리게 만드는 공식.
            yield return null;
        }

        spriteRenderer.color = originSpriteColor;
        transform.localScale = originScale;
    }

    IEnumerator ShootProjectils()
    {
        if(bossShooter != null)
        {
            bossShooter.Shoot();
        }

        yield return new WaitForSeconds(1.0f);

        isAttacking = false;
        attackTimer = 0.0f;
        shootAttack = !shootAttack;
    }

    IEnumerator Dash()
    {
        Vector2 targetDir = (GameManager.Instance.player.transform.position - transform.position).normalized;
        float timer = 0.0f;
        float dashTime = 0.5f;
        float dashSpeed = 25.0f;

        while(timer < dashTime)
        {
            timer += Time.deltaTime;

            transform.position += (Vector3)targetDir * dashSpeed * Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(1.0f);

        isAttacking = false;
        attackTimer = 0.0f;
        shootAttack = !shootAttack;
    }

    protected override void OnEnable()
    {
        base.OnEnable();

        UIManager.Instance.SetBossHPVisible(true);
        UIManager.Instance.UpdateBossHP(1.0f);
    }

    public override void TakeDamage(float damageAmount)
    {
        base.TakeDamage(damageAmount);

        if(currentHealth <= 0)
        {
            UIManager.Instance.UpdateBossHP(0.0f);
        }
        else
        {
            UIManager.Instance.UpdateBossHP(currentHealth / maxHealth);
        }

        if(isDead == true)
        {
            UIManager.Instance.SetBossHPVisible(false);
            return;
        }

        switch(currentPhase)
        {
            case BossPhase.Phase1:
                {
                    // 페이즈 전환 체크. HP가 현재 페이즈에 지정된 % 이하면 다음 페이즈로 전환.
                    if ((currentHealth / maxHealth) <= phaseHealthRatio[(int)currentPhase])
                    {
                        ChangePhase(BossPhase.Phase2);
                    }
                }
                break;

            case BossPhase.Phase2:
                {
                    // 페이즈 전환 체크. HP가 현재 페이즈에 지정된 % 이하면 다음 페이즈로 전환.
                    if ((currentHealth / maxHealth) <= phaseHealthRatio[(int)currentPhase])
                    {
                        ChangePhase(BossPhase.Phase3);
                    }
                }
                break;
        }
    }

    protected override void Die()
    {
        isDead = true;

        gameObject.SetActive(false);
    }

    void ChangePhase(BossPhase nextPhase)
    {
        currentPhase = nextPhase;
        moveSpeed *= 1.5f;
        originMoveSpeed = moveSpeed;

        if(spriteRenderer != null)
        {
            spriteRenderer.color = colorOnPhase[(int)currentPhase];
            originSpriteColor = spriteRenderer.color;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") == true)
        {
            // 보스가 공격 중일 때만 플레이어에게 데미지 적용.
            if(isAttacking == true)
            {
                Player player = collision.GetComponent<Player>();
                if (player != null)
                {
                    player.TakeDamage(dashDamage);
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, enemyController.GetAttackRange());
    }
}
