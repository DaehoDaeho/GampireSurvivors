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

    void Start()
    {
        spriteRenderer.color = colorOnPhase[(int)currentPhase];
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
}
