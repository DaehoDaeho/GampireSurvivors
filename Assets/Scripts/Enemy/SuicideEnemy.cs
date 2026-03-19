using System.Collections;
using UnityEngine;

public class SuicideEnemy : Enemy
{
    [SerializeField]
    private float explosionRadius = 3.5f;

    [SerializeField]
    private float explosionDamage = 30.0f;

    [SerializeField]
    private float distRange = 2.5f;

    [SerializeField]
    private float scaleTime = 1.5f;

    private bool isExploding = false;

    protected override void Update()
    {
        // 플레이어와의 거리를 계산.
        float dist = Vector2.Distance(transform.position, GameManager.Instance.player.transform.position);

        if(dist < distRange)
        {
            // 폭발 처리.
            StartCoroutine(Explode());
        }
    }

    IEnumerator Explode()
    {
        isExploding = true;
        moveSpeed = 0.0f;

        float timer = 0.0f;
        Vector3 originScale = transform.localScale;

        // 1.5초간 부풀어 오르도록.
        while(timer < scaleTime)
        {
            timer += Time.deltaTime;
            transform.localScale = originScale * (1.0f + (timer / scaleTime) * 0.8f);
            yield return null;
        }

        // 폭발 반경 체크 및 대미지 적용.
        Collider2D[] hit = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        
        //for(int i=0; i<hit.Length; ++i)
        //{
        //    Collider2D target = hit[i];
        //}

        foreach(Collider2D target in hit)
        {
            if(target.CompareTag("Player") == true)
            {
                Player player = target.GetComponent<Player>();
                if(player != null)
                {
                    player.TakeDamage(explosionDamage);
                }
            }
        }

        transform.localScale = originScale;

        GameObject obj = PoolManager.instance.GetObject(PoolID.ExplosionEffect);
        if(obj != null)
        {
            obj.transform.position = transform.position;
        }

        AudioManager.instance.PlaySFX(AudioType.Explode);

        Die();
    }
}
