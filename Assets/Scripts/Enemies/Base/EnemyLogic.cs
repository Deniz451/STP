// This script does not tell enemy any specific tasks
// Script only checks for conditions and depending on them changes the state of the enemy
// Depending on states the script then instructs other scripts what to do
// After the right condition is met, the script changes enemy state and triggers the respective function only once
// The function than locates appropriate script and tells it, also only once, what to do

using UnityEngine;

public abstract class EnemyLogic : MonoBehaviour
{

    // private variables
    protected enum States
    {
        Unassigned,
        Chasing,
        Attack,
        Death
    }
    [SerializeField] protected States currentStates = States.Unassigned;
    private States lastState = States.Unassigned;
    protected Transform player;
    protected bool isAttacking;


    // finds variables and triggers spawn function
    protected virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Spawn();
    }

    // checks for state change condition and if last state doesn equal to current state triggers new action
    protected virtual void Update()
    {
        if (!isAttacking) CheckForStates();

        if (lastState != currentStates)
        {
            lastState = currentStates;
            CheckForActions();
        }
    }

    protected abstract void CheckForStates();

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

    protected abstract void Chase();

    protected abstract void Attack();

    protected abstract void Death();

    public void SetAttackBoolFalse()
    {
        isAttacking = false;
        currentStates = States.Unassigned;
    }
}
