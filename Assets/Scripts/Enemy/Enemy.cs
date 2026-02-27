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

    protected override void Awake()
    {
        SetupEnemyData();
    }

    protected override void OnEnable()
    {
        Init();

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
}
