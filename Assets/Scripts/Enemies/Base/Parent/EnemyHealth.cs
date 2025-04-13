using System.Collections;
using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamagable
{
    private EnemyReferences enemyReferences;
    public event Action OnDeath;
    public float health; // After testing make var private
    private bool isDead = false;

    // Array that stores gameobject that are separte from the main body gameobject of the enemy
    // and deletes all of the objects in arr on death - remake models?
    public GameObject[] gameobjectsToDelete;


    protected virtual void Start()
    {
        enemyReferences = GetComponent<EnemyReferences>();
        health = enemyReferences.enemySO.health;
    }

    protected virtual void Update()
    {
        if (health <= 0 && !isDead) StartCoroutine(Die());
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        SoundManagerSO.PlaySFXClip(enemyReferences.enemySO.hit, transform.position, 0.5f);
        if (health <= 0 && !isDead) 
            StartCoroutine(Die());
    }

    public IEnumerator Die()
    {
        isDead = true;
        OnDeath?.Invoke();

        // Currently fixing death animations
        //yield return StartCoroutine(PlayDeathAnimation());
        StartCoroutine(ApplyDisolveShader());

        foreach (GameObject objectToDelete in gameobjectsToDelete) Destroy(objectToDelete);

        yield return new WaitForSeconds(enemyReferences.enemySO.dissolveDuration);

        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>().CurrenctyAmount += enemyReferences.enemySO.currency;

        Destroy(gameObject);
    }

    private IEnumerator ApplyDisolveShader()
    {
        float dissolveProgress = 0f;

        transform.Find("body").GetComponent<Renderer>().material = enemyReferences.enemySO.dissolveMaterial;

        while (dissolveProgress < 1f)
        {
            dissolveProgress += Time.deltaTime / enemyReferences.enemySO.dissolveDuration;
            dissolveProgress = Mathf.Clamp01(dissolveProgress);
            enemyReferences.enemySO.dissolveMaterial.SetFloat("_AlphaCliping", dissolveProgress);

            yield return null;
        }
    }

    private IEnumerator PlayDeathAnimation()
    {
        //enemyReferences.animator.runtimeAnimatorController = enemyReferences.enemySO.animatorController;
        enemyReferences.animator.SetTrigger("death");
        yield return new WaitForSeconds(0.5f);
    }
}
