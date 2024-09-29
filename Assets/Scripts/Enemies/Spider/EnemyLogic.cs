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
        Attack,
        Death
    }
    [SerializeField] private States currentStates = States.Unassigned;
    private States lastState = States.Unassigned;
    private Transform player;
    private bool isAttacking;

    // enemy scripts
    private EnemyMovement enemyMovement;
    private EnemyAttack enemyAttack;


    // finds variables and triggers spawn function
    private void Start()
    {
        enemyMovement = GetComponent<EnemyMovement>();
        enemyAttack = GetComponent<EnemyAttack>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Spawn();
    }

    // checks for state change condition and if last state doesn equal to current state triggers new action
    private void Update()
    {
        if (!isAttacking) CheckForStates();

        if (lastState != currentStates)
        {
            lastState = currentStates;
            CheckForActions();
        }
    }

    private void CheckForStates()
    {
        if (Vector3.Distance(transform.position, player.transform.position) > enemyAttack.attackDistance)
        {
            currentStates = States.Chasing;
        }
        else if (Vector3.Distance(transform.position, player.transform.position) <= enemyAttack.attackDistance)
        {
            currentStates = States.Attack;
        }
    }

    private void CheckForActions()
    {
        switch(currentStates)
        {
            case States.Chasing:
                Chase();
                break;
            case States.Attack:
                Attack();
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

    private void Attack()
    {
        Debug.LogWarning("Range attack");
        isAttacking = true;
        enemyMovement.StopChasing();
        StartCoroutine(enemyAttack.RangeAttack());
    }

    private void Death()
    {
        Debug.LogWarning("Dying");
    }

    public void SetAttackBoolFalse()
    {
        isAttacking = false;
        currentStates = States.Unassigned;
    }
}
