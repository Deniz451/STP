using UnityEngine;

public class SpineTest : MonoBehaviour
{
    public Transform[] bodyParts;
    public Transform head;
    private Vector3[] originalPositions;

    private void Start()
    {
        originalPositions = new Vector3[bodyParts.Length];  // Initialize array based on bodyParts length

        for (int i = 0; i < bodyParts.Length; i++)
        {
            originalPositions[i] = bodyParts[i].position;
        }
    }

    private void Update()
    {
        for (int i = 0; i < bodyParts.Length; i++)
        {
            bodyParts[i].position = originalPositions[i];
        }
    }
}
