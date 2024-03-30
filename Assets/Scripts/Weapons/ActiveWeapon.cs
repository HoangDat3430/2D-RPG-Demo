using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class ActiveWeapon : Singleton<ActiveWeapon>
{
    // this is the general class uses for weapons
    public MonoBehaviour curWeapon { get; private set; }

    private float CD;
    private PlayerInput input;
    private bool is_Attacking = false;
    public bool IsAttacking { get { return is_Attacking; } }
    protected override void Awake()
    {
        base.Awake();
        input = new PlayerInput();
    }
    private void OnEnable()
    {
        input.Enable();
    }
    private void Start()
    {
        input.Combat.Attack.started += _ => Attack();
    }
    public void SetWeapon(MonoBehaviour wp)
    {
        curWeapon = wp;
        CD = (curWeapon as IWeapon).GetWeaponInfo().cd;
        if (curWeapon != null && curWeapon is Staff)
        {
            transform.localPosition = new Vector3(0f, .3f, 0f);
        }
        else
        {
            transform.localPosition = Vector3.zero;
        }
        GetComponent<MouseFollow>().enabled = curWeapon is not Sword ? true : false;
    }
    private void SetCooldown()
    {
        is_Attacking = true;
        StopAllCoroutines();
        StartCoroutine(CooldownRoutine());
    }
    private IEnumerator CooldownRoutine()
    {
        yield return new WaitForSeconds(CD);
        is_Attacking = false;
    }
    protected virtual void Attack()
    {
        if (is_Attacking || curWeapon == null) return;
        (curWeapon as IWeapon).Attack();
        SetCooldown();
    }
    public void ToggleAttack(bool value)
    {
        is_Attacking = value;
    }
}
