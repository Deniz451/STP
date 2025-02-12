using System.Collections.Generic;
using UnityEngine;

public class SpineTest : MonoBehaviour
{
    public Transform head; // The first bone (head)
    public List<Transform> spineBones; // The bones in order from head to tail
    public float rotationSpeed = 10f;

    void Update()
    {
        for (int i = 1; i < spineBones.Count; i++) // Start from 1 to avoid modifying the head
        {
            Transform currentBone = spineBones[i];
            Transform previousBone = spineBones[i - 1];

            // Get local direction to previous bone
            Vector3 direction = (previousBone.position - currentBone.position).normalized;

            // Convert direction to local space
            Vector3 localDirection = currentBone.parent.InverseTransformDirection(direction);

            // Get target rotation (only around Z-axis)
            Quaternion targetRotation = Quaternion.FromToRotation(Vector3.right, localDirection);

            // Apply rotation smoothly
            currentBone.localRotation = Quaternion.Lerp(currentBone.localRotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}
