using UnityEngine;

public class ThrowablePizza : MonoBehaviour
{
    private float PizzaRotationSpeed = 300;
    private Rigidbody PizzaRigidBody;
    [SerializeField] float ThrowForce = 500;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PizzaRigidBody = GetComponent<Rigidbody>();
        PizzaRigidBody.AddForce(transform.forward * ThrowForce);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, PizzaRotationSpeed * Time.deltaTime, 0);
    }
}
