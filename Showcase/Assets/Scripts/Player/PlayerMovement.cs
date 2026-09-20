using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody m_Rigidbody;

    [SerializeField]
    private float m_Speed;


    private bool isGrounded;

    void Start()
    {

        m_Rigidbody = GetComponent<Rigidbody>();


    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W) && isGrounded == true)
        {

            m_Rigidbody.linearVelocity = transform.forward * m_Speed;
        }

    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            isGrounded = false;
        }
    }

}
