using System.Collections;
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

    void Update()
    {
        if (Input.GetMouseButton(0) && done)
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
        bullet.transform.position = spawn.position;
        bullet.transform.rotation = Quaternion.LookRotation(spawn.forward) * Quaternion.Euler(90, 0, 0);

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = spawn.forward * bulletSpeed;
        }

        Destroy(bullet, bulletLifetime);

        index = (index == 2) ? 1 : 2;

        yield return new WaitForSeconds(time);

        done = true;
    }
}
