using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float healthPoint = 5;
    [SerializeField] private GameObject DeathVFX;

    private KnockBack knockBack;
    private WhiteFlash whiteFlash;
    public float health
    {
        get { return healthPoint; } set { healthPoint = value; }
    }
    private void Awake()
    {
        knockBack = GetComponent<KnockBack>();
        whiteFlash = GetComponent<WhiteFlash>();
    }
    public void TakenDmg(float dmg)
    {
        healthPoint -= dmg;
        knockBack.GettingKnockBack(PlayerController.Instance.transform, 10f);
        StartCoroutine(whiteFlash.FlashRoutine());
        CheckDead();
    }
    private void CheckDead()
    {
        if(healthPoint <= 0)
        {
            Instantiate(DeathVFX, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
