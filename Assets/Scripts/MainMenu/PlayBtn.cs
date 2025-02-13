using System.Collections;
using UnityEngine;

public class PlayBtn : MonoBehaviour
{
    public GameObject bg;
    public Animator bgAnimator;
    public Animator cameraAnimator;
    public GameObject GunL;
    public GameObject GunR;


    public void Play()
    {
        bgAnimator.Play("main_menu_transition");
        StartCoroutine(PlayDelayedAnim());
        bg.GetComponent<BackgroundRotation>().enabled = false;
        GunL.GetComponent<BoxCollider>().enabled = true;
        GunR.GetComponent<BoxCollider>().enabled = true;
    }

    IEnumerator PlayDelayedAnim()
    {
        yield return new WaitForSeconds(0.5f);
        cameraAnimator.Play("main_menu_camera_transition");
    }
}
