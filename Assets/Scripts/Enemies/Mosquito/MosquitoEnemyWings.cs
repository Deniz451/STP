using UnityEngine;

public class MosquitoEnemyWings : MonoBehaviour
{
    [SerializeField] private Transform bone; // The bone (wing) to rotate
    [SerializeField] private float angleX = 45f; // The first angle in degrees
    [SerializeField] private float angleY = -45f; // The second angle in degrees
    [SerializeField] private float rotationSpeed = 30f; // Speed at which the rotation changes

    private Vector3 originalRotation; // Store the original rotation

    private void Start()
    {
        // Store the original rotation (as Vector3 for simplicity)
        originalRotation = bone.rotation.eulerAngles;
    }

    private void Update()
    {
        // Use Mathf.PingPong to oscillate between angleY and angleX over time
        float angle = Mathf.PingPong(Time.time * rotationSpeed, angleX - angleY) + angleY;

        // Apply the oscillating angle only on the X axis, keep Y and Z unchanged
        bone.rotation = Quaternion.Euler(angle, originalRotation.y, originalRotation.z);
    }
}
