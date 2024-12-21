using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VFX;

public class BulletScript : MonoBehaviour
{
    [SerializeField] float bulletDamage;
    [SerializeField] GameObject bloodEffect;
    ParticleManager pm;
    GameObject player;
    Rigidbody rb;
    
    
    // Start is called before the first frame update
    void Start()
    {
        pm = GameObject.FindGameObjectWithTag("ParticleManager").GetComponent<ParticleManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        rb = gameObject.GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            //other.gameObject.GetComponent<IDamagable>().TakeDamage(bulletDamage);

            Vector3 contact = other.ClosestPoint(transform.position);
            Vector3 direction = -rb.velocity.normalized;
            Quaternion rotation = Quaternion.LookRotation(direction);

            if(contact.y < 0) { contact.y = 2; }

            GameObject effect = GameObject.Instantiate(bloodEffect, contact, rotation);

            pm.PlayRandomColor(effect.GetComponentInChildren<VisualEffect>());

            StartCoroutine(ParticleDespawn(effect));
        }
        if (!other.CompareTag("Player"))
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
