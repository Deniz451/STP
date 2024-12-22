using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    [SerializeField] GameObject gunAnchor;
    public List<GunSO> guns;

    [SerializeField] GameObject gunPointL;
    [SerializeField] GameObject gunPointR;
    GameObject selectedGun;
    GameObject currentGunLeft;
    GameObject currentGunRight;
    GameObject gunR;

    ShootingScript ss;

    int i = 0;
    Quaternion displayRotation = new Quaternion(0.669098794f, 0.281925321f, -0.48636898f, 0.486076236f);

    

    private void Start()
    {
        selectedGun = GameObject.Instantiate(guns[i].gunPrefab, gunAnchor.transform.position, displayRotation);
        ss = GameObject.FindGameObjectWithTag("Player").GetComponent<ShootingScript>();
    }

    public void NextGun()
    {
        Destroy(selectedGun);
        if (i < guns.Count - 1) { i++; } else { i = 0; }
        selectedGun = GameObject.Instantiate(guns[i].gunPrefab, gunAnchor.transform.position, displayRotation);
    }

    public void PreviousGun()
    {
        Destroy(selectedGun);
        if (i == 0) { i = guns.Count -1; } else { i--; }
        selectedGun = GameObject.Instantiate(guns[i].gunPrefab, gunAnchor.transform.position, displayRotation);
    }

    public void SelectGunL()
    {
        ss.currentGunL = guns[i];
        if(currentGunLeft != null) { Destroy(currentGunLeft); }
        currentGunLeft = GameObject.Instantiate(guns[i].gunPrefab, ss.GunSpawnL.transform.position, guns[i].gunRotation);
    }
    public void SelectGunR()
    {
        ss.currentGunR = guns[i];
        if (currentGunRight != null) { Destroy(currentGunRight); }
        currentGunRight = GameObject.Instantiate(guns[i].gunPrefab, ss.GunSpawnR.transform.position, guns[i].gunRotation);
    }
}
