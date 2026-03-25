using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AutoShooter : MonoBehaviour
{
    [SerializeField]
    private GameObject projectilePrefab;

    [SerializeField]
    private GameObject[] statusEffectProjectilePrefab;

    [SerializeField]
    private float attackInterval = 0.5f;

    [SerializeField]
    private float attackRange = 8.0f;

    private float attackTimer = 0.0f;

    [SerializeField]
    private float upgradeDamage = 0.0f;

    [SerializeField]
    private float upgradeMoveSpeed = 0.0f;

    [SerializeField]
    private int projectileCount = 1;

    [SerializeField]
    private float spreadAngle = 60.0f;

    [SerializeField]
    private float aimDistance = 10.0f;

    private int shootCount = 0;

    // Update is called once per frame
    void Update()
    {
        attackTimer += Time.deltaTime;
        if(attackTimer >= attackInterval)
        {
            FindAndAttack();
            attackTimer = 0.0f;
        }
    }

    void FireSpreadProjectiles(Transform target, bool useNormalProjectile)
    {
        if(projectileCount <= 0)
        {
            return;
        }

        if(projectileCount == 1)
        {
            // 총알 발사 코드.
            Vector2 targetPosition = GetSpreadTargetPosition(target, 0.0f);

            if(useNormalProjectile == true)
            {
                SpawnNormalProjectile(targetPosition);
            }
            else
            {
                SpawnStatusEffectProjectile(targetPosition);
            }
        }
        else
        {
            // 여러 발의 총알 발사 코드.
            float angleStep = spreadAngle / (projectileCount - 1);  // 간격 각도 계산.
            float startAngle = -(spreadAngle * 0.5f);

            for(int i=0; i<projectileCount; ++i)
            {
                float currentAngle = startAngle + (angleStep * i);
                Vector2 targetPosition = GetSpreadTargetPosition(target, currentAngle);

                if (useNormalProjectile == true)
                {
                    SpawnNormalProjectile(targetPosition);
                }
                else
                {
                    SpawnStatusEffectProjectile(targetPosition);
                }
            }
        }
    }

    void SpawnNormalProjectile(Vector2 targetPosition)
    {
        GameObject projectile = PoolManager.instance.GetObject(PoolID.Projectile);
        if (projectile != null)
        {
            projectile.transform.position = transform.position;
            projectile.transform.rotation = Quaternion.identity;

            Projectile proj = projectile.GetComponent<Projectile>();
            if (proj != null)
            {
                ProjectileData projectileData = GameManager.Instance.GetProjectileData(proj.GetID());
                float damage = projectileData.damage + upgradeDamage;
                float moveSpeed = projectileData.moveSpeed + upgradeMoveSpeed;
                proj.Setup(targetPosition, moveSpeed, damage);
            }
            else
            {
                Destroy(projectile);
            }
        }
    }

    void SpawnStatusEffectProjectile(Vector2 targetPosition)
    {
        int index = Random.Range(0, statusEffectProjectilePrefab.Length);
        GameObject projectile = Instantiate(statusEffectProjectilePrefab[index], transform.position, Quaternion.identity);
        Projectile proj = projectile.GetComponent<Projectile>();
        if (proj != null)
        {
            ProjectileData projectileData = GameManager.Instance.GetProjectileData(proj.GetID());
            float damage = projectileData.damage + upgradeDamage;
            float moveSpeed = projectileData.moveSpeed + upgradeMoveSpeed;
            proj.Setup(targetPosition, moveSpeed, damage);
        }
    }

    /// <summary>
    /// 기준 적 방향을 중심으로 특정 각도만큼 회전된 방향을 계산하고,
    /// 그 방향으로 멀리 떨어진 가상의 목표 지점을 반환한다.
    /// </summary>
    /// <param name="target">중심 조준 기준 적</param>
    /// <param name="angleOffset">기준 방향에서 추가로 회전할 각도</param>
    /// <returns>회전된 방향을 반영한 월드 좌표 콕표 지점</returns>
    Vector2 GetSpreadTargetPosition(Transform target, float angleOffset)
    {
        Vector2 baseDirection = (Vector2)(target.position - transform.position).normalized;

        // z 축 기준 회전 값을 생성.
        Quaternion rotation = Quaternion.Euler(0.0f, 0.0f, angleOffset);
        Vector2 spreadDirection = rotation * baseDirection;

        Vector2 targetPosition = (Vector2)transform.position + (spreadDirection * aimDistance);
        return targetPosition;
    }

    void FireProjectile(Transform target)
    {
        if(PoolManager.instance == null)
        {
            return;
        }

        bool useNormalProjectile = true;

        //GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        if(shootCount == 0 || shootCount % 5 != 0)
        {
            shootCount++;            
        }
        else
        {
            shootCount++;
            useNormalProjectile = false;
        }

        FireSpreadProjectiles(target, useNormalProjectile);
    }

    public void SetUpgradeMoveSpeed(float speed)
    {
        upgradeMoveSpeed += speed;
    }

    public void SetUpgradeDamage(float damage)
    {
        upgradeDamage += damage;
    }

    void FindAndAttack()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, attackRange);

        // Mathf.Infinity : 무한대의 값.
        float minDistance = Mathf.Infinity;
        Transform closestTarget = null;

        for(int i=0; i<colliders.Length; ++i)
        {
            if (colliders[i].CompareTag("Enemy") == true)
            {
                float distance = Vector2.Distance(transform.position, colliders[i].transform.position);

                // 새로 측정한 거리가 기존의 거리보다 가까우면.
                if(distance < minDistance)
                {
                    // 기존의 거리 정보를 새 거리 정보로 갱신.
                    minDistance = distance;
                    closestTarget = colliders[i].transform;
                }
            }
        }

        if(closestTarget != null)
        {
            FireProjectile(closestTarget);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }

    public void AddProjectileCount(int count)
    {
        projectileCount += count;
        RefreshSpreadAngle();
    }

    void RefreshSpreadAngle()
    {
        spreadAngle = projectileCount * 10.0f;
    }
}
