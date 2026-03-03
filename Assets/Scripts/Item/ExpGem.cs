using UnityEditor.Rendering;
using UnityEngine;

public class ExpGem : MonoBehaviour
{
    [SerializeField]
    private int expAmount = 10;

    [SerializeField]
    private float magneticRange = 3.0f;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    private float currentSpeed = 0.0f;
    private bool isMagnetic = false;
    private Transform targetPlayer;

    void OnEnable()
    {
        currentSpeed = 0.0f;
        isMagnetic = false;

        if(GameManager.Instance != null && GameManager.Instance.player != null)
        {
            targetPlayer = GameManager.Instance.player.transform;
        }
    }

    public void SetExpData(int amount)
    {
        expAmount = amount;
        if (expAmount == 10)
        {
            spriteRenderer.color = Color.yellow;
        }
        else if (expAmount == 20)
        {
            spriteRenderer.color = new Color(1.0f, 1.0f, 0.0f, 1.0f);
        }
        else if (expAmount == 30)
        {
            spriteRenderer.color = Color.red;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(targetPlayer != null)
        {
            if(isMagnetic == false)
            {
                // 플레이어와 거리 체크.
                CheckMagneticRange();
            }
            else
            {
                // 플레이어에게 이동 처리.
                MoveTowardPlayer();
            }
        }
    }

    void CheckMagneticRange()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, targetPlayer.position);

        if(distanceToPlayer <= magneticRange)
        {
            isMagnetic = true;
        }
    }

    void MoveTowardPlayer()
    {
        // 가속도 로직.
        // 10.0f을 변수로 빼서 수정이 가능하도록 개선해도 됨.
        currentSpeed += (10.0f * Time.deltaTime);
        transform.position = Vector3.MoveTowards(transform.position, targetPlayer.position, currentSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") == true)
        {
            Player player = collision.GetComponent<Player>();

            if (player != null)
            {
                player.AddExperience(expAmount);
            }

            PoolManager.instance.ReturnExpGem(gameObject);
        }
    }
}
