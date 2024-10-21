using System;
using System.Collections;
using UnityEngine;

public class testHealth : MonoBehaviour
{

    [SerializeField] private EnemySO spider;
    [SerializeField] private Material spiderDissolveMaterial;
    private Animator animator;
    public Action OnDeath;
    [SerializeField] private float health;
    private bool isDead = false;


    // fuuuuuj
    public GameObject[] eyes;


    private void Start()
    {
        health = spider.health;
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (health <= 0 && !isDead) StartCoroutine(Die());
    }

    public IEnumerator Die()
    {
        isDead = true;
        OnDeath?.Invoke();
        animator.SetTrigger("death");
        yield return new WaitForSeconds(0.5f);
        GameObject.Find("body").GetComponent<Renderer>().material = spiderDissolveMaterial;


        // adjust this part, very ugly, fuuuuuj
        foreach(GameObject eye in eyes) Destroy(eye);


        yield return new WaitForSeconds(4f);

        // adjust this part, move destroying to logic
        Destroy(gameObject);
    }
}
