using UnityEngine;

public class DeliverySlot : MonoBehaviour
{
    public Material MaterialInactive;
    public Material MaterialActive;

    bool DeliveryActive = false;

    [SerializeField] GameObject GameManager;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameManager = GameObject.FindGameObjectWithTag("Manager");
    }

    // Update is called once per frame
    void Update()
    {
        
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

    void DeliveryComplete()
    {
        GameManager.GetComponent<GameSystemManager>().NextDelivery(this.gameObject);
        UpdateDeliveryStatus(false);
    }
}
