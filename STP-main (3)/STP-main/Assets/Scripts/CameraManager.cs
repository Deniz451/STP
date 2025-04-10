using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform titleScreenPos;
    public Transform gamePos;
    public Transform shopPos;
    private Camera _camera;
    private bool isLerping = false;
    private float titleToGameDuration = 5.0f;
    private float gameToShopDuration = 2.5f;
    //private float shopToGameDuration = 4.0f;
    private float lerpDuration;
    private float targetedSize;
    private float lerpStartTime;
    private Vector3 targetedPos;
    private Quaternion targetedRot;

    private void Awake() {
        _camera = Camera.main;
    }

    private void OnEnable() {
        EventManager.Instance.Subscribe(GameEvents.EventType.GameStart, LerpToGame); 
        EventManager.Instance.Subscribe(GameEvents.EventType.WaveEnd, LerpToShop); 
        EventManager.Instance.Subscribe(GameEvents.EventType.ShopClose, LerpToGame); 
    } 

    private void OnDestroy() {
        EventManager.Instance.Unsubscribe(GameEvents.EventType.GameStart, LerpToGame);    
        EventManager.Instance.Unsubscribe(GameEvents.EventType.WaveEnd, LerpToShop); 
        EventManager.Instance.Unsubscribe(GameEvents.EventType.ShopClose, LerpToGame); 
    } 

    private void LerpToGame() {
        targetedPos = gamePos.position;
        targetedRot = gamePos.rotation;
        lerpDuration = titleToGameDuration;
        targetedSize = 12f;
        lerpStartTime = Time.time;
        isLerping = true;
    }

    private void LerpToShop() {
        targetedPos = shopPos.position;
        targetedRot = shopPos.rotation;
        lerpDuration = gameToShopDuration;
        targetedSize = 2.2f;
        lerpStartTime = Time.time;
        isLerping = true;
    }
    private void Update() {
        if (!isLerping) return;

        float t = Mathf.Clamp01((Time.time - lerpStartTime) / lerpDuration);
        float easedT = Mathf.SmoothStep(0f, 1f, t);

        _camera.transform.SetPositionAndRotation(
                Vector3.Lerp(_camera.transform.position, targetedPos, easedT), 
                Quaternion.Lerp(_camera.transform.rotation, targetedRot, easedT));
        _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, targetedSize, easedT);

        if (t >= 1.0f) 
        {
            isLerping = false;
            _camera.transform.SetPositionAndRotation(targetedPos, targetedRot);
            _camera.orthographicSize = targetedSize;
        }
    }
}
