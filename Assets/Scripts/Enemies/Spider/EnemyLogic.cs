// This script does not tell spider any specific tasks
// Script only checks for conditions and depending on them changes the state of the spider
// Depending on states the script than instructs other scripts what to do
// After the right condition is met, the script changes spiders state and triggers the respective function only once
// The function than locates appropriate script and tell it, also only once, what to do

using UnityEngine;

public class EnemyLogic : MonoBehaviour
{

    // private variables
    private enum States
    {
        Unassigned,
        Chasing,
        MeleeAttack,
        RangeAttack,
        Death
    }
    [SerializeField] private States currentStates = States.Unassigned;
    private States lastState = States.Unassigned;
    private Transform player;

    // enemy scripts
    private EnemyMovement enemyMovement;
    private EnemyAttack enemyAttack;

    // inspector settings
    [SerializeField] private float rangeAttackDistance;
    [SerializeField] private float meleeAttackDistance;



    private void Start()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        enemyAttack = GetComponent<EnemyAttack>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Spawn();
    }

    private void Update()
    {
        CheckForStates();

        if (lastState != currentStates)
        {
            lastState = currentStates;
            CheckForActions();
        }
    }

    private void CheckForStates()
    {
        if (Vector3.Distance(transform.position, player.transform.position) > rangeAttackDistance)
        {
            currentStates = States.Chasing;
        }
        // must be before checking for the larger distance
        else if (Vector3.Distance(transform.position, player.transform.position) <= meleeAttackDistance)
        {
            currentStates = States.MeleeAttack;
        }
        else if (Vector3.Distance(transform.position, player.transform.position) <= rangeAttackDistance)
        {
            currentStates = States.RangeAttack;
        }
    }

    private void CheckForActions()
    {
        switch(currentStates)
        {
            case States.Chasing:
                Chase();
                break;
            case States.MeleeAttack:
                MeleeAtack();
                break;
            case States.RangeAttack:
                RangeAttack();
                break;
            case States.Death:
                Death();
                break;
        }
    }

    private void Spawn()
    {
        currentStates = States.Chasing;
    }

    private void Chase()
    {
        Debug.LogWarning("Chase");
        enemyMovement.ChasePlayer();
    }

    private void MeleeAtack()
    {
        Debug.LogWarning("Melee attack");
    }

    private void RangeAttack()
    {
        Debug.LogWarning("Range attack");
        enemyMovement.StopChasing();
        StartCoroutine(enemyAttack.RangeAttack());
    }

    private void Death()
    {
        Debug.LogWarning("Dying");
    }
}
