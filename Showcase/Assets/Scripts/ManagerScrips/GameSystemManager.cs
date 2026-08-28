using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class GameSystemManager : MonoBehaviour
{
    public SceneAsset GameScene;

    [Header("Delivery Slot Data")]
    [SerializeField] List<GameObject> DeliverySlots;
    [SerializeField] int SlotIndex;
    [SerializeField] GameObject NextActiveDelivery;

    [Header("Cash Data")]
    public TextMeshProUGUI CashText;
    [SerializeField] int DeliveryCash = 15;
    public int CurrentCashEarned;

    [HideInInspector]
    public int highScore;

    [HideInInspector]
    public int CashHighScore;

    [Header("Tips Data")]
    public TextMeshProUGUI TipsText;
    [SerializeField] int DeliveryTips;
    public int CurrentTipsEarned;
    public TextMeshProUGUI TipsNotifiText;
    [SerializeField] float TipsNotifiFadeTime;

    [HideInInspector]
    public float TipTimer = 20f;
    [HideInInspector]
    public bool isTipTiming;

    [Header("Game Timer")]
    public TextMeshProUGUI GameTimerText;
    [SerializeField] float CurrentGameTime;
    public static GameSystemManager Instance;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Selecting the first delivery slot to activate
        DeliverySlots = new List<GameObject>();
        DeliverySlots.AddRange(GameObject.FindGameObjectsWithTag("DeliverySlot"));
        SlotIndex = Random.Range(0, DeliverySlots.Count);
        GameObject FirstDeliverySlot = DeliverySlots[SlotIndex];
        FirstDeliverySlot.GetComponent<DeliverySlot>().UpdateDeliveryStatus(true);

        //Setting the GSM Instance
        Instance = this;

        isTipTiming = true;

        highScore = PlayerPrefs.GetInt("HighScore", 0);
        


    }

    // Singleton for GM, as we only ever want one
    void Awake()
    {
        if (Instance)
        {
            DestroyImmediate(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    void Update()
    {
        //Game Timer Countdown
        if(SceneManager.GetActiveScene().name == GameScene.name)
        {
            CurrentGameTime -= Time.deltaTime;
        int timerMinutes = Mathf.FloorToInt(CurrentGameTime / 60);
        int timerSeconds = Mathf.FloorToInt(CurrentGameTime % 60);
        GameTimerText.text = string.Format("{0:00}:{1:00}", timerMinutes, timerSeconds);

            if(CurrentGameTime < 1)
            {
                //End the game and move to results scene
                SceneManager.LoadScene("EndResultsScene");
                DontDestroyOnLoad(this);
            }

            
            // delete save for testing
            if (Input.GetKeyDown(KeyCode.E))
            {
                PlayerPrefs.DeleteKey("HighScore");
                Debug.Log("delete save" + highScore);
            }


        }

    }

    public void NextDelivery(GameObject PreviousDelivery, int TipValue)
    {
        //Preventing the previous slot from being selected again in the list - adding it back later
        //Selecting the next delivery slot

        DeliverySlots.Remove(PreviousDelivery);
        SlotIndex = Random.Range(0, DeliverySlots.Count);
        NextActiveDelivery = DeliverySlots[SlotIndex];
        NextActiveDelivery.GetComponent<DeliverySlot>().UpdateDeliveryStatus(true);
        GiveCashAndTips(TipValue);
        DeliverySlots.Add(PreviousDelivery);

        TipTimer = 20;
        isTipTiming = true;

        if (CurrentCashEarned > highScore)
        {
            PlayerPrefs.SetInt("HighScore", CurrentCashEarned);
            PlayerPrefs.Save();
            //Debug.Log(highScore);
        }


    }

    void GiveCashAndTips(int TipAmount)
    {
        //SHOULD MAKE TIP VALUE BE BASED ON HOW QUICK PLAYER DELIVERS

        // TO DO
        // This could be the place to then use our timer value with prortion to the cash given to the player
        // Im thinking we could divide a constant 1 over the timer to give us a multiplier, lets say at a perfect 1 second score we 
        // have a max tip of $300, constant 1 / 1 second score = 1, 1 x 300 = 300

        //Currently getting a random int for the tip
        DeliveryTips = TipAmount;
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

        //Reseting the notifi text
        yield return new WaitForSeconds(1f);
        TipsNotifiText.enabled = false;
        TipsNotifiText.color = new Color32(255, 255, 255, 255);
    }


}
