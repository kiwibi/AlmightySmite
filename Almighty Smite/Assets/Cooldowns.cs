using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cooldowns : MonoBehaviour
{
    public static Cooldowns instance;
    public static bool LightningOnCD;
    public static float LightingCD;
    private float timeStamp;
    private Image CDIcon;
    // Start is called before the first frame update
    void Start()
    {
        CDIcon = transform.GetChild(1).GetComponent<Image>();
        instance = this;
        Cooldowns.LightingCD = 0;
        Cooldowns.LightningOnCD = false;
        CDIcon.fillAmount = Cooldowns.LightingCD;
    }

    // Update is called once per frame
    void Update()
    {
        CDIcon.fillAmount -= .0043f;
        if (timeStamp < Time.time)
        {
            Cooldowns.LightningOnCD = false;
        }
    }

    public void SetCooldown(float seconds)
    {
        instance.CDIcon.fillAmount = 1;
        Cooldowns.LightningOnCD = true;
        timeStamp = Time.time + seconds;
    }
}
