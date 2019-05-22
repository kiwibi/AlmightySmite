using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityStrengths : MonoBehaviour
{
    private DamageDealer ParentDamage;
    private Animator StrengthAnimator;
    private void Awake()
    {
        ParentDamage = GetComponentInParent<DamageDealer>();
        StrengthAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ParentDamage.damageType.name == "TornadoCity" && StrengthAnimator.GetBool("TornadoCity") != true)
        {
            StrengthAnimator.SetBool("TornadoCity", true);
            StrengthAnimator.SetBool("LightningCity", false);
            StrengthAnimator.SetBool("EarthquakeCity", false);
        }
        else if (ParentDamage.damageType.name == "LightningCity" && StrengthAnimator.GetBool("LightningCity") != true)
        {
            StrengthAnimator.SetBool("TornadoCity", false);
            StrengthAnimator.SetBool("LightningCity", true);
            StrengthAnimator.SetBool("EarthquakeCity", false);
        }
        else if (ParentDamage.damageType.name == "EarthQuakeCity" && StrengthAnimator.GetBool("EarthquakeCity") != true)
        {
            StrengthAnimator.SetBool("TornadoCity", false);
            StrengthAnimator.SetBool("LightningCity", false);
            StrengthAnimator.SetBool("EarthquakeCity", true);
        }
    }
}
