using UnityEngine;

public class MosquitoWings : MonoBehaviour
{

    [SerializeField] private Transform wing1;
    [SerializeField] private Transform wing2;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float rotationAngle;
    private float currentRotation;



    private void Update()
    {
        // Calculate the current rotation using PingPong
        currentRotation = Mathf.PingPong(Time.time * rotationSpeed, rotationAngle * 2) - rotationAngle;

        // Rotate wing1 positively
        wing1.localRotation = Quaternion.Euler(currentRotation, wing1.localRotation.eulerAngles.y, wing1.localRotation.eulerAngles.z);

        // Rotate wing2 in the opposite direction
        wing2.localRotation = Quaternion.Euler(-currentRotation, wing2.localRotation.eulerAngles.y, wing2.localRotation.eulerAngles.z);
    }
}
