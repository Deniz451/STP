using System;
using System.Collections;
using UnityEngine;

public class BackBtn : MonoBehaviour
{
    public CameraController cameraController;
    public GameObject bg;
    private Animator bgAnimator;
    private BackgroundRotation bgRotation;

    public Action BackPressed;


    private void Start()
    {
        bgAnimator = bg.GetComponent<Animator>();
        bgRotation = bg.GetComponent<BackgroundRotation>();
        cameraController = GameObject.Find("cameraController").GetComponent<CameraController>();
    }

    public void BackButton()
    {
        StartCoroutine(PlayAnimationBackward("main_menu_transition"));
        cameraController.LerpCameraPos(2, CameraController.CameraPoints.Default);
        BackPressed.Invoke();
    }

    IEnumerator PlayAnimationBackward(string animationName)
    {
        float duration = bgAnimator.GetCurrentAnimatorStateInfo(0).length;
        float time = duration;

        while (time > 0)
        {
            time -= Time.deltaTime;
            bgAnimator.Play(animationName, 0, time / duration);
            yield return null;
        }

        bgAnimator.Play(animationName, 0, 0f);
        bgAnimator.enabled = false;

        bgRotation.enabled = true;
    }
}
