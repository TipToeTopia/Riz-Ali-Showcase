using NUnit.Framework.Internal;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Swinging : MonoBehaviour
{
    [SerializeField]
    private Camera playerCamera;
    [SerializeField]
    private float pullSpeed = 10f;
    [SerializeField]
    private float maxDistance = 50f;
    [SerializeField]
    private KeyCode pullButton = KeyCode.E;

    private bool isPulling;
    private Vector3 targetPoint;

    private const float STOPPPING_POINT = 10f;

    void Update()
    {
        // Press E to pull toward whatever you're looking at
        if (Input.GetKeyDown(pullButton) && !isPulling)
        {
            Ray ray = new Ray(
                playerCamera.transform.position,
                playerCamera.transform.forward
            );

            if (Physics.Raycast(ray, out RaycastHit hit, maxDistance))
            {
                targetPoint = hit.point;
                isPulling = true;
            }
        }

        // Move toward target
        if (isPulling)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPoint,
                pullSpeed * Time.deltaTime
            );

            // Stop when close enough
            if (Vector3.Distance(transform.position, targetPoint) < STOPPPING_POINT)
            {
                isPulling = false;
            }
        }
    }


}
