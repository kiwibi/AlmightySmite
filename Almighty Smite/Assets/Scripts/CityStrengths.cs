using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityStrengths : MonoBehaviour
{
    private DamageDealer ParentDamage;
    private Animator[] StrengthAnimators;
    private void Awake()
    {
        ParentDamage = GetComponentInParent<DamageDealer>();
        StrengthAnimators = GetComponentsInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.tag == "BossCity")
        {
            if (ParentDamage.damageType.name == "WeakT")
            {
                StrengthAnimators[0].SetBool("TornadoCity", false);
                StrengthAnimators[0].SetBool("LightningCity", true);
                StrengthAnimators[0].SetBool("EarthquakeCity", false);
                StrengthAnimators[1].SetBool("TornadoCity", false);
                StrengthAnimators[1].SetBool("LightningCity", false);
                StrengthAnimators[1].SetBool("EarthquakeCity", true);
            }
            else if (ParentDamage.damageType.name == "WeakL")
            {
                StrengthAnimators[0].SetBool("TornadoCity", true);
                StrengthAnimators[0].SetBool("LightningCity", false);
                StrengthAnimators[0].SetBool("EarthquakeCity", false);
                StrengthAnimators[1].SetBool("TornadoCity", false);
                StrengthAnimators[1].SetBool("LightningCity", false);
                StrengthAnimators[1].SetBool("EarthquakeCity", true);
            }
            else if (ParentDamage.damageType.name == "WeakE")
            {
                StrengthAnimators[0].SetBool("TornadoCity", true);
                StrengthAnimators[0].SetBool("LightningCity", false);
                StrengthAnimators[0].SetBool("EarthquakeCity", false);
                StrengthAnimators[1].SetBool("TornadoCity", false);
                StrengthAnimators[1].SetBool("LightningCity", true);
                StrengthAnimators[1].SetBool("EarthquakeCity", false);
            }
        }
        else
        {
            if (ParentDamage.damageType.name == "TornadoCity" && StrengthAnimators[0].GetBool("TornadoCity") != true)
            {
                StrengthAnimators[0].SetBool("TornadoCity", true);
                StrengthAnimators[0].SetBool("LightningCity", false);
                StrengthAnimators[0].SetBool("EarthquakeCity", false);
            }
            else if (ParentDamage.damageType.name == "LightningCity" && StrengthAnimators[0].GetBool("LightningCity") != true)
            {
                StrengthAnimators[0].SetBool("TornadoCity", false);
                StrengthAnimators[0].SetBool("LightningCity", true);
                StrengthAnimators[0].SetBool("EarthquakeCity", false);
            }
            else if (ParentDamage.damageType.name == "EarthQuakeCity" && StrengthAnimators[0].GetBool("EarthquakeCity") != true)
            {
                StrengthAnimators[0].SetBool("TornadoCity", false);
                StrengthAnimators[0].SetBool("LightningCity", false);
                StrengthAnimators[0].SetBool("EarthquakeCity", true);
            }
        }
    }
}
