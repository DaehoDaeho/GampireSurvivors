using UnityEngine;

public class Enemy : BaseUnit
{
    [SerializeField]
    private int dropExp;

    protected override void Awake()
    {
        base.Awake();

        dropExp = 10;
        unitName = "Slime";
    }

    public override void TakeDamage(float damageAmount)
    {
        base.TakeDamage(damageAmount);

        if(isDead == true)
        {
            return;
        }

        Debug.Log(unitName + "가(이) 공격받았습니다. 남은 체력 : " + currentHealth);
    }

    protected override void Die()
    {
        base.Die();

        // 경험치를 드랍하거나, 플레이어의 경험치를 올려주는 처리를 추후에 한다.
        Debug.Log(unitName + "가(이) 사망!! 경험치 : " + dropExp);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) == true)
        {
            TakeDamage(3);
        }
    }
}
