using System.Collections.Generic;
using UnityEngine;

public class GameSystemManager : MonoBehaviour
{

    [Header("Delivery Slot Data")]
    [SerializeField] List<GameObject> DeliverySlots;
    [SerializeField] int SlotIndex;
    [SerializeField] GameObject NextActiveSlot;

    private int DeliveryCash = 15;
    private int DeliveryTips;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DeliverySlots = new List<GameObject>();
        DeliverySlots.AddRange(GameObject.FindGameObjectsWithTag("DeliverySlot"));

        SlotIndex = Random.Range(0, DeliverySlots.Count);

        GameObject FirstDeliverySlot = DeliverySlots[SlotIndex];
        FirstDeliverySlot.GetComponent<DeliverySlot>().UpdateDeliveryStatus(true);
    }

    public void NextDelivery(GameObject PreviousDelivery)
    {
        DeliverySlots.Remove(PreviousDelivery);

        SlotIndex = Random.Range(0, DeliverySlots.Count);

        NextActiveSlot = DeliverySlots[SlotIndex];
        NextActiveSlot.GetComponent<DeliverySlot>().UpdateDeliveryStatus(true);
        GiveCashAndTips();

        DeliverySlots.Add(PreviousDelivery);
    }

    void GiveCashAndTips()
    {
        DeliveryTips = Random.Range(0, 15);
        Debug.Log("You got $" + DeliveryCash + " for a delivery!");
        Debug.Log("You got $" + DeliveryTips + " as a tip!");
    }
}
