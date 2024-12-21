using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VFX;

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

    IEnumerator ParticleDespawn(GameObject particle)
    {
        yield return new WaitForSeconds(45);
        Destroy(particle);
    }
}
