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
        //Init();
        Debug.Log("BaseUnit의 Awake 호출!!");
    }

    public void Init()
    {
        currentHealth = maxHealth;
        isDead = false;
    }

    protected virtual void OnEnable()
    {
        Init();
    }

    /// <summary>
    /// 대미지 적용.
    /// </summary>
    /// <param name="damageAmount">대미지 양</param>
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

        /* 추후 오브젝트 풀링을 추가할 것에 대비해
        파괴하지 않고 비활성화만 시켜둠. */
        gameObject.SetActive(false);

        Debug.Log("BaseUnit의 Die 호출!!");
    }

    /// <summary>
    /// 이름을 반환하는 함수.
    /// </summary>
    /// <returns>이름</returns>
    public string GetName()
    {
        return unitName;
    }

    public float GetMoveSpeed()
    {
        return moveSpeed;
    }

    public bool GetIsDead()
    {
        return isDead;
    }
}
