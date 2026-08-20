using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{
    private const string HORIZONTAL_AXIS = "Horizontal";
    private const string VERTICAL_AXIS = "Vertical";

    [SerializeField]
    private float movementSpeed = 5.0f;

    [SerializeField]
    private CharacterController playerControls;

    private Vector3 playerVelocity;

    private float playerGravity = -20.0f;

    void Update()
    {
        float x = Input.GetAxis(HORIZONTAL_AXIS);
        float z = Input.GetAxis(VERTICAL_AXIS);

        Vector3 move = (transform.right * x) + (transform.forward * z);
        playerControls.Move(move * (movementSpeed * Time.deltaTime));

        playerVelocity.y += playerGravity * Time.deltaTime;
        playerControls.Move(playerVelocity * Time.deltaTime);

    }
 
}
