using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : MonoBehaviour, IWeapon
{
    [SerializeField] private WeaponInfo wpInfo;

    private Animator m_Animator;
    private PolygonCollider2D weaponCollider;
    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
    }
    private void Start()
    {
        weaponCollider = transform.GetComponent<PolygonCollider2D>();
        weaponCollider.enabled = false;
        ActiveWeapon.Instance.GetComponent<MouseFollow>().enabled = false;
        ActiveWeapon.Instance.GetComponent<MouseFollow>().enabled = true;
    }
    private void Update()
    {
        ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0f, PlayerController.Instance.FacingLeft ? 180f:0f, 0f);
    }
    public void Attack()
    {
        m_Animator.SetTrigger("Attack");
    }
    public void OnAttackDone()
    {
    }
    public void OnResetCollider()// attach on animation clip to controll the collider like sword
    {
        weaponCollider.enabled = false;
    }
    public void OnenableCollider()
    {
        weaponCollider.enabled = true;
    }
    public WeaponInfo GetWeaponInfo()
    {
        return wpInfo;
    }
}
