using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    [SerializeField]
    protected string unitName;

    [SerializeField]
    protected float maxHealth;

    [SerializeField]
    protected float currentHealth;

    [SerializeField]
    protected float moveSpeed;

    [SerializeField]
    protected bool isDead;

    protected virtual void Awake()
    {
        currentHealth = maxHealth;
        isDead = false;
        Debug.Log("BaseUnit의 Awake 호출!!");
    }

    public virtual void TakeDamage(float damageAmount)
    {
        if (isDead == true)
        {
            return;
        }

        currentHealth -= damageAmount;

        if (currentHealth <= 0.0f)
        {
            Die();
        }

        Debug.Log("BaseUnit의 TakeDamage 호출!!");
    }

    protected virtual void Die()
    {
        isDead = true;

        // 추후 오브젝트 풀링을 추가할 것에 대비해 파괴하지 않고 비활성화만 시켜둠.
        gameObject.SetActive(false);

        Debug.Log("BaseUnit의 Die 호출!!");
    }
}
