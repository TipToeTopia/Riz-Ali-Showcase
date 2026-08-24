using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ThrowableCooldown : MonoBehaviour
{
    [SerializeField] float ThrowCooldown = 1;
    public Image CooldownImage;


    // Update is called once per frame
    void Update()
    {
        //Input to throw the Pizza
        if (Input.GetMouseButtonDown(1) && ThrowCooldown == 1)
        {
            StartCoroutine(StartCooldown());
        }
    }

    IEnumerator StartCooldown()
    {
        //As long as the cooldown is active the cooldown image should run
        while (ThrowCooldown > 0)
        {
            ThrowCooldown -= Time.deltaTime;
            CooldownImage.enabled = true;
            CooldownImage.fillAmount -= Time.deltaTime;
            yield return null;
        }
        
        //Reset all the cooldown value
        ThrowCooldown = 1;
        CooldownImage.fillAmount = 1;
        CooldownImage.enabled = false;

    }
}
