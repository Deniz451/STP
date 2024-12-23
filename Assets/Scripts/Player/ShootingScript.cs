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
        bullet.transform.position = gunInstance.GetComponentInChildren<Transform>().position; //spawne kulku na spravnym miste

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null) { rb.velocity = (gunInstance.GetComponentInChildren<Transform>().up * -1 * gun.bulletSpeed); }  //nastavi rychlost a smer (mozna by to chtelo v budoucnu optimalizovat, ale je 5:30 rn :) )

        StartCoroutine(DespawnBullet(bullet, gun.bulletLifetime));  //zapne timer na zniceni
        
        if(index == 2) { index = 1;} else {index = 2;}     //zmeni index

        yield return new WaitForSeconds(gun.attackDelay);

        done = true;
    }

    IEnumerator DespawnBullet(GameObject bullet, float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(bullet);
    }
}
