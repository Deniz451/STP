using UnityEngine;

public class BackgroundRotation : MonoBehaviour
{
    [SerializeField] private float maxRotationAngle = 45f; // Maximum rotation angle (in degrees)
    [SerializeField] private float rotationSpeed = 5f; // Speed of rotation

    private void Update()
    {
        // Get the mouse position as a normalized value (0 to 1)
        float mouseXNormalized = Input.mousePosition.x / Screen.width;

        // Calculate the target angle based on the mouse position
        float targetAngle = Mathf.Lerp(-maxRotationAngle, maxRotationAngle, mouseXNormalized);

        // Smoothly rotate towards the target angle
        Quaternion targetRotation = Quaternion.Euler(0, targetAngle, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}
