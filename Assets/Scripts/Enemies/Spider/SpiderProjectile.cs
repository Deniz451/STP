using Unity.Mathematics;
using UnityEngine;

public class SpiderProjectile : MonoBehaviour
{
    [SerializeField] private EnemySO enemySO;
    [SerializeField] private GameObject webPlanePrefab;
    [SerializeField] private float projectileSpeed;
    [HideInInspector] public Vector3 targetPosition;
    private Vector3 dir;


    private void Start()
    {
        dir = (targetPosition - transform.position).normalized;
        GetComponent<Rigidbody>().AddForce(dir * projectileSpeed, ForceMode.Impulse);
        Destroy(gameObject, 5f);
    }

    private void OnTriggerEnter(Collider collision)
    {
        /*if (collision.gameObject.layer == LayerMask.NameToLayer("Terrain"))
        {
            Instantiate(webPlanePrefab, transform.position, quaternion.identity);
            Destroy(gameObject);
        }*/
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<IDamagable>().TakeDamage(enemySO.damage);
            Destroy(gameObject);
        }
    }

    private void OnDrawGizmos()
    {
        /*Gizmos.color = Color.red;
        Vector3 previousPoint = startPos;
        for (float i = 0; i <= 1; i += 0.05f)
        {
            Vector3 ab = Vector3.Lerp(startPos, midPoint, i);
            Vector3 bc = Vector3.Lerp(midPoint, targetPosition, i);
            Vector3 pointOnCurve = Vector3.Lerp(ab, bc, i);

            Gizmos.DrawLine(previousPoint, pointOnCurve);
            previousPoint = pointOnCurve;
        }*/
    }

    // Old attack
    /*private Vector3 CalculateMidPoint()
    {
        Vector3 midpoint = (startPos + targetPosition) / 2;
        midpoint.y += 50;
        return midpoint;
    }*/

    // needs to be called every frame to work
    /*private void ArchAttack()
    {
        t += Time.deltaTime * projectileSpeed;
        t = Mathf.Clamp(t, 0f, 1f);

        Vector3 ab = Vector3.Lerp(startPos, midPoint, t);
        Vector3 bc = Vector3.Lerp(midPoint, targetPosition, t);

        transform.position = Vector3.Lerp(ab, bc, t);
    }*/
}
