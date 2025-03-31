using UnityEngine;

public class CharController : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float movementForce;
    [SerializeField] float maxVelocity = 10f;
    [SerializeField] float gravityValue = -9.81f;
    Vector3 moveDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {
        rb.AddForce(new Vector3(0, gravityValue, 0) * 1);

        if (rb != null && rb.velocity.magnitude >= maxVelocity)
        {
            if (Input.GetKey(KeyCode.W)) { rb.AddForce(movementForce * Vector3.forward); }
            if (Input.GetKey(KeyCode.S)) { rb.AddForce(movementForce * -Vector3.forward); }
            if (Input.GetKey(KeyCode.D)) { rb.AddForce(movementForce * Vector3.right); }
            if (Input.GetKey(KeyCode.A)) { rb.AddForce(movementForce * -Vector3.right); }
        }
    }
}
