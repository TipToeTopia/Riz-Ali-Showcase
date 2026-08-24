
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerLook : MonoBehaviour
{
    private float mouseX;
    private float mouseY;

    [SerializeField]
    private float mouseSensitivity = 100f;

    [SerializeField]
    private Transform playerTransform;

    private float xRotation;

    private const string X_MOUSE = "Mouse X";
    private const string Y_MOUSE = "Mouse Y";

    private const float MAXIMUM_CLAMP = 120.0f;
    private const float MINIMUM_CLAMP = -120.0f;

    void Start()
    {
        transform.localRotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
    }

    void Update()
    {
        mouseX = Input.GetAxis(Y_MOUSE) * mouseSensitivity * Time.deltaTime;
        mouseY = Input.GetAxis(X_MOUSE) * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseX;
        xRotation = Mathf.Clamp(xRotation, MINIMUM_CLAMP, MAXIMUM_CLAMP);

        transform.localRotation = Quaternion.Euler(xRotation, 0.0f, -0.0f);
        playerTransform.Rotate(Vector3.up * mouseY);


    }
}
