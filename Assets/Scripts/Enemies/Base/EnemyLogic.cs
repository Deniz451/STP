// class does not execute specific functions
// class checks statemnets depending on values and decides which state teh enemy should be in
// after the state switch OnStateSwitch functions is executed respective functions in other classes are called

using UnityEngine;

public abstract class EnemyLogic : MonoBehaviour
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

    [SerializeField] private EnemySO enemySO;
    protected Transform player;
    protected bool isAttacking;


    protected virtual void Start()
    {
        // maybe make player transform accessible globaly?
        player = GameObject.FindGameObjectWithTag("Player")?.transform ?? player;
        Spawn();
    }

    protected virtual void Update()
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
        if (player != null && Vector3.Distance(transform.position, player.transform.position) > enemySO.attackDistance)
        {
            currentStates = States.Chasing;
        }
        else if (player != null && Vector3.Distance(transform.position, player.transform.position) <= enemySO.attackDistance)
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

    protected abstract void Chase();

    protected abstract void Attack();

    protected virtual void Death()
    {
        currentStates = States.Death;
        StopAllCoroutines();
        GameObject.Find("Rig").SetActive(false);
    }

    protected void CompletedAttack()
    {
        isAttacking = false;
    }
}
