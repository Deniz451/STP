using System;
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

    public Action Shoot;

    private int index = 1;
    private bool done = true;
    private bool canShoot;


    private void OnEnable() {
        EventManager.Instance.Subscribe(GameEvents.EventType.PlayerEnabled, () => canShoot = true);
        EventManager.Instance.Subscribe(GameEvents.EventType.PlayerDisabled, () => canShoot = false);
    }

    private void OnDestroy() {
        EventManager.Instance.Unsubscribe(GameEvents.EventType.PlayerEnabled, () => canShoot = true);
        EventManager.Instance.Unsubscribe(GameEvents.EventType.PlayerDisabled, () => canShoot = false);
    }

    private void Start()
    {
        gunL = Instantiate(selectedWeaponsSO.gunL.gunPrefab, gunLHolder);
        gunR = Instantiate(selectedWeaponsSO.gunR.gunPrefab, gunRHolder);
    }

    void Update()
    {
        if (Input.GetMouseButton(0) && done && canShoot)
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

        Shoot?.Invoke();
        SoundManagerSO.PlaySFXClip(gun.fire, transform.position, 0.5f);

        GameObject bullet = Instantiate(gun.projectilePrefab);

        bullet.transform.position = (index == 1) ? bulletSpawnR.position : bulletSpawnL.position;

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
