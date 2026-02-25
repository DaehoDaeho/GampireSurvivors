using UnityEngine;

public class AutoShooter : MonoBehaviour
{
    [SerializeField]
    private GameObject projectilePrefab;

    [SerializeField]
    private float attackInterval = 0.5f;

    [SerializeField]
    private float attackRange = 8.0f;

    private float attackTimer = 0.0f;

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

    void FireProjectile(Transform target)
    {
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        if(projectile != null)
        {
            Projectile proj = projectile.GetComponent<Projectile>();
            if(proj != null)
            {
                proj.Setup(target.position);
            }
            else
            {
                Destroy(projectile);
            }
        }
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
}
