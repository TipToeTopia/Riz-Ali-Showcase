using NUnit.Framework.Internal;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Swinging : MonoBehaviour
{
    public Camera playerCamera;
    public Rigidbody playerRigidbody;

    public float maxDistance = 100f;
    public float pullForce = 20f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            //TryPull();

            Ray ray = new Ray(
            this.transform.position,
            this.transform.forward
            );

            if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
            {

                Vector3 direction = (hit.transform.position - playerRigidbody.position).normalized;

                playerRigidbody.AddForce(direction * pullForce, ForceMode.Force);

            }
            else
                print("N/A");
        }

       

    }

     

        

    void TryPull()
    {
        Ray ray = new Ray(
            playerCamera.transform.position,
            playerCamera.transform.forward
        );

        if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
        {
            Debug.Log("1");
            // Pull toward the object we were looking at
            Vector3 direction = (hit.transform.position - playerRigidbody.position).normalized;

            playerRigidbody.AddForce(direction * pullForce, ForceMode.Force);
        }
    }


}
