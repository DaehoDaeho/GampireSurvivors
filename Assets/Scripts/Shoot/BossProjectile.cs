using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 3.0f;

    [SerializeField]
    private float lifetime = 3.0f;

    [SerializeField]
    private float damage = 60.0f;

    private Vector2 direction;

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);
    }

    void OnEnable()
    {
        Destroy(gameObject, lifetime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") == false)
        {
            return;
        }

        // 1. GameManager의 PlayerObj를 사용.
        GameManager.Instance.playerObj.TakeDamage(damage);

        Destroy(gameObject);
    }
}
