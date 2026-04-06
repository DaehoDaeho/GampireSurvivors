using UnityEngine;

public class RangedEnemy : Enemy
{
    [SerializeField]
    private GameObject projectilePrefab;
    
    protected override void Update()
    {
        if(isDead == true)
        {
            return;
        }

        float distance = Vector2.Distance(transform.position, GameManager.Instance.player.transform.position);

        if(distance <= enemyController.GetAttackRange())
        {
            // 사격.
            Shoot();
        }
    }

    void Shoot()
    {
        attackTimer += Time.deltaTime;
        if(attackTimer >= attackInterval)
        {
            GameObject go = PoolManager.instance.GetObject(PoolID.EnemyProjectile);
            if(go != null)
            {
                go.transform.position = transform.position;

                Projectile projectile = go.GetComponent<Projectile>();
                if (projectile != null)
                {
                    projectile.Setup(GameManager.Instance.player.transform.position, 4.0f, 10.0f);
                }

                attackTimer = 0.0f;
            }
        }
    }
}
