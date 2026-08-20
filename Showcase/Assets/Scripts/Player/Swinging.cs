using NUnit.Framework.Internal;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Swinging : MonoBehaviour
{



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Vector3 fwd = transform.TransformDirection(Vector3.forward);

        RaycastHit hit;

        if (Physics.Raycast(transform.position, fwd, 100))
        {
            print("There is something in front of the object!");
            Debug.DrawRay(transform.position, fwd, Color.red);

        }
        else
            print("N/A");


    }
        
    
}
