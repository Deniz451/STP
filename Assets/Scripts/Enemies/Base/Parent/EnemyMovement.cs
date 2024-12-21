using UnityEngine;

public abstract class EnemyMovement : MonoBehaviour
{
    private EnemyReferences enemyReferences;
    private bool isChasing = false;
    private Vector3 moveDir;
    private bool isColliding = false;
    private bool hasRotated = false;

    [SerializeField] private float rayLength;


    protected virtual void Start()
    {
        enemyReferences = GetComponent<EnemyReferences>();
    }

    protected virtual void Update()
    {
        if (enemyReferences.playerTransform != null && isChasing)
        {
            if (!isColliding)
            {
                LookAtPlayer();
                moveDir = (enemyReferences.playerTransform.position - transform.position).normalized;
                moveDir.y = 0;
            }

            if (CanGetToPlayer())
            {
                isColliding = false;
                return;
            }

            else FindPath();
        }
    }

    private void FixedUpdate()
    {
        if (isChasing && enemyReferences.playerTransform != null)
        {
            enemyReferences.rb.velocity = moveDir * enemyReferences.enemySO.moveSpeed;
        }
    }

    public void ChasePlayer()
    {
        if (enemyReferences.playerTransform != null) isChasing = true;
    }

    public void StopChasing()
    {
        isChasing = false;
        enemyReferences.rb.velocity = Vector3.zero;
    }

    private void LookAtPlayer()
    {
        transform.LookAt(enemyReferences.playerTransform.position);
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
    }

    private void FindPath()
    {
        Vector3 dirToPlayer = (enemyReferences.playerTransform.position - transform.position).normalized;
        dirToPlayer.y = 0;

        Vector3 forwardRayOrigin = transform.position + Vector3.up * 0.5f;
        Vector3 leftDirection = Quaternion.Euler(0, -45, 0) * dirToPlayer;
        Vector3 rightDirection = Quaternion.Euler(0, 45, 0) * dirToPlayer;
        Vector3 sideLeftDirection = Quaternion.Euler(0, -90, 0) * dirToPlayer;
        Vector3 sideeRightDirection = Quaternion.Euler(0, 90, 0) * dirToPlayer;

        bool obstacleForward = Physics.Raycast(forwardRayOrigin, dirToPlayer, rayLength);
        bool obstacleLeft = Physics.Raycast(forwardRayOrigin, leftDirection, rayLength);
        bool obstacleRight = Physics.Raycast(forwardRayOrigin, rightDirection, rayLength);
        bool obstacleSideLeft = Physics.Raycast(forwardRayOrigin, leftDirection, rayLength);
        bool obstacleSideRight = Physics.Raycast(forwardRayOrigin, rightDirection, rayLength);
        if (obstacleForward || obstacleLeft || obstacleRight || obstacleSideLeft || obstacleSideRight) isColliding = true;

        Debug.DrawRay(forwardRayOrigin, dirToPlayer * rayLength, obstacleForward ? Color.red : Color.green);
        Debug.DrawRay(forwardRayOrigin, leftDirection * rayLength, obstacleLeft ? Color.red : Color.green);
        Debug.DrawRay(forwardRayOrigin, rightDirection * rayLength, obstacleRight ? Color.red : Color.green);
        Debug.DrawRay(forwardRayOrigin, sideLeftDirection * rayLength, obstacleSideLeft ? Color.red : Color.green);
        Debug.DrawRay(forwardRayOrigin, sideeRightDirection * rayLength, obstacleSideRight ? Color.red : Color.green);

        if (obstacleForward)
        {
            if (!hasRotated)
            {
                hasRotated = true;

                bool moveLeft = !obstacleSideLeft;
                bool moveRight = !obstacleSideRight;

                if (moveLeft && !moveRight) transform.Rotate(0, -90, 0);
                else if (!moveLeft && moveRight) transform.Rotate(0, 90, 0);
                else if (moveLeft && moveRight) transform.Rotate(0, 90, 0);

                moveDir = transform.forward;
                moveDir.y = 0;
            }  
        }
    }

    private bool CanGetToPlayer()
    {
        Vector3 dirToPlayer = (enemyReferences.playerTransform.position - transform.position).normalized;
        dirToPlayer.y = 0;

        Vector3 leftRayOrigin = transform.position + Vector3.up * 0.5f + transform.right * -0.7f;
        Vector3 rightRayOrigin = transform.position + Vector3.up * 0.5f + transform.right * 0.7f;

        bool leftRayHitsPlayer = false;
        bool rightRayHitsPlayer = false;

        if (Physics.Raycast(leftRayOrigin, dirToPlayer, out RaycastHit leftHit, Mathf.Infinity)) leftRayHitsPlayer = leftHit.collider.CompareTag("Player");
        if (Physics.Raycast(rightRayOrigin, dirToPlayer, out RaycastHit rightHit, Mathf.Infinity)) rightRayHitsPlayer = rightHit.collider.CompareTag("Player");

        Debug.DrawRay(leftRayOrigin, dirToPlayer * 999, leftRayHitsPlayer ? Color.green : Color.red);
        Debug.DrawRay(rightRayOrigin, dirToPlayer * 999, rightRayHitsPlayer ? Color.green : Color.red);

        if (leftRayHitsPlayer && rightRayHitsPlayer) return true;
        else return false;
    }
}
