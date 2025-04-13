using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [Header("Player Part Holders")]
    public GameObject gunLPlayer;
    public GameObject gunRPlayer;

    [Header("Shop Camera Anchors")]
    public Transform gunLCamera;
    public Transform gunRCamera;

    public List<ShopItem> weapons = new();

    private bool isZoomedToPart = false;


    private void OnEnable() {
        EventManager.Instance.Subscribe(GameEvents.EventType.WaveEnd, OpenShop);
        EventManager.Instance.Subscribe(GameEvents.EventType.ShopContinueBtnClick, ShopClose);
    }

    private void OnDestroy() {
        EventManager.Instance.Unsubscribe(GameEvents.EventType.WaveEnd, OpenShop);
        EventManager.Instance.Unsubscribe(GameEvents.EventType.ShopContinueBtnClick, ShopClose);
    }

    private void Start() {
        gunLPlayer.GetComponent<PlayerPartHolder>().OnChangeablePartClicked += () => { 
            isZoomedToPart = true; 
            EventManager.Instance.Publish(GameEvents.EventType.SelectedGunL); 
            EventManager.Instance.Publish(GameEvents.EventType.ClickedChangeablePart); 
        };

        gunRPlayer.GetComponent<PlayerPartHolder>().OnChangeablePartClicked += () => { 
            isZoomedToPart = true; 
            EventManager.Instance.Publish(GameEvents.EventType.SelectedGunR); 
            EventManager.Instance.Publish(GameEvents.EventType.ClickedChangeablePart); 
        };
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Mouse1) && isZoomedToPart) {
            isZoomedToPart = false;
            EventManager.Instance.Publish(GameEvents.EventType.ShopRightClicked);
            GameObject.Find("CameraManager").GetComponent<CameraManager>().LerpToShop();
        }
    }

    private void OpenShop() {
        EventManager.Instance.Publish(GameEvents.EventType.ShopOpen);
    }

    private void ShopClose() {
        EventManager.Instance.Publish(GameEvents.EventType.ShopClose);
    }
}
