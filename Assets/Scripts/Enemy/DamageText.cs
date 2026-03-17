using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    [SerializeField]
    private TMP_Text textDamage;

    [SerializeField]
    private float moveSpeed = 2.0f;

    [SerializeField]
    private float alphaSpeed = 1.0f;

    [SerializeField]
    private Color textColor;

    private void OnEnable()
    {
        textColor.a = 1.0f;
        textDamage.color = textColor;
        Invoke("ReturnToPool", 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        // 위로 서서히 올라가는 연출.
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);

        // 서서히 투명해지는 연출.
        textColor.a = textColor.a - (alphaSpeed * Time.deltaTime);
        textDamage.color = textColor;
    }

    public void SetDamage(float amount)
    {
        textDamage.text = amount.ToString();
    }

    void ReturnToPool()
    {
        if(PoolManager.instance != null)
        {
            PoolManager.instance.ReturnObject(PoolID.DamageText, gameObject);
        }
    }
}
