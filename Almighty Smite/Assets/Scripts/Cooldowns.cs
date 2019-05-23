using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cooldowns : MonoBehaviour
{
    public static Cooldowns instance;
    public static bool TornadoOnCD;
    public static float TornadoCD;
    public static bool LightningOnCD;
    public static float LightningCD;
    private float TornadoTimeStamp;
    private float LightningTimeStamp;
    private Image TornadoIcon;
    public Image LightningIcon;
    // Start is called before the first frame update
    void Start()
    {
        TornadoIcon = transform.GetChild(1).GetComponent<Image>();
        instance = this;
        Cooldowns.TornadoCD = 0;
        Cooldowns.TornadoOnCD = false;
        TornadoIcon.fillAmount = Cooldowns.TornadoCD;
    }

    // Update is called once per frame
    void Update()
    {
        TornadoIcon.fillAmount -= 0.25f * Time.deltaTime;
        LightningIcon.fillAmount -= 1 * Time.deltaTime;
        if (TornadoTimeStamp < Time.time)
        {
            Cooldowns.TornadoOnCD = false;
        }
        if(LightningTimeStamp < Time.time)
        {
            Cooldowns.LightningOnCD = false;
        }
    }

    public void SetCooldown(float seconds, string name)
    {
        if (name == "Tornado")
        {
            instance.TornadoIcon.fillAmount = 1;
            Cooldowns.TornadoOnCD = true;
            TornadoTimeStamp = Time.time + seconds;
        }
        else if (name == "Lightning")
        {
            instance.LightningIcon.fillAmount = 1;
            Cooldowns.LightningOnCD = true;
            LightningTimeStamp = Time.time + seconds;
        }
        else
            return;
    }
}
