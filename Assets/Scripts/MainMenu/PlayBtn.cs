using System.Collections;
using UnityEngine;

public class PlayBtn : MonoBehaviour
{
    public GameObject bg;
    public Animator bgAnimator;
    public CameraController cameraController;
    public GameObject GunL;
    public GameObject GunR;


    private void Start()
    {
        cameraController = GameObject.Find("cameraController").GetComponent<CameraController>();
    }

    public void Play()
    {
        bgAnimator.enabled = true;
        bgAnimator.Play("main_menu_transition");
        StartCoroutine(PlayDelayedAnim());
        bg.GetComponent<BackgroundRotation>().enabled = false;
        GunL.GetComponent<BoxCollider>().enabled = true;
        GunR.GetComponent<BoxCollider>().enabled = true;
    }

    IEnumerator PlayDelayedAnim()
    {
        yield return new WaitForSeconds(0.5f);
        cameraController.LerpCameraPos(2, CameraController.CameraPoints.PartSelection);
    }
}
