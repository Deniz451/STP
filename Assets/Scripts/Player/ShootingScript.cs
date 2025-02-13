using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    public GameObject GunSpawnR;
    public GameObject GunSpawnL;

    [SerializeField] GunSO defaultGun;
    [SerializeField] GameObject gunManager;
    GunManager gm;

    public GunSO currentGunL;
    public GunSO currentGunR;
    public GameObject gunR;
    public GameObject gunL;
    public Transform spawnL;
    public Transform spawnR;
    public Transform headHolder;

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

            foreach (GunSO gun in gm.guns)                                              //loadne ulozeny zmeny zbrani pri zapnuti hry
            {
                if (gun == null) { Debug.LogWarning("One of the guns in GunManager is null."); continue; }

                if (gun.name == savedWeaponL) {currentGunL = gun;}
                if (gun.name == savedWeaponR) { currentGunR = gun;}
            }
        }

        if(currentGunL == null) { currentGunL = defaultGun; } 
        if(currentGunR == null) { currentGunR = defaultGun; }

        Debug.Log($"Chosen gun L: {currentGunL.name}, gun R: {currentGunR.name}");

        if (currentGunR.gunPrefab == null) { Debug.LogError("CurrentGunR gunPrefab is not assigned!"); }
        if (GunSpawnR == null) { Debug.LogError("GunSpawnR is not assigned!"); }

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

                    if (currentGunR == null) { currentGunR = defaultGun; }
                    StartCoroutine(ShootBullet(currentGunR, gunR));
                    break;
                case 2:
                    if (currentGunL == null) { currentGunL = defaultGun; }
                    StartCoroutine(ShootBullet(currentGunL, gunL));
                    break;
            }
        }
    }

    IEnumerator ShootBullet(GunSO gun, GameObject gunInstance)
    {
        done = false;

        GameObject bullet = Instantiate(gun.projectilePrefab);
        bullet.transform.position = (index == 1) ? spawnL.position : spawnR.position;

        Vector3 shootDirection = headHolder.forward.normalized;

        bullet.transform.rotation = Quaternion.LookRotation(Vector3.down, shootDirection);

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null) rb.velocity = headHolder.forward * gun.bulletSpeed;

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
