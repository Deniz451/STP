using UnityEngine;

public class IKControllerAir : MonoBehaviour
{
    [SerializeField] private Transform parentObject;
    [SerializeField] private float distanceCap;
    [SerializeField] private float followSpeed;
    [SerializeField] private float returnSpeed;
    [SerializeField] private float distanceFactor;
    private Vector3 legVelocity;
    private Vector3 originalLegPosition;
    private Vector3 lastParentPosition;
    private bool parentChangedPos = false;


    void Start()
    {
        originalLegPosition = transform.localPosition;
        lastParentPosition = parentObject.position;
    }

    void Update()
    {
        if (parentObject.position != lastParentPosition)
        {
            parentChangedPos = true;
            lastParentPosition = parentObject.position;
        }
        else parentChangedPos = false;

        if (parentChangedPos && Vector3.Distance(transform.position, parentObject.position) < distanceCap)
        {
            Vector3 targetPosition = transform.position - parentObject.transform.forward * distanceFactor;
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref legVelocity, 1 / followSpeed);
        }
        else
        {
            Vector3 returnPosition = parentObject.TransformPoint(originalLegPosition);
            transform.position = Vector3.SmoothDamp(transform.position, returnPosition, ref legVelocity, 1 / returnSpeed);
        }

        parentChangedPos = false;
    }
}
