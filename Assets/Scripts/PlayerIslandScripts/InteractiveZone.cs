using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractiveZone : MonoBehaviour
{
    [SerializeField] GameObject anchorPoint;

    [SerializeField] GameObject textPrompt;
    [SerializeField] GameObject playBtn;
    [SerializeField] GameObject[] btns;

    [SerializeField] Camera mainCam;
    [SerializeField] Camera shedCam;
    
    GameObject player;
    bool inside = false;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        shedCam.enabled = false;
        mainCam.enabled = true;
    }
   
    void Update()
    {
        textPrompt.SetActive(inside);

        if (inside && Input.GetKeyDown(KeyCode.E))
        {
            player.transform.position = anchorPoint.transform.position;
            
            player.GetComponent<CharControllerNew>().enabled = false;
            player.GetComponentInChildren<CharRotation>().enabled = false;
            player.GetComponentInChildren<ShootingScript>().enabled = false;

            foreach (GameObject btn in btns)
            {
                btn.SetActive(true);
            }
            playBtn.SetActive(false);

            SwitchCams();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        { 
            inside = true; 
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player")) 
        { 
            inside = false; 
        }
    }

    public void HideBtns()
    {
        foreach (GameObject btn in btns)
        {
            btn.SetActive(false);
        }
        playBtn.SetActive(true);
        SwitchCams();
    }

    private void SwitchCams()
    {
        shedCam.enabled = !shedCam.enabled;
        mainCam.enabled = !mainCam.enabled;
    }
}
