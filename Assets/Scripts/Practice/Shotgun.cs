using UnityEngine;

public class Shotgun : Weapon
{
    public override void Upgrade(int addMaxAmmo)
    {
        maxAmmoCount += addMaxAmmo * 3;
        currentAmmoCount = maxAmmoCount;
    }
}
