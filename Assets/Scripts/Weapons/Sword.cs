using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour, IWeapon
{
    [SerializeField] private GameObject slashPrefab;
    [SerializeField] private Transform slashSpawnPoint;
    [SerializeField] private WeaponInfo wpInfo;

    private Animator m_Animator;
    private GameObject slashEffect;
    private BoxCollider2D weaponCollider;
    private void Awake()
    {
        m_Animator = GetComponent<Animator>();
    }
    private void Start()
    {
        weaponCollider = transform.GetComponent<BoxCollider2D>();
        weaponCollider.enabled = false;
    }
    private void Update()
    {
        ActiveWeapon.Instance.transform.rotation = Quaternion.Euler(0f, PlayerController.Instance.FacingLeft ? 180f:0f, 0f);
        // keep the sword is permanent in front of the player
    }
    public void Attack()
    {
        weaponCollider.enabled = true;
        m_Animator.SetTrigger("Attack");
        slashEffect = Instantiate(slashPrefab, slashSpawnPoint.position, Quaternion.identity);// create the effect slash
        slashEffect.transform.parent = transform.parent;
        if (PlayerController.Instance.FacingLeft)
        {
            slashEffect.transform.eulerAngles = new Vector3(0f, 180f, 30f);// slash effect also need to create the same direction with player and sword
        }
        else
        {
            slashEffect.transform.eulerAngles = new Vector3(0f, 0f, 30f);
        }
    }
    public void OnAttackDone()
    {
    }
    public void OnResetCollider()// attach on the animation clip
    {
        weaponCollider.enabled = false;
    }
    public WeaponInfo GetWeaponInfo()
    {
        return wpInfo;
    }
}
