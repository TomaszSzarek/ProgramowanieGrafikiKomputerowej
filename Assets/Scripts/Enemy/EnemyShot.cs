using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShot : MonoBehaviour
{
    public GameObject bulletPrefab; 
    public Transform playerTransform;

    public float shootingRange = 20f;
    public float fireRate = 2f;
    private float nextFireTime; 

    private void Start()
    {
        nextFireTime = Time.time + fireRate; 
    }

    private void Update()
    {
        if (Vector3.Distance(transform.position, playerTransform.position) <= shootingRange)
        {
            if (Time.time >= nextFireTime)
            {
                Shoot();
                
                nextFireTime = Time.time + fireRate;
            }
        }
    }

    private void Shoot()
    {
        Vector3 shootDirection = (playerTransform.position - transform.position).normalized;
        Vector3 spawnPosition = transform.position + shootDirection;
        GameObject newBullet = Instantiate(bulletPrefab, spawnPosition, Quaternion.identity);
        newBullet.GetComponent<Bullet>().SetDirection(shootDirection);
    }
}
