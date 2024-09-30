using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CharController : MonoBehaviour
{
    private CharacterController controller;
    private float verticalVelocity;
    private bool playerGrounded;
    private float groundedTimer;

    [SerializeField] private float playerSpeed = 5f; // Adjusted for better responsiveness
    [SerializeField] private float jumpHeight = 2f;
    private float gravityValue = 9.81f;

    Timer timer;
    float time1;
    float time2;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        // Check if the player is grounded
        playerGrounded = controller.isGrounded;

        // Reset vertical velocity if grounded
        if (playerGrounded)
        {
            if (verticalVelocity < 0)
            {
                verticalVelocity = -1f; // Small downward force to keep grounded
            }

            groundedTimer = 0.2f; // Reset grounded timer
        }
        else
        {
            groundedTimer -= Time.deltaTime; // Decrease grounded timer if not grounded
        }

        // Apply gravity
        verticalVelocity -= gravityValue * Time.deltaTime;

        // Get movement input
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        Vector3 move = new Vector3(horizontalInput, 0, verticalInput).normalized; // Local input

        // Move in the player's forward direction
        Vector3 moveDirection = transform.TransformDirection(move) * playerSpeed;

        // Handle jumping
        if (Input.GetButtonDown("Jump") && groundedTimer > 0)
        {
            verticalVelocity = Mathf.Sqrt(jumpHeight * 2 * gravityValue);
        }

        // Combine movement with vertical velocity
        moveDirection.y = verticalVelocity;

        // Move the character
        controller.Move(moveDirection * Time.deltaTime);
    }
}

