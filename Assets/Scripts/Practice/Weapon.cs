using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField]
    protected string weaponName;

    [SerializeField]
    protected float damage;

    [SerializeField]
    protected int level;

    [SerializeField]
    protected float maxAmmoCount;

    protected float currentAmmoCount;

    public virtual void Shoot()
    {
        if(currentAmmoCount <= 0)
        {
            return;
        }

        --currentAmmoCount;

        // 발사 처리.
    }

    public virtual void Reload()
    {
        currentAmmoCount = maxAmmoCount;
    }

    public virtual void Upgrade(int addMaxAmmo)
    {
        maxAmmoCount += addMaxAmmo;
        currentAmmoCount = maxAmmoCount;
    }
}
