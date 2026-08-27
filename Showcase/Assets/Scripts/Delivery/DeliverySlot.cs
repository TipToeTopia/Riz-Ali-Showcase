using System;
using System.Collections;
using UnityEngine;

public class DeliverySlot : MonoBehaviour
{
    public Material MaterialInactive;
    public Material MaterialActive;

    bool DeliveryActive = false;

    [SerializeField] GameObject GameManager;
    [SerializeField] float TipTimer = 20f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Finding and setting GameManager
        // Should we be using tag??
        GameManager = GameObject.FindGameObjectWithTag("Manager");
    }


    private void OnTriggerEnter(Collider other)
    {
        //checking to see if the pizza has collided with the delivery slot
        if (other.CompareTag("Throwable") && DeliveryActive)
        {
            Destroy(other.gameObject);
            DeliveryComplete();
        }
    }

    public void UpdateDeliveryStatus(bool SlotActive)
    {
        //Setting collision trigger and material
        if (SlotActive == true)
        {
            this.GetComponent<Renderer>().material = MaterialActive;
            this.GetComponent<Collider>().enabled = true;
            this.GetComponent<Collider>().isTrigger = true;
            DeliveryActive = true;
            StartCoroutine(BeginTipTimer());
        }
        else
        {
            this.GetComponent<Renderer>().material = MaterialInactive;
            this.GetComponent <Collider>().enabled = false;
            DeliveryActive = false;
        }
    }

    void DeliveryComplete()
    {
        //GameManager.GetComponent<GameSystemManager>().isTipTiming = false;

        //Rounding and converting value to int and ensuring the Tip value cannot be 0 --- Int16 because we dont need the higher capacity of int32 or int64
        int TipTimerValue = Convert.ToInt16(Mathf.RoundToInt(TipTimer));
        if (TipTimerValue < 1)
        {
            TipTimerValue = 1;
        }

        //Calling manager to pick a new delivery whilst disabling and excluding this delivery
        GameManager.GetComponent<GameSystemManager>().NextDelivery(this.gameObject, TipTimerValue);
        UpdateDeliveryStatus(false);
    }

    //Tip timer counting down as long as the Delivery slot is active.
    IEnumerator BeginTipTimer()
    {
        while (DeliveryActive)
        {
            TipTimer -= Time.deltaTime;
            yield return null;
        }
    }
}
