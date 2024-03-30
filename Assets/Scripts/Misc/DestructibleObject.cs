using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObject : MonoBehaviour
{
    [SerializeField] private GameObject DestroyVFX;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<DamageSource>() || collision.GetComponent<Projectile>())
        {
            Instantiate(DestroyVFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
            if(ActiveWeapon.Instance.curWeapon is not Sword && ActiveWeapon.Instance.curWeapon is not Spear) Destroy(collision.gameObject);
        }
    }
}
