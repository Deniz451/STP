using UnityEngine;

public class IKController : MonoBehaviour
{
    [Header("Leg Settings")]
    public float distanceCap; // the distance at which the leg moves
    public float curveHeight; // how high the leg moves
    public float speedFactor; // speed factor of the individual leg
    public Transform target; // the target gameobject that the last bone of the leg follows
    public Transform raycast; // the point from where the raycast is shot to determine the position of the target position

    private Vector3 targetPos; // position on the ground the leg should stick to
    private Vector3 raycastHitPos; // the position where raycast hit the ground and the leg should move to
    private bool isOnGround = true;
    private float t = 0f;


    [Header("Gizmo Settings")]
    [SerializeField] private bool raycastLine = true;
    [SerializeField] private bool raycastHitPoint = true;
    [SerializeField] private bool lockOnPosition = true;



    private void Start()
    {
        targetPos = target.position; // initialy set up the the lockOn position on the position of the lef by default
    }

    private void Update()
    {
        if (isOnGround)
        {
            target.position = targetPos;
        }
        else
        {
            t += Time.deltaTime * speedFactor;
            Vector3 newPosition = Vector3.Lerp(target.position, targetPos, t);

            float curve = Mathf.Sin(t * Mathf.PI) * curveHeight;
            newPosition.y += curve;

            target.position = newPosition;

            if (Vector3.Distance(target.position, targetPos) <= 0.1)
            {
                isOnGround = true;
                t = 0f;
            }
        }
    }

    void OnDrawGizmos()
    {
        Vector3 rayOrigin = raycast.position;

        Vector3 rayDirection = -raycast.up;

        if (raycastLine)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(rayOrigin, rayOrigin + rayDirection * 10f);
        }

        RaycastHit hit;
        if (Physics.Raycast(rayOrigin, rayDirection, out hit, 10f))
        {
            if (raycastHitPoint)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(hit.point, 0.35f);
            }

            raycastHitPos = hit.point;
        }

        if (lockOnPosition)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(targetPos, 0.35f);
        }

        if (Vector3.Distance(target.position, raycastHitPos) >= distanceCap) ChangeTargetPosition();
    }

    private void ChangeTargetPosition()
    {
        targetPos = raycastHitPos; // if the distance gets too large, change the lockOn position to the new position of the raycast
        isOnGround = false;
    }
}