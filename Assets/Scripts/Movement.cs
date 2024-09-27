using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{

    [Header("Body Height Settings")]
    [SerializeField] private float smoothingSpeed = 10f; // how fast the body moves to new y position

    [Header("Body Rotation Settings")]
    [SerializeField] private Transform left;
    [SerializeField] private Transform right;
    [SerializeField] private bool rotateBody;

    // Settings for custom path following
    [Header("Path Settings")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private Transform[] Points;
    [SerializeField] private bool followPath;
    private int pointIndex;

    // Settings for the navmesh, picking random location
    [Header("Navmesh Settings")]
    [SerializeField] private float patrolRadius;
    [SerializeField] private float waitTime;
    [SerializeField] private NavMeshAgent agent;
    private Vector3 currentDestination;
    private bool isWaiting = false;



    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        StartCoroutine(PatrolRoutine());
    }

    private void Update()
    {
        /*GetAverageHeight();
        Vector3 newPosition = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, targetHeight, Time.deltaTime * smoothingSpeed), transform.position.z);
        if (Mathf.Abs(newPosition.y - transform.position.y) > heightChangeThreshold) transform.position = newPosition;*/

        if (rotateBody) RotateBody();

        if (pointIndex < Points.Length && Points.Length != 0 && followPath)
        {
            if (pointIndex <=  Points.Length - 1)
            {
                transform.position = Vector3.MoveTowards(transform.position, Points[pointIndex].position, moveSpeed * Time.deltaTime);
                if (Vector3.Distance(transform.position, Points[pointIndex].position) <= 0.1) pointIndex++;

            }
        }
    }


    private void RotateBody()
    {
        // ROTATE TOWARDS THE ACTIVE PATH POINT 
        /*Vector3 direction = new Vector3(Points[pointIndex].position.x - transform.position.x, 0, Points[pointIndex].position.z - transform.position.z);
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Euler(0, targetRotation.eulerAngles.y, 0);*/

        
        // VERSION 1
        //transform.rotation = Quaternion.Euler(Mathf.Atan((left.position.y - right.position.y) / Vector3.Distance(left.position, right.position)) * Mathf.Rad2Deg, 270, 0);

        // VERSION 2
        Vector3 legDirection = right.position - left.position;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            Vector3 surfaceNormal = hit.normal;
            Quaternion targetRotation = Quaternion.FromToRotation(transform.up, surfaceNormal) * transform.rotation;
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * smoothingSpeed);
        }

        // VERSION 3
        /*Vector3 pointA = legs[0].transform.position;
        Vector3 pointB = legs[1].transform.position;
        Vector3 pointC = legs[2].transform.position;

        Vector3 AB = pointB - pointA;
        Vector3 AC = pointC - pointA;

        Vector3 planeNormal = Vector3.Cross(AB, AC).normalized;

        Quaternion planeRotation = Quaternion.FromToRotation(Vector3.up, planeNormal);

        transform.rotation = planeRotation;*/

        // VERSION 4 
        /*Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit info = new RaycastHit();
        Quaternion RotationRef = Quaternion.Euler(0, 0, 0);

        if (Physics.Raycast(ray, out info, WhatIsGround))
        {
            RotationRef = Quaternion.Lerp(transform.rotation, Quaternion.FromToRotation(Vector3.up, info.normal), animCurve.Evaluate(time));
            transform.rotation = Quaternion.Euler(RotationRef.eulerAngles.x, transform.eulerAngles.y, RotationRef.eulerAngles.z);
        }*/
    }

   private  IEnumerator PatrolRoutine()
    {
        while (true)
        {
            if (!isWaiting)
            {
                Vector3 randomDestination = GetRandomPoint(transform.position, patrolRadius);

                if (randomDestination != Vector3.zero)
                {
                    agent.SetDestination(randomDestination);
                    isWaiting = true;

                    yield return new WaitUntil(() => !agent.pathPending && agent.remainingDistance <= agent.stoppingDistance);

                    yield return new WaitForSeconds(waitTime);

                    isWaiting = false;
                }
            }

            yield return null;
        }
    }

    private Vector3 GetRandomPoint(Vector3 center, float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += center;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, NavMesh.AllAreas))
        {
            return hit.position;
        }

        return Vector3.zero;
    }

    private void OnDrawGizmos()
    {
        if (agent != null && currentDestination != Vector3.zero)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, currentDestination);

            Gizmos.color = Color.red;
            Gizmos.DrawSphere(currentDestination, 0.5f);
        }
    }
}
