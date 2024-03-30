using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bow : MonoBehaviour, IWeapon
{
    [SerializeField] private WeaponInfo wpInfo;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private Transform arrowSpawnPoint;

    readonly int FIREHASH = Animator.StringToHash("Fire");
    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public void Attack()
    {
        animator.SetTrigger(FIREHASH);
        GameObject arrow = Instantiate(arrowPrefab, arrowSpawnPoint.position, ActiveWeapon.Instance.transform.rotation);// create the arrow
        arrow.GetComponent<Projectile>().UpdateProjectileRange(wpInfo.range);// update data and fire the arrow
    }
    private void Start()
    {
    }
    public WeaponInfo GetWeaponInfo()
    {
        return wpInfo;
    }
}
