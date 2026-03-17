using UnityEngine;

public class HitEffect : MonoBehaviour
{
    private void Start()
    {
        Invoke("ReturnObject", 3.0f);
    }

    void ReturnObject()
    {
        PoolManager.instance.ReturnObject(PoolID.HitEffect, gameObject);
    }
}
