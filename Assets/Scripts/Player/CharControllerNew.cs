using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharControllerNew : MonoBehaviour
{
    CharacterController controller;
    Rigidbody rb;
    float verticalVelocity;

    [SerializeField] private float dashDelay = 3f;
    [SerializeField] float playerSpeed = 5f;
    [SerializeField] float pushForce = 8f;
    [SerializeField] float dashDuration = 0.3f;
    public Slider dashBar;
    float gravityValue = 9.81f;
    float currentDelay = 0;

    Vector3 currentMoveDirection;
    bool isDashing = false;
    private bool canMove;
    public GameObject dashClone;
    public float cloneDistance;
    private Vector3 lastClonePos;


    private void OnEnable() {
        EventManager.Instance.Subscribe(GameEvents.EventType.PlayerEnabled, () => canMove = true);
        EventManager.Instance.Subscribe(GameEvents.EventType.PlayerDisabled, () => canMove = false);
    }

    private void OnDestroy() {
        EventManager.Instance.Unsubscribe(GameEvents.EventType.PlayerEnabled, () => canMove = true);
        EventManager.Instance.Unsubscribe(GameEvents.EventType.PlayerDisabled, () => canMove = false);
    }

    void Start()
    {
        controller = GetComponent<CharacterController>();
        controller.enabled = true;
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;  // Ensure Rigidbody doesn't interfere initially
    }

    void Update()
    {
        if (!canMove) return;
        
        if (!isDashing)
        {
            verticalVelocity -= gravityValue * Time.deltaTime;

            float horizontalInput = Input.GetAxisRaw("Horizontal");
            float verticalInput = Input.GetAxisRaw("Vertical");
            Vector3 move = new Vector3(horizontalInput, 0, verticalInput).normalized;
            currentMoveDirection = move;

            Vector3 moveDirection = transform.TransformDirection(move) * playerSpeed;
            moveDirection.y = verticalVelocity;

            controller.Move(moveDirection * Time.deltaTime);
        }
        else {
            if (Vector3.Distance(transform.position, lastClonePos) >= cloneDistance) {
                GameObject clone = Instantiate(dashClone, transform.position, transform.rotation);
                Destroy(clone, 1f);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space) && currentDelay <= 0)
        {
            currentDelay = dashDelay;
            Vector3 dodgeDirection = currentMoveDirection != Vector3.zero ? currentMoveDirection.normalized : Vector3.forward;
            StartCoroutine(Dash(dashDuration, pushForce, dodgeDirection));
        }
        currentDelay -= Time.deltaTime;
        dashBar.value = dashDelay - currentDelay;
        //if (currentDelay <= 0) { Debug.Log("Dash Ready"); }
    }

    IEnumerator Dash(float duration, float force, Vector3 direction)
    {
        lastClonePos = transform.position;
        controller.enabled = false;
        rb.isKinematic = false;
        rb.useGravity = false;  // Disable gravity

        rb.velocity = Vector3.zero;  // Reset velocity
        rb.AddForce(direction * force, ForceMode.Impulse);  // Apply dash force

        isDashing = true;
        yield return new WaitForSeconds(duration);  // Wait for the duration of the dash

        rb.velocity = Vector3.zero;  // Reset velocity after dash
        rb.isKinematic = true;
        controller.enabled = true;
        rb.useGravity = true;  // Re-enable gravity

        isDashing = false;
    }
}


