using UnityEngine;

public class CameraManager : MonoBehaviour
{
    private Camera _camera;
    public Transform titleScreenPos;
    public Transform gamePos;
    private bool isLerping = false;
    private float lerpDuration = 5.0f;
    private float lerpStartTime;

    private void Awake() {
        _camera = Camera.main;
    }

    private void OnEnable() {
        EventManager.Instance.Subscribe(GameEvents.EventType.GameStart, StartLerpToGamePos); 
    } 

    private void OnDestroy() {
        EventManager.Instance.Unsubscribe(GameEvents.EventType.GameStart, StartLerpToGamePos);    
    } 

    private void StartLerpToGamePos() {
        lerpStartTime = Time.time;
        isLerping = true;
    }

    private void Update() {
        if (!isLerping) return;

        float t = Mathf.Clamp01((Time.time - lerpStartTime) / lerpDuration);
        float easedT = Mathf.SmoothStep(0f, 1f, t);

        _camera.transform.SetPositionAndRotation(
                Vector3.Lerp(_camera.transform.position, gamePos.position, easedT), 
                Quaternion.Lerp(_camera.transform.rotation, gamePos.rotation, easedT));
        _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, 12, easedT);

        if (t >= 1.0f) 
        {
            isLerping = false;
            _camera.transform.SetPositionAndRotation(gamePos.position, gamePos.rotation);
            _camera.orthographicSize = 12;
        }
    }
}
