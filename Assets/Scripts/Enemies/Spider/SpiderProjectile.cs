using Unity.Mathematics;
using UnityEngine;

public class SpiderProjectile : MonoBehaviour
{

    [SerializeField] private GameObject webPlanePrefab;
    [SerializeField] private float projectileSpeed;
    [HideInInspector] public Vector3 targetPosition;
    private Vector3 startPos;
    private Vector3 midPoint;
    private float t = 0;


    private void Start()
    {
        startPos = transform.position;
        midPoint =  CalculateMidPoint();
    }

    private void Update()
    {
        t += Time.deltaTime * projectileSpeed;
        t = Mathf.Clamp(t, 0f, 1f);

        Vector3 ab = Vector3.Lerp(startPos, midPoint, t);
        Vector3 bc = Vector3.Lerp(midPoint, targetPosition, t);

        transform.position = Vector3.Lerp(ab, bc, t);
    }

    private Vector3 CalculateMidPoint()
    {
        Vector3 midpoint = (startPos + targetPosition) / 2;
        midpoint.y += 50;
        return midpoint;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Instantiate(webPlanePrefab, transform.position, quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 previousPoint = startPos;
        for (float i = 0; i <= 1; i += 0.05f)
        {
            Vector3 ab = Vector3.Lerp(startPos, midPoint, i);
            Vector3 bc = Vector3.Lerp(midPoint, targetPosition, i);
            Vector3 pointOnCurve = Vector3.Lerp(ab, bc, i);

            Gizmos.DrawLine(previousPoint, pointOnCurve);
            previousPoint = pointOnCurve;
        }
    }
}
