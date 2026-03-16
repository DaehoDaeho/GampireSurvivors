using System.Collections;
using UnityEngine;

public enum ProjectileType
{
    Normal = 0,
    Slow = 1,
    Burn = 2,
    Freeze = 3
}

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private int projectildID;

    [SerializeField]
    private ProjectileType projectileType;

    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private float damage;

    [SerializeField]
    private bool applyNormalDamage = true;

    [SerializeField]
    private bool useObjectPooling = true;

    [SerializeField]
    private string ignoreTarget;

    [SerializeField]
    private string hitTarget;

    private Vector2 moveDirection;

    // Update is called once per frame
    void Update()
    {
        // 매 프레임마다 정해진 방향으로 날아간다.
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime, Space.Self);
    }

    public int GetID()
    {
        return projectildID;
    }

    public void Setup(Vector3 targetPosition, float speed, float damage)
    {
        moveSpeed = speed;
        this.damage = damage;

        Vector2 direction = targetPosition - transform.position;
        moveDirection = direction.normalized;

        // 날아가는 방향으로 투사체의 머리를 회전시킨다.(아크탄젠트 수학 공식 이용)
        // Mathf.Rad2Deg : Radian -> Degree 값으로 변환.
        // (Degree) 0 ~ 360 -> (Radian) -180 ~ 180
        float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        //Destroy(gameObject, 3.0f);

        // 지정한 시간이 지난 후 문자열로 지정된 이름의 함수를 호출하도록 예약.
        Invoke("ReturnToPool", 3.0f);
        //StartCoroutine("ReturnToQueue", 3.0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(ignoreTarget) == true)
        {
            return;
        }

        if(applyNormalDamage == true)
        {
            if (collision.CompareTag(hitTarget) == true)
            {
                BaseUnit hitUnit = collision.GetComponent<BaseUnit>();
                if (hitUnit != null)
                {
                    hitUnit.TakeDamage(damage);
                }

                ApplyStatusEffect(collision);
            }
        }
        else
        {
            if (collision.CompareTag(hitTarget) == true)
            {
                if(projectileType == ProjectileType.Normal)
                {
                    BaseUnit hitUnit = collision.GetComponent<BaseUnit>();
                    if (hitUnit != null)
                    {
                        hitUnit.TakeDamage(damage);
                    }
                }
                else
                {
                    ApplyStatusEffect(collision);
                }
            }
        }
        

        //Destroy(gameObject);

        // 예약한 Invoke를 취소하는 함수.
        CancelInvoke("ReturnToPool");
        //StopCoroutine("ReturnToQueue");

        if(useObjectPooling == true)
        {
            ReturnToPool();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void ApplyStatusEffect(Collider2D collision)
    {
        BossEnemy boss = collision.GetComponent<BossEnemy>();
        if(boss != null)
        {
            return;
        }

        switch (projectileType)
        {
            case ProjectileType.Slow:
                {
                    Enemy enemy = collision.GetComponent<Enemy>();
                    if (enemy != null)
                    {
                        enemy.ApplySlow(3.0f, 0.5f);
                    }
                }
                break;

            case ProjectileType.Burn:
                {
                    Enemy enemy = collision.GetComponent<Enemy>();
                    if (enemy != null)
                    {
                        enemy.ApplyBurn(3.0f, 10.0f);
                    }
                }
                break;

            case ProjectileType.Freeze:
                {
                    Enemy enemy = collision.GetComponent<Enemy>();
                    if (enemy != null)
                    {
                        enemy.ApplyFreeze(3.0f);
                    }
                }
                break;
        }
    }

    void ReturnToPool()
    {
        if(PoolManager.instance != null)
        {
            PoolManager.instance.ReturnProjectile(gameObject);
        }
    }

    IEnumerator ReturnToQueue(float time)
    {
        yield return new WaitForSeconds(time);

        ReturnToPool();
    }
}
