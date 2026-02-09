using UnityEngine;

public class BaseUnit : MonoBehaviour
{
    [SerializeField]
    private string unitName;

    [SerializeField]
    private float maxHealth;

    [SerializeField]
    private float currentHealth;

    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private bool isDead;

    private void Awake()
    {
        currentHealth = maxHealth;
        isDead = false;
    }

    public void TakeDamage(float damageAmount)
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
    }

    void Die()
    {
        isDead = true;

        // 추후 오브젝트 풀링을 추가할 것에 대비해 파괴하지 않고 비활성화만 시켜둠.
        gameObject.SetActive(false);
    }
}
