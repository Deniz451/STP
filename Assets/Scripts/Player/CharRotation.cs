using System.Collections;
using UnityEngine;

public class CharRotation : MonoBehaviour
{
    Vector3 mousePosition;
    public LayerMask mask;
    private bool canRotate;


    private void OnEnable() {
        EventManager.Instance.Subscribe(GameEvents.EventType.PlayerEnabled, () => canRotate = true);
        EventManager.Instance.Subscribe(GameEvents.EventType.PlayerDisabled, () => canRotate = false);
        EventManager.Instance.Subscribe(GameEvents.EventType.ShopOpen, LerpToDefaultCaller);
    }

    private void OnDestroy() {
        EventManager.Instance.Unsubscribe(GameEvents.EventType.PlayerEnabled, () => canRotate = true);
        EventManager.Instance.Unsubscribe(GameEvents.EventType.PlayerDisabled, () => canRotate = false);
        EventManager.Instance.Unsubscribe(GameEvents.EventType.ShopOpen, LerpToDefaultCaller);
    }

    void Update()
    {
        if (!canRotate) return;

        mousePosition = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hitInfo, Mathf.Infinity, mask))
        {
            Vector3 pointToLook = hitInfo.point;
            Vector3 direction = pointToLook - transform.position;
            direction.y = 0;
            Debug.DrawRay(transform.position, direction);
            transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        }
    }

    private void LerpToDefaultCaller() { StartCoroutine(LerpToDefault()); }

    private IEnumerator LerpToDefault() {
        Quaternion startRot = transform.rotation;
        Quaternion endRot = Quaternion.Euler(0, 0, 0);

        float angle = Quaternion.Angle(startRot, endRot);
        float duration = angle / 180f;
        float elapsedTime = 0f;

        while (elapsedTime < duration) {
            float t = elapsedTime / duration;
            transform.rotation = Quaternion.Slerp(startRot, endRot, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.rotation = endRot;
    }
}
