using System.Collections;
using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    [SerializeField] private EnemySO enemySO;
    private Animator animator;
    public Action OnDeath;
    public float health; // make private
    private bool isDead = false;


    // REMAKE
    public GameObject[] gameobjectsToDelete;


    protected virtual void Start()
    {
        health = enemySO.health;
        animator = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        if (health <= 0 && !isDead) StartCoroutine(Die());
    }

    public IEnumerator Die()
    {
        isDead = true;
        OnDeath?.Invoke();

        //yield return StartCoroutine(PlayDeathAnimation());
        StartCoroutine(ApplyDisolveShader());

        // REMAKE
        foreach (GameObject objectToDelete in gameobjectsToDelete) Destroy(objectToDelete);

        yield return new WaitForSeconds(enemySO.dissolveDuration);

        Destroy(gameObject);
    }

    private IEnumerator ApplyDisolveShader()
    {
        float dissolveProgress = 0f;

        GameObject.Find("body").GetComponent<Renderer>().material = enemySO.dissolveMaterial;

        while (dissolveProgress < 1f)
        {
            dissolveProgress += Time.deltaTime / enemySO.dissolveDuration;
            dissolveProgress = Mathf.Clamp01(dissolveProgress);
            enemySO.dissolveMaterial.SetFloat("_AlphaCliping", dissolveProgress);

            yield return null;
        }
    }

    private IEnumerator PlayDeathAnimation()
    {
        animator.runtimeAnimatorController = enemySO.animatorController;
        animator.SetTrigger("death");
        yield return new WaitForSeconds(0.5f);
    }
}
