using UnityEngine;

public class GameSystemManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject[] DeliverySlots = GameObject.FindGameObjectsWithTag("DeliverySlot");
        int index = Random.Range(0, DeliverySlots.Length);

        GameObject FirstDeliverySlot = DeliverySlots[index];
        FirstDeliverySlot.GetComponent<DeliverySlot>().UpdateDeliveryStatus(true);
    }
}
