using System.Collections;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private int projectildID;

    [SerializeField]
    private float moveSpeed;

    [SerializeField]
    private float damage;

    private Vector2 moveDirection;

    // Update is called once per frame
    void Update()
    {
        // 매 프레임마다 정해진 방향으로 날아간다.
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime, Space.Self);
    }

    public void Setup(Vector3 targetPosition)
    {
        if(GameManager.Instance != null)
        {
            ProjectileData projectileData = GameManager.Instance.GetProjectileData(projectildID);
            if(projectileData != null)
            {
                moveSpeed = projectileData.moveSpeed;
                damage = projectileData.damage;
            }
            else
            {
                moveSpeed = 10.0f;
                damage = 10.0f;
            }
        }

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
        if(collision.CompareTag("Enemy") == true)
        {
            BaseUnit hitUnit = collision.GetComponent<BaseUnit>();
            if(hitUnit != null)
            {
                hitUnit.TakeDamage(damage);
            }
        }

        //Destroy(gameObject);

        // 예약한 Invoke를 취소하는 함수.
        CancelInvoke("ReturnToPool");
        //StopCoroutine("ReturnToQueue");
        ReturnToPool();
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
