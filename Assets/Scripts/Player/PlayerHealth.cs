using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : Singleton<PlayerHealth>
{
    public bool isDead {  get; private set; }

    private Slider healthSlider;

    private int maxHealth = 3;
    private float knockBackThrustAmount = 10f;
    private float recoveryTime = 1f;

    private int curHealth;
    private bool canTakeDmg = true;
    private KnockBack knockBack;
    private WhiteFlash whiteFlash;

    const string SCENE_REBORN = "Scene1";
    protected override void Awake()
    {
        knockBack = GetComponent<KnockBack>();
        whiteFlash = GetComponent<WhiteFlash>();
    }
    private void Start()
    {
        isDead = false;
        curHealth = maxHealth;
        healthSlider = UIFade.Instance.transform.Find("Health/HealthSlider").GetComponent<Slider>();
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        EnemyAI enemy = collision.gameObject.GetComponent<EnemyAI>();

        if(enemy)
        {
            TakeDmg(1, collision.transform);
        }
    }
    public void TakeDmg(int dmg, Transform hitTrans)
    {
        if(!canTakeDmg) { return; } // can not take dmg while in recovery time
        knockBack.GettingKnockBack(hitTrans, knockBackThrustAmount);
        StartCoroutine(whiteFlash.FlashRoutine());
        curHealth -= dmg;
        UpdateHealth();
        CheckIfPlayerDeath();
        canTakeDmg = false;
        StartCoroutine(RecoveryRoutine());
    }
    private void CheckIfPlayerDeath()
    {
        if(curHealth <= 0 && !isDead)
        {
            isDead = true;
            curHealth = 0;
            Destroy(ActiveWeapon.Instance.gameObject);
            StartCoroutine(RebornSceneRoutine());
        }
    }
    private IEnumerator RebornSceneRoutine()
    {
        UIFade.Instance.FadeToBlack();
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
        SceneManager.LoadScene(SCENE_REBORN); // load scene 1
        UIFade.Instance.FadeToWhite();
        curHealth = maxHealth;
        UpdateHealth();
    }
    private void UpdateHealth()
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = curHealth;// update the slider UI 
        Debug.Log(healthSlider.value);
    }
    private IEnumerator RecoveryRoutine()
    {
        yield return new WaitForSeconds(recoveryTime);
        canTakeDmg = true;
    }
}
