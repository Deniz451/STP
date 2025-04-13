using UnityEngine;

public class CharController : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float maxSpeed = 25f;
    [SerializeField] float dashForce = 10f;

    bool isDashing = false;
    Vector3 dashDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Space) && isDashing == false && rb != null)
        {
            isDashing = !isDashing;
            dashDirection = rb.velocity;
        }
    }

    private void FixedUpdate()
    {
        if (rb != null)
        {
            Vector3 moveDirection = Vector3.zero;
            if (Input.GetKey(KeyCode.W) && rb.velocity.magnitude < maxSpeed)
            {
                moveDirection += Vector3.forward;
            }
            if (Input.GetKey(KeyCode.S) && rb.velocity.magnitude < maxSpeed)
            {
                moveDirection += Vector3.back;
            }
            if (Input.GetKey(KeyCode.A) && rb.velocity.magnitude < maxSpeed)
            {
                moveDirection += Vector3.left;
            }
            if (Input.GetKey(KeyCode.D) && rb.velocity.magnitude < maxSpeed)
            {
                moveDirection += Vector3.right;
            }

            moveDirection = moveDirection.normalized;
            rb.velocity = Vector3.zero;
            rb.AddForce(moveDirection * moveSpeed * Time.deltaTime, ForceMode.VelocityChange);

            if(moveDirection!= Vector3.zero)
            {
                dashDirection = moveDirection;
            }

            if (isDashing)
            {
                /*if(dashDirection.x >= 1) { dashDirection.x = 1; }
                else if(dashDirection.x <= -1) {  dashDirection.x = -1; }
                else {  dashDirection.x = 0; }

                if (dashDirection.z >= 1) { dashDirection.z = 1; }
                else if (dashDirection.z <= -1) { dashDirection.z = -1; }
                else { dashDirection.z = 0; }*/

                dashDirection.y = 0;

                if (moveDirection == Vector3.zero)
                {
                    moveDirection = dashDirection;
                }

                rb.AddForce(moveDirection.normalized * dashForce * Time.deltaTime, ForceMode.Impulse);

                Debug.Log("Dashed by: "+ moveDirection * dashForce * Time.deltaTime);

                isDashing = false;
            }
        }
        else { Debug.Log("Wtf kde RB?!?"); }
    }
}
