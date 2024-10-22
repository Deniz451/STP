using System;
using System.Collections;
using UnityEngine;

public class testHealth : MonoBehaviour
{

    [SerializeField] private EnemySO spider;
    [SerializeField] private Material spiderDissolveMaterial;
    private Animator animator;
    public Action OnDeath;
    public float health; // make private
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
        // set bool to true to execute functions only once
        isDead = true;
        // invoke the action so that subscribed function in logic script exectues
        OnDeath?.Invoke();
        // play death animation
        animator.SetTrigger("death");
        // wait the duration of the animation
        yield return new WaitForSeconds(0.5f);
        // start dissolve effect
        StartCoroutine(ApplyDisolveShader());
        // delete the separete objects, eyes
        // adjust this part, very ugly, fuuuuuj
        foreach (GameObject eye in eyes) Destroy(eye);
        // wait for the dration of teh dissolve effect
        yield return new WaitForSeconds(spider.dissolveDuration);
        // delete gameobject
        // adjust this part, move destroying to logic
        Destroy(gameObject);
    }

    private IEnumerator ApplyDisolveShader()
    {
        float dissolveProgress = 0f;

        GameObject.Find("body").GetComponent<Renderer>().material = spider.dissolveMaterial;

        while (dissolveProgress < 1f)
        {
            dissolveProgress += Time.deltaTime / spider.dissolveDuration;
            dissolveProgress = Mathf.Clamp01(dissolveProgress);
            spiderDissolveMaterial.SetFloat("_AlphaCliping", dissolveProgress);

            yield return null;
        }
    }
}
