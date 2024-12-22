using UnityEngine;

public class CharController : MonoBehaviour
{
    private CharacterController controller;
    private float verticalVelocity;
    private bool isMoving = false;

    [SerializeField] private float playerSpeed = 5f;
    [SerializeField] private float gravityValue = 9.81f;
    [SerializeField] private float coyoteTime = 0.2f;
    [SerializeField] private float jumpBufferingTime = 0.2f;
    [SerializeField] private float audioInterval;
    [SerializeField] private AudioClip stepClip;

    private float coyoteTimeCounter;
    private float jumpBufferingCounter;
    private float timeSinceLastAudio;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        HandleAudio();
        UpdateGroundStatus();
        HandleCoyoteAndJumpBuffering();
        ApplyGravity();
        HandleMovementAndJumping();
    }

    private void HandleAudio()
    {
        timeSinceLastAudio += Time.deltaTime;
        if (timeSinceLastAudio >= audioInterval && isMoving)
        {
            timeSinceLastAudio = 0;
            SoundManagerSO.PlaySFXClip(stepClip, transform.position, 1f);
        }
    }

    private void UpdateGroundStatus()
    {
        if (controller.isGrounded)
        {
            if (verticalVelocity < 0)
            {
                verticalVelocity = -1f;
            }
            coyoteTimeCounter = coyoteTime;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
    }

    private void HandleCoyoteAndJumpBuffering()
    {
        if (Input.GetButtonDown("Jump"))
        {
            jumpBufferingCounter = jumpBufferingTime;
        }
        else
        {
            jumpBufferingCounter -= Time.deltaTime;
        }
    }

    private void ApplyGravity()
    {
        verticalVelocity -= gravityValue * Time.deltaTime;
    }

    private void HandleMovementAndJumping()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        Vector3 move = new Vector3(horizontalInput, 0, verticalInput).normalized;

        isMoving = move != Vector3.zero;

        Vector3 moveDirection = transform.TransformDirection(move) * playerSpeed;

        /*if (coyoteTimeCounter > 0 && Input.GetButtonDown("Jump"))
        {
            verticalVelocity = Mathf.Sqrt(jumpHeight * 2 * gravityValue);
        }*/

        moveDirection.y = verticalVelocity;
        controller.Move(moveDirection * Time.deltaTime);
    }
}
