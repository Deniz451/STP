using System.Collections.Generic;
using UnityEngine;

public class BodyFollow : MonoBehaviour
{
    public Transform head;
    public List<Transform> bodySegments;
    public float followSpeed = 5f;
    public float rotationSpeed = 10f;
    public float segmentSpacing = 1f;

    private Vector3[] previousPositions;

    void Start()
    {
        previousPositions = new Vector3[bodySegments.Count + 1];
    }

    void Update()
    {
        previousPositions[0] = head.position;

        for (int i = 0; i < bodySegments.Count; i++)
        {
            Transform segment = bodySegments[i];
            Vector3 targetPos = previousPositions[i];

            segment.position = Vector3.Lerp(segment.position, targetPos, followSpeed * Time.deltaTime);

            Vector3 direction = (targetPos - segment.position).normalized;
            if (direction != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
                Vector3 euler = targetRotation.eulerAngles;
                segment.rotation = Quaternion.Lerp(segment.rotation, Quaternion.Euler(0, 0, euler.z), rotationSpeed * Time.deltaTime);
            }

            previousPositions[i + 1] = segment.position;
        }
    }
}
