using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSource : MonoBehaviour
{
    private float dmg;
    private void Start()
    {
        MonoBehaviour activeWeapon = ActiveWeapon.Instance.curWeapon;
        dmg = (activeWeapon as IWeapon).GetWeaponInfo().dmg;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
        enemyHealth?.TakenDmg(dmg);
    }
}
