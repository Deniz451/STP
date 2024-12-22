using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunManager : MonoBehaviour
{
    [SerializeField] GameObject gunAnchor;
    public List<GunSO> guns;        //Musi byt v kazde scene s player kvuli tomuhle listu

    GameObject selectedGun;
    GameObject currentGunLeft;
    GameObject currentGunRight;

    ShootingScript ss;

    int i = 0;
    Quaternion displayRotation = new Quaternion(0.669098794f, 0.281925321f, -0.48636898f, 0.486076236f);

    

    private void Start()
    {
        ss = GameObject.FindGameObjectWithTag("Player").GetComponent<ShootingScript>();
    }

    public void OpenGarage()
    {
        selectedGun = GameObject.Instantiate(guns[i].gunPrefab, gunAnchor.transform.position, displayRotation);         //metoda na start vymeny zbrani, musim dodelat
    }

    public void NextGun()
    {
        Destroy(selectedGun);
        if (i < guns.Count - 1) { i++; } else { i = 0; }
        selectedGun = GameObject.Instantiate(guns[i].gunPrefab, gunAnchor.transform.position, displayRotation);
    }

    public void PreviousGun()                                                                                           //meni zbrane ve vyberu
    {
        Destroy(selectedGun);
        if (i == 0) { i = guns.Count -1; } else { i--; }
        selectedGun = GameObject.Instantiate(guns[i].gunPrefab, gunAnchor.transform.position, displayRotation);
    }

    public void SelectGunL()
    {
        ss.currentGunL = guns[i];
        if(ss.gunL != null) { Destroy(ss.gunL); }
        ss.gunL = GameObject.Instantiate(guns[i].gunPrefab, ss.GunSpawnL.transform.position, guns[i].gunRotation);

        ss.gunL.transform.parent = GameObject.Find("playerHead").transform;
        ss.gunL.transform.localRotation = Quaternion.Euler(ss.gunL.transform.eulerAngles.x, ss.gunL.transform.eulerAngles.y, 0);        //setuje rotaci z na 0 protoze to tweakovalo

        Debug.Log($"Chosen Gun: {ss.currentGunL}");
    }
    public void SelectGunR()
    {
        ss.currentGunR = guns[i];
        if (ss.gunR != null) { Destroy(ss.gunR); }
        ss.gunR = GameObject.Instantiate(guns[i].gunPrefab, ss.GunSpawnR.transform.position, guns[i].gunRotation);

        ss.gunR.transform.parent = GameObject.Find("playerHead").transform;
        ss.gunR.transform.localRotation = Quaternion.Euler(ss.gunR.transform.eulerAngles.x, ss.gunR.transform.eulerAngles.y, 0);        //setuje rotaci z na 0 protoze to tweakovalo
    }

    public void SaveGuns()
    {
        PlayerPrefs.SetString("WeaponL", ss.currentGunL.name);
        PlayerPrefs.SetString("WeaponR", ss.currentGunR.name);          //musi se callnout pred menenim scen
    }
}
