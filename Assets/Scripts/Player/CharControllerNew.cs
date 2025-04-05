using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CharControllerNew : MonoBehaviour
{
    CharacterController controller;
    Rigidbody rb;
    float verticalVelocity;

    public float dashDelay;
    public float playerSpeed = 5f;
    public float pushForce = 8f;
    public float dashDuration = 0.3f;
    public Slider dashBar;
    private float gravityValue = 9.81f;
    private float currentDelay = 0;
    private Vector3 currentMoveDirection;
    private bool isDashing = false;
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
        rb.isKinematic = true;
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
                lastClonePos = transform.position;
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
    }

    IEnumerator Dash(float duration, float force, Vector3 direction)
    {
        EventManager.Instance.Publish(GameEvents.EventType.PlayerDashStart);
        lastClonePos = transform.position;
        controller.enabled = false;
        rb.isKinematic = false;
        rb.useGravity = false;

        rb.velocity = Vector3.zero;
        rb.AddForce(direction * force, ForceMode.Impulse);

        isDashing = true;
        yield return new WaitForSeconds(duration);

        rb.velocity = Vector3.zero;
        rb.isKinematic = true;
        controller.enabled = true;
        rb.useGravity = true;

        isDashing = false;
        EventManager.Instance.Publish(GameEvents.EventType.PlayerDashEnd);
    }
}


