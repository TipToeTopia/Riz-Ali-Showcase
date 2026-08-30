using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Slider SensSlider;
    public TextMeshProUGUI SensSliderText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitSettings();
    }

    public void UpdateSliderText()
    {
        float SensSliderValue = Mathf.Round(SensSlider.value * 10f) * 0.1f;
       
        SensSliderText.SetText(SensSliderValue.ToString());
    }

    void InitSettings()
    {
        //TODO
        //Lets pull the player settings and update everything correctly here maybe???
    }
}
