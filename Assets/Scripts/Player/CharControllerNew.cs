using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharControllerNew : MonoBehaviour
{
    CharacterController controller;
    float verticalVelocity;

    [SerializeField] float playerSpeed = 5f;
    float gravityValue = 9.81f;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        verticalVelocity -= gravityValue * Time.deltaTime;

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        Vector3 move = new Vector3 (horizontalInput, 0, verticalInput).normalized;

        Vector3 moveDirection = transform.TransformDirection(move) * playerSpeed;
        moveDirection.y = verticalVelocity;

        controller.Move(moveDirection * Time.deltaTime);
    }
}
