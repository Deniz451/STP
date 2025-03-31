using UnityEngine;

public class PartSelection : MonoBehaviour
{
    [SerializeField] private SelectedWeaponsSO selectedWeaponsSO;
    [SerializeField] private Transform gunLHolder, gunRHolder, gunLPreview, gunRPreview;
    [SerializeField] private GameObject[] gunPrefabs;
    [SerializeField] private GameObject playBtn, backBtn, launchBtn, hintTxt;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private Material outlineMAT;
    [SerializeField] private GunSO gun1, gun2;

    private GameObject currentLGun, currentRGun, previewGun;
    private bool isPreviewing, isPreviewingLeft;


    private void Start()
    {
        InstantiateGun(gunLHolder, selectedWeaponsSO.gunL.gunName, ref currentLGun);
        InstantiateGun(gunRHolder, selectedWeaponsSO.gunR.gunName, ref currentRGun);

        playBtn.GetComponent<PlayBtn>().PlayPressed += EnableGunColliders;
        backBtn.GetComponent<BackBtn>().BackPressed += DisableGunColliders;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && isPreviewing) ExitPreview();
    }

    private void InstantiateGun(Transform holder, string gunName, ref GameObject currentGun)
    {
        var gun = Instantiate(gunPrefabs[gunName == "Gun1" ? 0 : 1], holder);
        gun.transform.localPosition = Vector3.zero;
        gun.transform.localRotation = Quaternion.identity;

        var collider = gun.AddComponent<BoxCollider>();
        collider.enabled = false;

        var part = gun.AddComponent<SelectablePart>();
        part.outliner = outlineMAT;
        part.clicked += PreviewParts;

        currentGun = gun;
    }

    private void InstantiatePreviewGun(Transform holder, string gunName)
    {
        var gun = Instantiate(gunPrefabs[gunName == "Gun1" ? 0 : 1], holder);
        gun.transform.localPosition = Vector3.zero;
        gun.transform.localRotation = Quaternion.identity;

        var collider = gun.AddComponent<BoxCollider>();
        collider.enabled = true;

        var preview = gun.AddComponent<PreviewPart>();
        preview.outliner = outlineMAT;
        preview.clicked = SwitchPart;

        previewGun = gun;
    }

    private void SwitchPart(PreviewPart part)
    {
        if (isPreviewingLeft)
        {
            ReplaceGun(ref currentLGun, gunLHolder, gunLPreview, ref selectedWeaponsSO.gunL);
        }
        else
        {
            ReplaceGun(ref currentRGun, gunRHolder, gunRPreview, ref selectedWeaponsSO.gunR);
        }
    }

    private void ReplaceGun(ref GameObject currentGun, Transform holder, Transform previewHolder, ref GunSO selectedGun)
    {
        Destroy(currentGun);
        Destroy(previewGun);

        selectedGun = selectedGun == gun1 ? gun2 : gun1;
        InstantiateGun(holder, selectedGun.gunName, ref currentGun);
        InstantiatePreviewGun(previewHolder, selectedGun == gun1 ? "Gun2" : "Gun1");
    }

    private void PreviewParts(SelectablePart part)
    {
        isPreviewing = true;
        hintTxt.SetActive(true);
        Destroy(previewGun);
        backBtn.SetActive(false);
        launchBtn.SetActive(false);

        if (part.gameObject == currentLGun)
        {
            isPreviewingLeft = true;
            SetGunColliders(false, true);
            cameraController.LerpCameraPos(2, CameraController.CameraPoints.GunLInspect, 1.75f);
            InstantiatePreviewGun(gunLPreview, selectedWeaponsSO.gunL.gunName == "Gun1" ? "Gun2" : "Gun1");
        }
        else
        {
            isPreviewingLeft = false;
            SetGunColliders(true, false);
            cameraController.LerpCameraPos(2, CameraController.CameraPoints.GunRInspect, 1.75f);
            InstantiatePreviewGun(gunRPreview, selectedWeaponsSO.gunR.gunName == "Gun1" ? "Gun2" : "Gun1");
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

    private void EnableGunColliders() => SetGunColliders(true, true);
    private void DisableGunColliders() => SetGunColliders(false, false);

    private void SetGunColliders(bool left, bool right)
    {
        currentLGun.GetComponent<BoxCollider>().enabled = left;
        currentRGun.GetComponent<BoxCollider>().enabled = right;
    }
}
