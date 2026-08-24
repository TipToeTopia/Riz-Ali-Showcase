using TMPro;
using UnityEngine;

public class DisplayTotalScore : MonoBehaviour
{
    public TextMeshProUGUI TotalCashEarnedText;
    public TextMeshProUGUI TotalTipsEarnedText;

    public TextMeshProUGUI TotalMoneyEarnedText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //Setting all the cash and tip values based on the GameSystemManager Instance created
        TotalCashEarnedText.text = ("Total Cash Earned: $" + GameSystemManager.Instance.CurrentCashEarned);
        TotalTipsEarnedText.text = ("Total Tips Earned: $" + GameSystemManager.Instance.CurrentTipsEarned);

        int TotalMoney = GameSystemManager.Instance.CurrentCashEarned + GameSystemManager.Instance.CurrentTipsEarned;
        TotalMoneyEarnedText.text = ("Total Money Earned: $" + TotalMoney);
    }
}
