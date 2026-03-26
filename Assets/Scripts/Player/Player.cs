using System.Collections;
using UnityEngine;

public class Player : BaseUnit
{
    [SerializeField]
    private int currentLevel;

    [SerializeField]
    private int maxExp;

    [SerializeField]
    private int currentExp;

    [Header("대쉬 설정")]
    [SerializeField]
    float dashSpeed = 15.0f;

    [SerializeField]
    float dashDuration = 0.3f;

    [SerializeField]
    float dashCooldown = 1.0f;

    [SerializeField]
    float dashDamage = 30.0f;

    [SerializeField]
    float dashDamageRadius = 1.5f;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    [SerializeField]
    private GameObject playerGhostPrefab;

    [SerializeField]
    private float ghostCooldown = 0.05f;

    private bool isDashing = false;
    private float dashCooldownTimer = 0.0f;

    protected override void Awake()
    {
        base.Awake();

        Init();

        currentLevel = 1;
        maxExp = 100;
        currentExp = 0;
        unitName = "Hero";
    }

    void Start()
    {
        UIManager.Instance.UpdateExpBar(0.0f);
        UIManager.Instance.UpdateLevel(currentLevel);
    }

    void Update()
    {
        if(isDashing == true)
        {
            return;
        }

        dashCooldownTimer += Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.Space) == true && dashCooldownTimer >= dashCooldown)
        {
            StartCoroutine(DashRoutine());
        }
    }

    IEnumerator DashRoutine()
    {
        isDashing = true;
        dashCooldownTimer = 0.0f;

        float movex = Input.GetAxisRaw("Horizontal");
        float movey = Input.GetAxisRaw("Vertical");

        Vector2 dashDir = new Vector2(movex, movey).normalized;

        float timer = 0.0f;
        float trailTimer = 0.0f;

        while(timer < dashDuration)
        {
            timer += Time.deltaTime;

            transform.Translate(dashDir * dashSpeed * Time.deltaTime);

            // 데미지 처리.
            ApplyDashDamage();

            // 잔상 생성.
            trailTimer += Time.deltaTime;
            if(trailTimer >= ghostCooldown)
            {
                CreateTrail();
                trailTimer = 0.0f;
            }

            yield return null;
        }

        isDashing = false;
    }

    void ApplyDashDamage()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, dashDamageRadius);
        foreach(Collider2D hit in hits)
        {
            if(hit.CompareTag("Enemy") == true)
            {
                Enemy enemy = hit.GetComponent<Enemy>();
                if(enemy != null)
                {
                    enemy.TakeDamage(dashDamage);
                }
            }
        }
    }

    void CreateTrail()
    {
        GameObject obj = Instantiate(playerGhostPrefab);
        if(obj != null)
        {
            GhostTrail ghostTrail = obj.GetComponent<GhostTrail>();
            if(ghostTrail != null)
            {
                ghostTrail.Init(spriteRenderer.sprite, transform.position, transform.rotation,
                    transform.localScale, spriteRenderer.flipX);
            }
        }
    }

    public override void TakeDamage(float damageAmount)
    {
        if(isDashing == true)
        {
            return;
        }

        base.TakeDamage(damageAmount);

        // 이미 죽은 상태라면 추가 효과를 처리하지 않도록 한다.
        if(isDead == true)
        {
            return;
        }

        // 플레이어만의 피격 효과.
        Debug.Log("플레이어가 피해를 입었습니다!! 현재 체력 : " + currentHealth);
    }

    protected override void Die()
    {
        base.Die();

        if(GameManager.Instance != null)
        {
            // 게임 오버 등의 추가 처리를 추후에 한다.
            Debug.Log("게임 오버! 플레이어가 사망했습니다!");
        }
    }

    public void AddExperience(int expAmount)
    {
        currentExp += expAmount;

        if(currentExp >= maxExp)
        {
            LevelUp();
        }

        UIManager.Instance.UpdateExpBar((float)currentExp / (float)maxExp);
    }

    void LevelUp()
    {
        ++currentLevel;

        // 초과 경험치 이월 : 목표치를 빼고 남은 경험치를 다음 레벨 시작 경험치로 유지.
        currentExp -= maxExp;

        maxExp = (int)(maxExp * 1.5f);

        UIManager.Instance.UpdateLevel(currentLevel);

        UIManager.Instance.OpenUI(UIType.Upgrade);
    }

    public void AddDashDamage(float damage)
    {
        dashDamage += damage;
    }
}
