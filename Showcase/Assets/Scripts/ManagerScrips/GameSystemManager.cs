using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;

public class GameSystemManager : MonoBehaviour
{

    [Header("Delivery Slot Data")]
    [SerializeField] List<GameObject> DeliverySlots;
    [SerializeField] int SlotIndex;
    [SerializeField] GameObject NextActiveDelivery;

    [Header("Cash Data")]
    public TextMeshProUGUI CashText;
    [SerializeField] int DeliveryCash = 15;
    [SerializeField] int CurrentCashEarned;

    [Header("Tips Data")]
    public TextMeshProUGUI TipsText;
    [SerializeField] int DeliveryTips;
    [SerializeField] int CurrentTipsEarned;
    public TextMeshProUGUI TipsNotifiText;
    [SerializeField] float TipsNotifiFadeTime;

    [Header("Game Timer")]
    public TextMeshProUGUI GameTimerText;
    [SerializeField] float CurrentGameTime;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Selecting the first delivery slot to activate
        DeliverySlots = new List<GameObject>();
        DeliverySlots.AddRange(GameObject.FindGameObjectsWithTag("DeliverySlot"));
        SlotIndex = Random.Range(0, DeliverySlots.Count);
        GameObject FirstDeliverySlot = DeliverySlots[SlotIndex];
        FirstDeliverySlot.GetComponent<DeliverySlot>().UpdateDeliveryStatus(true);
    }

    void Update()
    {
        //Game Timer Countdown
        CurrentGameTime -= Time.deltaTime;
        int timerMinutes = Mathf.FloorToInt(CurrentGameTime / 60);
        int timerSeconds = Mathf.FloorToInt(CurrentGameTime % 60);
        GameTimerText.text = string.Format("{0:00}:{1:00}", timerMinutes, timerSeconds);

        if(CurrentGameTime < 1)
        {
            //End the game and move to results scene
            Debug.Log("Shift Over");
        }
    }

    public void NextDelivery(GameObject PreviousDelivery)
    {
        //Preventing the previous slot from being selected again in the list
        //Selecting the next delivery slot
        DeliverySlots.Remove(PreviousDelivery);
        SlotIndex = Random.Range(0, DeliverySlots.Count);
        NextActiveDelivery = DeliverySlots[SlotIndex];
        NextActiveDelivery.GetComponent<DeliverySlot>().UpdateDeliveryStatus(true);
        GiveCashAndTips();
        DeliverySlots.Add(PreviousDelivery);
    }

    void GiveCashAndTips()
    {
        //SHOULD MAKE TIP VALUE BE BASED ON HOW QUICK PLAYER DELIVERS

        //Currently getting a random int for the tip
        DeliveryTips = Random.Range(1, 15);
        Debug.Log("You got $" + DeliveryCash + " for a delivery!");
        Debug.Log("You got $" + DeliveryTips + " as a tip!");
        StartCoroutine(TipsNotifi());

        CurrentCashEarned += DeliveryCash;
        CashText.text = ("Cash Earned: $" + CurrentCashEarned);

        CurrentTipsEarned += DeliveryTips;
        TipsText.text = ("Tips Earned: $" + CurrentTipsEarned);
    }

    IEnumerator TipsNotifi()
    {
        //Tip notification text fade out
        TipsNotifiText.enabled = true;
        TipsNotifiText.text = ("You got a $" + DeliveryTips + " tip!");

        while (TipsNotifiText.color.a > 0f)
        { 
            TipsNotifiText.color = new Color(255, 255, 255, TipsNotifiText.color.a - Time.deltaTime / TipsNotifiFadeTime);
            yield return null;
        }

        yield return new WaitForSeconds(1f);
        TipsNotifiText.enabled = false;
        TipsNotifiText.color = new Color32(255, 255, 255, 255);
    }


}
