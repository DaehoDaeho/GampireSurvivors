using UnityEngine;

public class Rifle : Weapon
{
    public override void Shoot()
    {
        if (currentAmmoCount <= 0)
        {
            return;
        }

        currentAmmoCount -= 2;
    }

    public override void Upgrade(int addMaxAmmo)
    {
        maxAmmoCount += addMaxAmmo * 2;
        currentAmmoCount = maxAmmoCount;
    }
}
