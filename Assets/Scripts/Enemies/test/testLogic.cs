// class does not execute specific functions
// class checks statemnets depending on values and decides which state teh enemy should be in
// after the state switch OnStateSwitch functions is executed respective functions in other classes are called

using System.Collections.Generic;
using UnityEngine;

public class testLogic : MonoBehaviour
{

    private enum States
    {
        Unassigned,
        Chasing,
        Attack,
        Death
    }
    [SerializeField] private States currentStates = States.Unassigned;
    private States lastState = States.Unassigned;

    [SerializeField] private EnemySO spiderSO;
    private Transform player;
    private bool isAttacking;


    private testMovement movement;
    private testAttack attack;
    private testHealth health;


    private void Start()
    {


        movement = GetComponent<testMovement>();
        attack = GetComponent<testAttack>();
        health = GetComponent<testHealth>();

        attack.OnAttackComplete += CompletedAttack;
        health.OnDeath += Death;

        // maybe make player transform accessible globaly?
        player = GameObject.FindGameObjectWithTag("Player")?.transform ?? player;
        Spawn();
    }

    private void Update()
    {
        // tries to find the player if there was no initialy at the start
        if (player == null && GameObject.FindGameObjectWithTag("Player")) player = GameObject.FindGameObjectWithTag("Player").transform;

        if (!isAttacking) CheckForStates();

        if (lastState != currentStates)
        {
            lastState = currentStates;
            OnStateChange();
        }
    }

    private void CheckForStates()
    {
        if (player != null && Vector3.Distance(transform.position, player.transform.position) > spiderSO.attackDistance)
        {
            currentStates = States.Chasing;
        }
        else if (player != null && Vector3.Distance(transform.position, player.transform.position) <= spiderSO.attackDistance)
        {
            currentStates = States.Attack;
        }
        else if (GameObject.FindGameObjectWithTag("Player") == null)
        {
            currentStates = States.Unassigned;
        }
    }

    private void OnStateChange()
    {
        switch (currentStates)
        {
            case States.Chasing:
                Chase();
                break;
            case States.Attack:
                Attack();
                break;
            /*case States.Death:
                Death();
                break;*/
        }
    }

    // the first state when enemy is instantiated
    // later add things like play spawn animations etc
    private void Spawn()
    {
        currentStates = States.Chasing;
    }

    private void Chase()
    {
        Debug.Log("Chasing");
        // calls the function to chase player
        movement.ChasePlayer();
    }

    private void Attack()
    {
        Debug.Log("Attacking");
        isAttacking = true;
        movement.StopChasing();

        StartCoroutine(attack.Attack(player.position));
    }

    private void Death()
    {
        currentStates = States.Death;
        movement.StopChasing();
        movement.StopAllCoroutines();
        attack.StopAllCoroutines();
        StopAllCoroutines();
        GameObject.Find("Rig").SetActive(false);
    }

    private void CompletedAttack()
    {
        isAttacking = false;
    }
}
