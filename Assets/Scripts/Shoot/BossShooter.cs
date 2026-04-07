using UnityEngine;

public class BossShooter : MonoBehaviour
{
    [SerializeField]
    private GameObject projectilePrefab;

    [SerializeField]
    private int projectileCount = 12;

    public void Shoot()
    {
        float angleStep = 360.0f / projectileCount;
        float angle = 0.0f;

        for(int i=0; i<projectileCount; ++i)
        {
            float x = Mathf.Cos(angle) * Mathf.Deg2Rad;
            float y = Mathf.Sin(angle) * Mathf.Deg2Rad;

            Vector2 projectileDirection = new Vector2(x, y);

            GameObject go = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
            BossProjectile bossProjectile = go.GetComponent<BossProjectile>();
            bossProjectile.SetDirection(projectileDirection);

            // 다음 총알의 각도 계산.
            angle += angleStep;
        }
    }
}
