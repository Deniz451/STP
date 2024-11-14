using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletSpawnR;
    [SerializeField] Transform bulletSpawnL;

    [SerializeField] float bulletSpeed = 10f;
    [SerializeField] float attackSpeed = 0.5f;
    [SerializeField] float bulletLifetime = 10f;

    int index = 1;
    bool done = true;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0)&&done) 
        {
            switch (index)
            {
                case 1:
                    StartCoroutine(ShootBullet(bulletPrefab, attackSpeed, bulletSpawnR));
                break;
                case 2:
                    StartCoroutine(ShootBullet(bulletPrefab, attackSpeed, bulletSpawnL));
                break;
            }
        }
    }

    IEnumerator ShootBullet(GameObject bullet, float time, Transform spawn)
    {
        done = false;

        bullet = Instantiate(bullet);
        bullet.transform.position = spawn.position; //Spawn bullet at right position

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null) { rb.velocity = spawn.forward * bulletSpeed; }  //Set speed and direction

        StartCoroutine(DespawnBullet(bullet, bulletLifetime));  //Start despawn timer
        
        if(index == 2) { index = 1;} else {index = 2;}     //Set index

        yield return new WaitForSeconds(time);

        done = true;
    }
    IEnumerator DespawnBullet(GameObject bullet, float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(bullet);
    }
}
