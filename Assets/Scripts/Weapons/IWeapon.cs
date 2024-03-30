using UnityEngine;

interface IWeapon
{
    // I created an interface for weapons
    public void Attack();
    public WeaponInfo GetWeaponInfo();
}