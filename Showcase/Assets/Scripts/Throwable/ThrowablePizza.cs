using UnityEngine;

public class ThrowablePizza : MonoBehaviour
{
    private float PizzaRotationSpeed = 200;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, PizzaRotationSpeed * Time.deltaTime, 0);
    }
}
