using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : BaseUnit
{
    [SerializeField]
    private int dropExp;

    [SerializeField]
    private Image imageHPBar;

    [SerializeField]
    private int enemyID;

    [SerializeField]
    private int[] randomExp = new int[3] { 10, 20, 30 };

    [SerializeField]
    private float originMoveSpeed;

    [SerializeField]
    private SpriteRenderer spriteRenderer;

    private Coroutine slowCoroutine;
    private Coroutine burnCoroutine;
    private Coroutine freezeCoroutine;


    protected override void Awake()
    {
        SetupEnemyData();
        originMoveSpeed = moveSpeed;
    }

    protected override void OnEnable()
    {
        Init();

        moveSpeed = originMoveSpeed;
        spriteRenderer.color = Color.white;

        // 실행중인 모든 코루틴을 중단.
        StopAllCoroutines();

        slowCoroutine = null;
        burnCoroutine = null;
        freezeCoroutine = null;

        UpdateHPBar();
    }

    public override void TakeDamage(float damageAmount)
    {
        base.TakeDamage(damageAmount);

        UpdateHPBar();

        if (isDead == true)
        {
            return;
        }

        Debug.Log(unitName + "가(이) 공격받았습니다. 남은 체력 : " + currentHealth);
    }

    protected override void Die()
    {
        base.Die();

        if(PoolManager.instance != null)
        {
            GameObject expGem = PoolManager.instance.GetExpGem();
            if (expGem != null)
            {
                expGem.transform.position = transform.position;
                expGem.transform.rotation = Quaternion.identity;

                ExpGem gem = expGem.GetComponent<ExpGem>();
                if(gem != null)
                {
                    int index = Random.Range(0, randomExp.Length);
                    gem.SetExpData(randomExp[index]);
                }
            }

            PoolManager.instance.ReturnEnemy(gameObject);
        }

        // 경험치를 드랍하거나, 플레이어의 경험치를 올려주는 처리를 추후에 한다.
        Debug.Log(unitName + "가(이) 사망!! 경험치 : " + dropExp);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) == true)
        {
            TakeDamage(3);
        }
    }

    protected virtual void UpdateHPBar()
    {
        if(currentHealth <= 0)
        {
            imageHPBar.fillAmount = 0.0f;
            return;
        }

        imageHPBar.fillAmount = currentHealth / maxHealth;
    }

    public void SetupEnemyData()
    {
        if(GameManager.Instance != null)
        {
            EnemyData enemyData = GameManager.Instance.GetEnemyData(enemyID);
            if(enemyData != null)
            {
                maxHealth = enemyData.maxHealth;
                unitName = enemyData.enemyName;
                moveSpeed = enemyData.moveSpeed;
                dropExp = enemyData.dropEXP;
            }
        }
    }

    public void ApplySlow(float duration, float penaltyPercent)
    {
        // 이미 실행중인 상태 이상 효과가 있다면 취소시키기.
        if(slowCoroutine != null)
        {
            StopCoroutine(slowCoroutine);
        }

        slowCoroutine = StartCoroutine(SlowRoutine(duration, penaltyPercent));
    }

    IEnumerator SlowRoutine(float duration, float penaltyPercent)
    {
        // 속도 감소 적용 (예 : penaltyPercent가 0.5면 속도가 50% 감소)
        moveSpeed = originMoveSpeed * (1.0f - penaltyPercent);

        // 시각적 효과를 주고 싶을 경우 SpriteRenderer의 색상을 변경.
        spriteRenderer.color = Color.grey;

        yield return new WaitForSeconds(duration);

        moveSpeed = originMoveSpeed;
        spriteRenderer.color = Color.white;
        slowCoroutine = null;
    }

    public void ApplyBurn(float duration, float damagePerSecond)
    {
        if(burnCoroutine != null)
        {
            StopCoroutine(burnCoroutine);
        }

        burnCoroutine = StartCoroutine(BurnRoutine(duration, damagePerSecond));
    }

    IEnumerator BurnRoutine(float duration, float damagePerSecond)
    {
        float elapsed = 0.0f;

        while(elapsed < duration)
        {
            TakeDamage(damagePerSecond);
            spriteRenderer.color = Color.red;

            yield return new WaitForSeconds(0.5f);

            spriteRenderer.color = Color.white;

            yield return new WaitForSeconds(0.5f);
            elapsed++;
        }

        burnCoroutine = null;
    }

    public void ApplyFreeze(float duration)
    {
        if(freezeCoroutine != null)
        {
            StopCoroutine(freezeCoroutine);
        }

        freezeCoroutine = StartCoroutine(FreezeRoutine(duration));
    }

    IEnumerator FreezeRoutine(float duration)
    {
        moveSpeed = 0.0f;
        spriteRenderer.color = Color.cyan;

        yield return new WaitForSeconds(duration);

        moveSpeed = originMoveSpeed;
        spriteRenderer.color = Color.white;
        freezeCoroutine = null;
    }
}
