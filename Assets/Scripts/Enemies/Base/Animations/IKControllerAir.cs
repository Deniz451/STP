using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class IKControllerAir : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private Transform defaultLegPosition;
    [SerializeField] private float distanceCap;
    [SerializeField] private float offset;
    private Vector3 targetPos;

    private void Start()
    {
        targetPos = target.position;
    }

    private void Update()
    {
        target.position = targetPos;

        if (Vector3.Distance(defaultLegPosition.position, targetPos) >= distanceCap)
        {
            targetPos = new(defaultLegPosition.position.x, defaultLegPosition.position.y, defaultLegPosition.position.z + offset);
        }
    }


}
