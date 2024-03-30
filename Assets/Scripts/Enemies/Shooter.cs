using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour, IEnemy
{
    [SerializeField] private GameObject prefabBullet;

    public void Attack()
    {
        Vector2 tarDir = PlayerController.Instance.transform.position - transform.position;
        GameObject bullet = Instantiate(prefabBullet, transform.position, Quaternion.identity);
        bullet.transform.right = tarDir;
    }
}
