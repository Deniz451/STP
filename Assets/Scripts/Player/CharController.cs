using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
    CharacterController controller;
    float verticalVelocity;
    bool playerGrounded;
    float groundedTimer;
    [SerializeField] float playerSpeed = 2f;
    [SerializeField] float jumpHeight = 1f;
    float gravityValue = 9.81f;
    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        playerGrounded = controller.isGrounded;
        if (playerGrounded)
        {
            groundedTimer = 0.2f;
        }
        if (groundedTimer > 0)
        {
            groundedTimer -= Time.deltaTime;
        }
        if (playerGrounded && verticalVelocity < 0)
        {
            verticalVelocity = 0;
        }
        verticalVelocity -= gravityValue * Time.deltaTime;

        Vector3 move = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
        move *= playerSpeed;

        if (move.magnitude > 0.05f)
        {
            gameObject.transform.forward = move;
        }

        if (Input.GetButtonDown("Jump") && groundedTimer > 0)
        {
            groundedTimer = 0;
            verticalVelocity = Mathf.Sqrt(jumpHeight * 2 * gravityValue);
        }

        move.y = verticalVelocity;
        controller.Move(move * Time.deltaTime);
    }
}

