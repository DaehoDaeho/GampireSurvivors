using UnityEngine;

public class Player : BaseUnit
{
    [SerializeField]
    private int currentExp;

    protected override void Awake()
    {
        base.Awake();

        currentExp = 0;
        unitName = "Hero";
    }

    public override void TakeDamage(float damageAmount)
    {
        base.TakeDamage(damageAmount);

        // 이미 죽은 상태라면 추가 효과를 처리하지 않도록 한다.
        if(isDead == true)
        {
            return;
        }

        // 플레이어만의 피격 효과.
        Debug.Log("플레이어가 피해를 입었습니다!! 현재 체력 : " + currentHealth);
    }

    protected override void Die()
    {
        base.Die();

        if(GameManager.Instance != null)
        {
            // 게임 오버 등의 추가 처리를 추후에 한다.
            Debug.Log("게임 오버! 플레이어가 사망했습니다!");
        }
    }
}
