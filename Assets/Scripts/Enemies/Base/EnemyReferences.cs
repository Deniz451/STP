using System.Collections;
using UnityEngine;

public class EnemyReferences : MonoBehaviour
{
    [SerializeField] public EnemySO enemySO;
    [HideInInspector] public Transform playerTransform;
    [HideInInspector] public Rigidbody rb;
    [HideInInspector] public Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform ?? playerTransform;

        if (playerTransform == null) StartCoroutine(FindPlayer());
    }

    // tries to find the player if there was no initialy at the start
    private IEnumerator FindPlayer()
    {
        while (playerTransform == null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player")?.transform ?? playerTransform;
            yield return null;
        }
    }
}
