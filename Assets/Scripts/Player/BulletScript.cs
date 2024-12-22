using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VFX;
using System.Collections;

public class BulletScript : MonoBehaviour
{
    [SerializeField] float bulletDamage;
    [SerializeField] GameObject bloodEffect;

    Rigidbody rb;
    ParticleManager pm;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        pm = GameObject.FindGameObjectWithTag("ParticleManager").GetComponent<ParticleManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            //other.gameObject.GetComponent<IDamagable>().TakeDamage(bulletDamage);

            Vector3 contact = other.ClosestPoint(transform.position);
            Vector3 direction = -rb.velocity.normalized;                            //najde bod kolize a rotaci smerem odkud priletela kulka
            Quaternion rotation = Quaternion.LookRotation(direction);

            if(contact.y < 0) { contact.y = 2; }

            GameObject effect = GameObject.Instantiate(bloodEffect, contact, rotation);     //spawne effekt krve

            pm.PlayRandomColor(effect.GetComponentInChildren<VisualEffect>());      //vybere random barvu krve

            StartCoroutine(ParticleDespawn(effect));


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
