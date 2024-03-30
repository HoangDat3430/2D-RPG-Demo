using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor.PackageManager;
using UnityEngine;

public class Staff : MonoBehaviour, IWeapon
{
    [SerializeField] private WeaponInfo wpInfo;
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private Transform laserStartPoint;

    readonly int FIREHASH = Animator.StringToHash("Attack");

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    
    private void Start()
    {
    }
    private void Update()
    {
    }
    public void Attack()
    {
        animator.SetTrigger(FIREHASH);
        GameObject laser = Instantiate(laserPrefab, laserStartPoint.position, Quaternion.identity);// same with bow
        laser.GetComponent<MagicLaser>().UpdateRange(wpInfo.range);
    }
    public void SpawnLaserAnimEvent() {
        
    }
    public WeaponInfo GetWeaponInfo()
    {
        return wpInfo;
    }
}
