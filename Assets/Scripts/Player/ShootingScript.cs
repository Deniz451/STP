using System.Collections;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    public Transform gunRHolder;
    public Transform gunLHolder;
    public Transform bulletSpawnL;
    public Transform bulletSpawnR;

    public GameObject gunR;
    public GameObject gunL;

    public Transform headHolder;
    public SelectedWeaponsSO selectedWeaponsSO;

    private int index = 1;
    private bool done = true;

    private void Start()
    {
        // Instantiate guns and assign to variables
        gunL = Instantiate(selectedWeaponsSO.gunL.gunPrefab, gunLHolder);
        gunR = Instantiate(selectedWeaponsSO.gunR.gunPrefab, gunRHolder);
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && done)
        {
            switch (index)
            {
                case 1:
                    StartCoroutine(ShootBullet(selectedWeaponsSO.gunR, gunR));
                    break;
                case 2:
                    StartCoroutine(ShootBullet(selectedWeaponsSO.gunL, gunL));
                    break;
            }
        }
    }

    IEnumerator ShootBullet(GunSO gun, GameObject gunInstance)
    {
        done = false;

        // Create bullet and set spawn position based on the current gun
        GameObject bullet = Instantiate(gun.projectilePrefab);
        bullet.transform.position = (index == 1) ? bulletSpawnR.position : bulletSpawnL.position;

        // Set bullet direction
        Vector3 shootDirection = headHolder.forward.normalized;
        bullet.transform.rotation = Quaternion.LookRotation(Vector3.down, shootDirection);

        // Add velocity to the bullet
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null) rb.velocity = headHolder.forward * gun.bulletSpeed;

        // Handle bullet despawning
        StartCoroutine(DespawnBullet(bullet, gun.bulletLifetime));

        // Alternate the gun for the next shot
        index = (index == 2) ? 1 : 2;

        // Delay between shots
        yield return new WaitForSeconds(gun.attackDelay);

        done = true;
    }

    IEnumerator DespawnBullet(GameObject bullet, float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(bullet);
    }
}
