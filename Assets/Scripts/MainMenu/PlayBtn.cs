using System;
using System.Collections;
using UnityEngine;

public class PlayBtn : MonoBehaviour
{
    public GameObject bg;
    public Animator bgAnimator;
    public CameraController cameraController;

    public Action PlayPressed;


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

        PlayPressed.Invoke();
    }

    IEnumerator PlayDelayedAnim()
    {
        yield return new WaitForSeconds(0.5f);
        cameraController.LerpCameraPos(2, CameraController.CameraPoints.PartSelection);
    }
}
