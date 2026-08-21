using UnityEngine;

public class DeliverySlot : MonoBehaviour
{
    public Material MaterialInactive;
    public Material MaterialActive;

    bool DeliveryActive = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Throwable") && DeliveryActive)
        {
            Destroy(other.gameObject);
            Debug.Log("Delivery Done");

            NewDeliverySlot();
        }
        else
        {
            Destroy(other.gameObject);
            Debug.Log("Delivery Failed");
        }
    }

    public void UpdateDeliveryStatus(bool SlotActive)
    {
        if (SlotActive == true)
        {
            this.GetComponent<Renderer>().material = MaterialActive;
            this.GetComponent<Collider>().enabled = true;
            this.GetComponent<Collider>().isTrigger = true;
            DeliveryActive = true;
        }
        else
        {
            this.GetComponent<Renderer>().material = MaterialInactive;
            this.GetComponent <Collider>().enabled = false;
            DeliveryActive = false;
        }
    }

    void NewDeliverySlot()
    {
        GameObject[] DeliverySlots = GameObject.FindGameObjectsWithTag("DeliverySlot");
        int index = Random.Range(0, DeliverySlots.Length);

        GameObject NextActiveSlot = DeliverySlots[index];
        NextActiveSlot.GetComponent<DeliverySlot>().UpdateDeliveryStatus(true);

        UpdateDeliveryStatus(false);
    }
}
