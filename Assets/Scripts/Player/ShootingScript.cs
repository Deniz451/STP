using System;
using System.Collections;
using UnityEngine;

public class ShootingScript : MonoBehaviour
{
    public Transform gunRHolder;
    public Transform gunLHolder;
    public Transform bulletSpawnL;
    public Transform bulletSpawnR;

    public Transform headHolder;
    private GunSO gunL;
    private GunSO gunR;
    private GameObject gunLPrefab;
    private GameObject gunRPrefab;

    public Action Shoot;

    private int index = 1;
    private bool doneLeft = true;
    private bool doneRight = true;
    private bool canShoot;


    private void OnEnable() {
        EventManager.Instance.Subscribe(GameEvents.EventType.PlayerEnabled, () => canShoot = true);
        EventManager.Instance.Subscribe(GameEvents.EventType.PlayerDisabled, () => canShoot = false);
    }

    private void OnDestroy() {
        EventManager.Instance.Unsubscribe(GameEvents.EventType.PlayerEnabled, () => canShoot = true);
        EventManager.Instance.Unsubscribe(GameEvents.EventType.PlayerDisabled, () => canShoot = false);
    }

    private void Start() {
        var playerManager = GetComponent<PlayerManager>();
        playerManager.switchWeapons += SwitchWeapons;
    }

    void Update()
    {
        if (!canShoot) return;
        if (Input.GetMouseButton(0))
        {
            if (doneLeft) StartCoroutine(ShootBullet(gunR, doneRight));
            else if (doneRight) StartCoroutine(ShootBullet(gunL, doneLeft));
        }
    }

    IEnumerator ShootBullet(GunSO gun, bool marker)
    {
        marker = false;

        Shoot?.Invoke();
        SoundManagerSO.PlaySFXClip(gun.fireClip, transform.position, 0.5f);

        GameObject bullet = Instantiate(gun.projectilePrefab);

        bullet.transform.position = (index == 1) ? bulletSpawnR.position : bulletSpawnL.position;

        Vector3 shootDirection = headHolder.forward.normalized;
        bullet.transform.rotation = Quaternion.LookRotation(Vector3.down, shootDirection);

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null) rb.velocity = headHolder.forward * gun.bulletSpeed;

        StartCoroutine(DespawnBullet(bullet, gun.bulletLifetime));

        index = (index == 2) ? 1 : 2;

        yield return new WaitForSeconds(gun.attackDelay);

        marker = true;
    }

    IEnumerator DespawnBullet(GameObject bullet, float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(bullet);
    }

    private void SwitchWeapons(GunSO gunL, GunSO gunR) {
        this.gunL = gunL;
        this.gunR = gunR;

        if (gunLPrefab != null) Destroy(gunLPrefab);
        if (gunRPrefab != null) Destroy(gunRPrefab);
        
        gunLPrefab = Instantiate(gunL.prefab, gunLHolder.position, gunL.prefab.transform.rotation);
        gunLPrefab.transform.SetParent(gunLHolder.transform, true);
        gunLPrefab.transform.rotation = new(0, 0, 0, 0);
        gunLPrefab.transform.localScale = gunL.prefab.transform.localScale;
        gunLPrefab.GetComponent<ChangeablePart>().canSelect = true;

        gunRPrefab = Instantiate(gunR.prefab, gunRHolder.position, gunR.prefab.transform.rotation);
        gunRPrefab.transform.SetParent(gunRHolder.transform, true);
        gunRPrefab.transform.rotation = new(0, 0, 0, 0);
        gunRPrefab.transform.localScale = gunR.prefab.transform.localScale;
        gunRPrefab.GetComponent<ChangeablePart>().canSelect = true;
    }
}
