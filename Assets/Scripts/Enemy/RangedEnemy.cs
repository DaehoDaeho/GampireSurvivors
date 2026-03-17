using UnityEngine;

public class RangedEnemy : Enemy
{
    [SerializeField]
    private GameObject projectilePrefab;

    [SerializeField]
    private float shotInterval = 2.5f;

    [SerializeField]
    private EnemyController enemyController;

    private float shotTimer;

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
        shotTimer += Time.deltaTime;
        if(shotTimer >= shotInterval)
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

                shotTimer = 0.0f;
            }
        }
    }
}
