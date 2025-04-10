using UnityEngine;

public class IKControllerAir : MonoBehaviour
{
    public Transform target;
    public Rigidbody rb;
    public float maxOffset = 2f; // Max distance from target
    public float returnSpeed = 2f; // Speed of returning to target position

    private Vector3 offset;

    void Start()
    {
        if (!target) Debug.LogError("Target not assigned.");
        if (!rb) Debug.LogError("Rigidbody not assigned.");
        offset = transform.position - target.position;
    }

    void Update()
    {
        if (!target || !rb) return;

        float velocityMagnitude = rb.velocity.magnitude;
        Vector3 directionAway = (transform.position - target.position).normalized;
        Vector3 dynamicOffset = directionAway * Mathf.Clamp(velocityMagnitude, 0, maxOffset);

        Vector3 desiredPosition = target.position + offset + dynamicOffset;
        transform.position = Vector3.Lerp(transform.position, desiredPosition, returnSpeed * Time.deltaTime);
    }
}
