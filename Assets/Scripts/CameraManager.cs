using System;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform titleScreenPos;
    public Transform gamePos;
    public Transform shopPos;
    public Transform shopGunLPos;
    public Transform shopGunRPos;
    
    private float titleToGameDuration = 3.0f;
    private float gameToShopDuration = 1.5f;
    private float shopToParts = 1f;
    //private float shopToGameDuration = 4.0f;

    private Camera _camera;
    private bool isLerping = false;
    private float lerpDuration;
    private float lerpTimer;

    private float startSize;
    private float targetedSize;
    private Vector3 startPos;
    private Vector3 targetedPos;
    private Quaternion startRot;
    private Quaternion targetedRot;

    private Action _event;


    private void Awake() {
        _camera = Camera.main;
    }

    private void OnEnable() {
        EventManager.Instance.Subscribe(GameEvents.EventType.GameStart, LerpToGame); 
        EventManager.Instance.Subscribe(GameEvents.EventType.WaveEnd, LerpToShop); 
        EventManager.Instance.Subscribe(GameEvents.EventType.ShopClose, LerpToGame); 
        EventManager.Instance.Subscribe(GameEvents.EventType.SelectedGunL, LerpToShopGunL); 
        EventManager.Instance.Subscribe(GameEvents.EventType.SelectedGunR, LerpToShopGunR); 
        EventManager.Instance.Subscribe(GameEvents.EventType.ShopRightClicked, LerpToShop); 
    } 

    private void OnDestroy() {
        EventManager.Instance.Unsubscribe(GameEvents.EventType.GameStart, LerpToGame);    
        EventManager.Instance.Unsubscribe(GameEvents.EventType.WaveEnd, LerpToShop); 
        EventManager.Instance.Unsubscribe(GameEvents.EventType.ShopClose, LerpToGame); 
        EventManager.Instance.Unsubscribe(GameEvents.EventType.SelectedGunL, LerpToShopGunL); 
        EventManager.Instance.Unsubscribe(GameEvents.EventType.SelectedGunR, LerpToShopGunR); 
        EventManager.Instance.Unsubscribe(GameEvents.EventType.ShopRightClicked, LerpToShop); 

    } 

    private void LerpToGame() {
        startPos = _camera.transform.position;
        targetedPos = gamePos.position;
        startRot = _camera.transform.rotation;
        targetedRot = gamePos.rotation;
        lerpDuration = titleToGameDuration;
        startSize = _camera.orthographicSize;
        targetedSize = 12f;
        lerpTimer = 0;
        _event = () => EventManager.Instance.Publish(GameEvents.EventType.CameraInGameEvent);
        EventManager.Instance.Publish(GameEvents.EventType.CameraToGameEvent);
        isLerping = true;
    }

    public void LerpToShop() {
        startPos = _camera.transform.position;
        targetedPos = shopGunLPos.position;
        startRot = _camera.transform.rotation;
        targetedRot = shopGunLPos.rotation;
        lerpDuration = gameToShopDuration;
        startSize = _camera.orthographicSize;
        targetedSize = 2.2f;
        lerpTimer = 0;
        _event = () => EventManager.Instance.Publish(GameEvents.EventType.CameraInShopEvent);
        EventManager.Instance.Publish(GameEvents.EventType.CameraToShopEvent);
        isLerping = true;
    }

    public void LerpToShopGunL() {
        startPos = _camera.transform.position;
        targetedPos = shopGunLPos.position;
        startRot = _camera.transform.rotation;
        targetedRot = shopGunLPos.rotation;
        lerpDuration = shopToParts;
        startSize = _camera.orthographicSize;
        targetedSize = 1f;
        lerpTimer = 0;
        isLerping = true;
    }

    public void LerpToShopGunR() {
        startPos = _camera.transform.position;
        targetedPos = shopGunRPos.position;
        startRot = _camera.transform.rotation;
        targetedRot = shopGunRPos.rotation;
        lerpDuration = shopToParts;
        startSize = _camera.orthographicSize;
        targetedSize = 1f;
        lerpTimer = 0;
        isLerping = true;
    }

    private void Update() {
        if (!isLerping) return;

        lerpTimer += Time.deltaTime;
        float t = Mathf.Clamp01(lerpTimer / lerpDuration);
        float easedT = Mathf.SmoothStep(0f, 1f, t);

        _camera.transform.SetPositionAndRotation(
            Vector3.Lerp(startPos, targetedPos, easedT),
            Quaternion.Slerp(startRot, targetedRot, easedT));

        _camera.orthographicSize = Mathf.Lerp(startSize, targetedSize, easedT);

        if (t >= 1f)
        {
            isLerping = false;
            _event?.Invoke();
        }
    }
}
