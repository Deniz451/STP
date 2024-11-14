using UnityEngine;

public class IKController : MonoBehaviour
{
    [Header("Leg Settings")]
    public AnimationCurve legMovementCurve; // curve, at which the leg moves
    public float distanceCap; // the distance at which the leg moves to new position
    public float speedFactor; // speed factor of how fast the leg moves

    [HideInInspector] public Transform target; // the target gameobject that the last bone of the leg follows
    [HideInInspector] public Transform raycast; // the point from where the raycast is shot to determine the position of the target position
    private Vector3 targetPos; // position on the ground the leg should stick to
    private Vector3 raycastHitPos; // the position where raycast hit the ground and the leg should move to
    private Vector3 rayOrigin; // position from which the raycast is shot
    private Vector3 rayDirection; // the direction at which the raycast is shot
    private bool isOnGround = true;
    private float t = 0f;

    [Header("Gizmo Settings")]
    [SerializeField] private bool Raycast = true;
    [SerializeField] private bool LegStickingPosition = true;
    [SerializeField] private bool EndBoneFollowPosition = true;
    [SerializeField] private bool RaycastHitPoint = true;


    private void Start()
    {
        targetPos = target.position; // initialy set up the the lockOn position on the position of the leg by default
    }


    private void Update()
    {
        ControlLeg();
        ShootRaycast();
    }

    private void ControlLeg()
    {
        // if the leg is on ground and doesnt move, it should stick to the target position
        if (isOnGround)
        {
            target.position = targetPos;
        }
        // if the leg should move it moves in a curve to the new target position over time
        else
        {
            t += Time.deltaTime * speedFactor;
            Vector3 newPosition = Vector3.Lerp(target.position, targetPos, t);

            float curveValue = legMovementCurve.Evaluate(t);
            newPosition.y += curveValue;

            target.position = newPosition;

            if (Vector3.Distance(target.position, targetPos) <= 0.1)
            {
                target.position = targetPos;
                isOnGround = true;
                t = 0f;
            }

            //target.position = targetPos;
        }
    }

    private void ShootRaycast()
    {
        rayOrigin = raycast.position;
        rayDirection = -raycast.up;

        // shooting the raycast
        RaycastHit hit;
        if (Physics.Raycast(rayOrigin, rayDirection, out hit, 10f))
        {
            // storing the hit position
            raycastHitPos = hit.point;
        }

        // if the distance from the leg to the raycast hit gets too large, call function
        if (Vector3.Distance(target.position, raycastHitPos) >= distanceCap) ChangeTargetPosition();
    }

    // changes the target position to the current position of the raycast and tells the leg to move by boolean
    private void ChangeTargetPosition()
    {
        targetPos = raycastHitPos;
        isOnGround = false;
    }

    void OnDrawGizmos()
    {
        if (Raycast)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(rayOrigin, rayOrigin + rayDirection * 10f);
        }
        if (LegStickingPosition)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(targetPos, 0.35f);
        }
        if (EndBoneFollowPosition)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(target.position, 0.15f);
        }
        if (RaycastHitPoint)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(raycastHitPos, 0.075f);
        }
    }
}