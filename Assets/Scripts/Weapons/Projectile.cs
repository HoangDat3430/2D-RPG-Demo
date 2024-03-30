using System.Collections;
using System.Collections.Generic;
using Cinemachine.Utility;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 22f;
    [SerializeField] private GameObject hitVFX;
    [SerializeField] private bool isEnemyProjectile;
    [SerializeField] private float projectileRange = 10f;

    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }
    private void Update()
    {
        MoveProjectile();
        DetectFireRange();  
    }
    public void UpdateProjectileRange(float range)
    {
        projectileRange = range;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyHealth enemyHealth = collision.GetComponent<EnemyHealth>();
        Indestructible indestructible = collision.GetComponent<Indestructible>();
        PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();

        if (!collision.isTrigger && (enemyHealth || indestructible || playerHealth))
        {
            if ((playerHealth && isEnemyProjectile) || (enemyHealth && !isEnemyProjectile))
            {
                playerHealth?.TakeDmg(1, transform);
                Instantiate(hitVFX, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            else if(!collision.isTrigger && indestructible) {
                Instantiate(hitVFX, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
            
        }
    }
    private void DetectFireRange()
    {
        if(Vector3.Distance(transform.position, startPos) > projectileRange)
        {
            Destroy(gameObject);
        }
    }
    private void MoveProjectile()
    {
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }
}
