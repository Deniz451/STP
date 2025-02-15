using Unity.VisualScripting;
using UnityEngine;

public class PartSelection : MonoBehaviour
{
    private SelectablePart[] parts;
    public SelectedWeaponsSO selectedWeaponsSO;

    public Transform gunLHolder;
    public Transform gunRHolder;
    public Transform gunLPreview;
    public Transform gunRPreview;

    public GameObject[] gunPrefabs; // 0 - Gun1 | 1 - Gun2

    public GameObject playBtn;
    public GameObject backBtn;
    public GameObject launchBtn;

    private GameObject currentLGun;
    private GameObject currentRGun;
    private GameObject previewGun;

    public CameraController cameraController;

    public Material outlineMAT;

    public bool isPreviewing = false;
    public bool isPreviewingLeft = false;

    public GunSO gun1;
    public GunSO gun2;

    public GameObject hintTxt;


    private void Start()
    {
        InstantiateGun(gunLHolder, selectedWeaponsSO.gunL.gunName);
        InstantiateGun(gunRHolder, selectedWeaponsSO.gunR.gunName);

        playBtn.GetComponent<PlayBtn>().PlayPressed += EnableGunColliders;
        backBtn.GetComponent<BackBtn>().BackPressed += DisableGunColliders;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && isPreviewing) ExitPreview();
    }

    void InstantiateGun(Transform holder, string gunName)
    {
        var gun = Instantiate(gunPrefabs[gunName == "Gun1" ? 0 : 1], holder).transform;
        gun.localPosition = Vector3.zero;
        gun.localRotation = Quaternion.identity;

        gun.AddComponent<BoxCollider>();
        gun.GetComponent<BoxCollider>().enabled = false;
        gun.AddComponent<SelectablePart>();
        gun.GetComponent<SelectablePart>().outliner = outlineMAT;
        gun.GetComponent<SelectablePart>().clicked += PreviewParts;

        if (holder == gunLHolder) currentLGun = gun.gameObject;
        else currentRGun = gun.gameObject;
    }

    void InstantiatePreviewGun(Transform holder, string gunName)
    {
        var gun = Instantiate(gunPrefabs[gunName == "Gun1" ? 0 : 1], holder).transform;
        gun.localPosition = Vector3.zero;
        gun.localRotation = Quaternion.identity;

        gun.AddComponent<BoxCollider>();
        gun.GetComponent<BoxCollider>().enabled = true;
        gun.AddComponent<PreviewPart>();
        gun.GetComponent<PreviewPart>().outliner = outlineMAT;
        gun.GetComponent<PreviewPart>().clicked = SwitchPart;

        previewGun = gun.gameObject;
    }

    private void SwitchPart(PreviewPart part)
    {
        if (isPreviewingLeft)
        {
            Destroy(currentLGun);
            Destroy(previewGun);

            if (selectedWeaponsSO.gunL == gun1)
            {
                selectedWeaponsSO.gunL = gun2;
                InstantiateGun(gunLHolder, selectedWeaponsSO.gunL.gunName);
                InstantiatePreviewGun(gunLPreview, "Gun1");
            }
            else
            {
                selectedWeaponsSO.gunL = gun1;
                InstantiateGun(gunLHolder, selectedWeaponsSO.gunL.gunName);
                InstantiatePreviewGun(gunLPreview, "Gun2");
            }
        }
        else
        {
            Destroy(currentRGun);
            Destroy(previewGun);

            if (selectedWeaponsSO.gunR == gun1)
            {
                selectedWeaponsSO.gunR = gun2;
                InstantiateGun(gunRHolder, selectedWeaponsSO.gunR.gunName);
                InstantiatePreviewGun(gunRPreview, "Gun1");
            }
            else
            {
                selectedWeaponsSO.gunR = gun1;
                InstantiateGun(gunRHolder, selectedWeaponsSO.gunR.gunName);
                InstantiatePreviewGun(gunRPreview, "Gun2");
            }
        }
    }


    private void PreviewParts(SelectablePart part)
    {
        isPreviewing = true;

        hintTxt.SetActive(true);

        if (previewGun != null)
            Destroy(previewGun);

        backBtn.SetActive(false);
        launchBtn.SetActive(false);

        if (part.gameObject == currentLGun)
        {
            isPreviewingLeft = true;

            currentLGun.GetComponent<BoxCollider>().enabled = false;
            currentRGun.GetComponent<BoxCollider>().enabled = true;
            cameraController.LerpCameraPos(2, CameraController.CameraPoints.GunLInspect, 1.75f);

            if (selectedWeaponsSO.gunL.gunName == "Gun1")
                InstantiatePreviewGun(gunLPreview, "Gun2");
            else
                InstantiatePreviewGun(gunLPreview, "Gun1");
        }
        else
        {
            isPreviewingLeft = false;

            currentRGun.GetComponent<BoxCollider>().enabled = false;
            currentLGun.GetComponent<BoxCollider>().enabled = true;
            cameraController.LerpCameraPos(2, CameraController.CameraPoints.GunRInspect, 1.75f);

            if (selectedWeaponsSO.gunR.gunName == "Gun1")
                InstantiatePreviewGun(gunRPreview, "Gun2");
            else
                InstantiatePreviewGun(gunRPreview, "Gun1");
        }
    }

    private void ExitPreview()
    {
        isPreviewing = false;

        hintTxt.SetActive(false);

        Destroy(previewGun);

        backBtn.SetActive(true);
        launchBtn.SetActive(true);

        EnableGunColliders();

        cameraController.LerpCameraPos(2, CameraController.CameraPoints.PartSelection, 5.5f);
    }

    private void EnableGunColliders()
    {
        currentLGun.GetComponent<BoxCollider>().enabled = true;
        currentRGun.GetComponent<BoxCollider>().enabled = true;
    }

    private void DisableGunColliders()
    {
        currentLGun.GetComponent<BoxCollider>().enabled = false;
        currentRGun.GetComponent<BoxCollider>().enabled = false;
    }

}
