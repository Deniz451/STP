using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] float bulletDamage;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<IDamagable>().TakeDamage(bulletDamage);
            Destroy(gameObject);
        }
        else if (!other.CompareTag("Player") && !other.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }

    }
}
