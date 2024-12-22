using System.Collections;
<<<<<<< Updated upstream
=======
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
>>>>>>> Stashed changes
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
        if(PlayerPrefs.HasKey("WeaponL") || PlayerPrefs.HasKey("WeaponR"))
        {
            savedWeaponL = PlayerPrefs.GetString("WeaponL");
            savedWeaponR = PlayerPrefs.GetString("WeaponR");
            foreach (GunSO gun in gm.guns)                                              //loadne ulozeny zmeny zbrani pri zapnuti hry
            {
                if (gun.name == savedWeaponL) {currentGunL = gun;}
                if (gun.name == savedWeaponR) { currentGunR = gun;}
                if (currentGunL.name == savedWeaponL && currentGunR.name == savedWeaponR) {break;}
            }
        }

        if(currentGunL == null) { currentGunL = defaultGun; } if(currentGunR == null) {currentGunR = defaultGun; }

        gunR = GameObject.Instantiate(currentGunR.gunPrefab, GunSpawnR.transform.position, currentGunR.gunRotation);
        gunL = GameObject.Instantiate(currentGunL.gunPrefab, GunSpawnL.transform.position, currentGunL.gunRotation);        //spawne zbrane do modelu hrace

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
<<<<<<< Updated upstream
                    StartCoroutine(ShootBullet(bulletPrefab, attackSpeed, bulletSpawnR));
                    break;
                case 2:
                    StartCoroutine(ShootBullet(bulletPrefab, attackSpeed, bulletSpawnL));
                    break;
=======
                    if(currentGunR == null) { currentGunR = defaultGun; }
                    StartCoroutine(ShootBullet(currentGunR, gunR));                             //vola metody pro strileni
                break;
                case 2:
                    if (currentGunL == null) { currentGunL = defaultGun; }
                    StartCoroutine(ShootBullet(currentGunL, gunL));
                break;
>>>>>>> Stashed changes
            }
        }
    }

    IEnumerator ShootBullet(GunSO gun, GameObject gunInstance)
    {
        done = false;

<<<<<<< Updated upstream
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
=======
        GameObject bullet = Instantiate(gun.projectilePrefab);
        bullet.transform.position = gunInstance.GetComponentInChildren<Transform>().position; //spawne kulku na spravnym miste

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null) { rb.velocity = (gunInstance.GetComponentInChildren<Transform>().up * gun.bulletSpeed); }  //nastavi rychlost a smer (mozna by to chtelo v budoucnu optimalizovat, ale je 5:30 rn :) )

        StartCoroutine(DespawnBullet(bullet, gun.bulletLifetime));  //zapne timer na zniceni
        
        if(index == 2) { index = 1;} else {index = 2;}     //zmeni index
>>>>>>> Stashed changes

        yield return new WaitForSeconds(gun.attackDelay);

        done = true;
    }
<<<<<<< Updated upstream
=======

    IEnumerator DespawnBullet(GameObject bullet, float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(bullet);
    }
>>>>>>> Stashed changes
}
