using UnityEngine;

public class DerivedUnit : BaseUnit
{
    [SerializeField]
    protected float attackPower;

    protected override void Awake()
    {
        base.Awake();
        attackPower = 50.0f;
        Debug.Log("DerivedUnit의 Awake 호출!!");
    }

    public override void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount * 2.0f;

        Debug.Log("DerivedUnit의 TakeDamage 호출!!");

        if (currentHealth <= 0.0f)
        {
            Die();
        }
    }

    protected override void Die()
    {
        Debug.Log("Derived의 Die 호출!!");

        Destroy(gameObject);
    }
}
