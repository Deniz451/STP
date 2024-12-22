using System.Collections;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    public GameObject GunSpawnR;
    public GameObject GunSpawnL;

    [SerializeField] GunSO defaultGun;
    [SerializeField] GameObject gunManager;
    GunManager gm;

    public GunSO currentGunL;
    public GunSO currentGunR;
    public GameObject gunR;
    public GameObject gunL;

    int index = 1;
    bool done = true;
    string savedWeaponL;
    string savedWeaponR;

    private void Start()
    {
        gm = gunManager.GetComponent<GunManager>();
        if (PlayerPrefs.HasKey("WeaponL") || PlayerPrefs.HasKey("WeaponR"))
        {
            savedWeaponL = PlayerPrefs.GetString("WeaponL");
            savedWeaponR = PlayerPrefs.GetString("WeaponR");
            foreach (GunSO gun in gm.guns)
            {
                if (gun.name == savedWeaponL) currentGunL = gun;
                if (gun.name == savedWeaponR) currentGunR = gun;
                if (currentGunL.name == savedWeaponL && currentGunR.name == savedWeaponR) break;
            }
        }

        if (currentGunL == null) currentGunL = defaultGun;
        if (currentGunR == null) currentGunR = defaultGun;

        gunR = Instantiate(currentGunR.gunPrefab, GunSpawnR.transform.position, currentGunR.gunRotation);
        gunL = Instantiate(currentGunL.gunPrefab, GunSpawnL.transform.position, currentGunL.gunRotation);

        gunR.transform.parent = gameObject.transform.Find("playerHead");
        gunL.transform.parent = gameObject.transform.Find("playerHead");
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && done)
        {
            switch (index)
            {
                case 1:
                    if (currentGunR == null) currentGunR = defaultGun;
                    StartCoroutine(ShootBullet(currentGunR, gunR));
                    break;
                case 2:
                    if (currentGunL == null) currentGunL = defaultGun;
                    StartCoroutine(ShootBullet(currentGunL, gunL));
                    break;
            }
        }
    }

    IEnumerator ShootBullet(GunSO gun, GameObject gunInstance)
    {
        done = false;

        GameObject bullet = Instantiate(gun.projectilePrefab);
        bullet.transform.position = gunInstance.GetComponentInChildren<Transform>().position;

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null) rb.velocity = gunInstance.GetComponentInChildren<Transform>().up * gun.bulletSpeed;

        StartCoroutine(DespawnBullet(bullet, gun.bulletLifetime));

        index = (index == 2) ? 1 : 2;

        yield return new WaitForSeconds(gun.attackDelay);

        done = true;
    }

    IEnumerator DespawnBullet(GameObject bullet, float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(bullet);
    }
}
