using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightningCharges : MonoBehaviour
{
    public Image[] Charges;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        switch (AbilitiesInput.LightningPool)
        {
            case 0:
                foreach (var Charge in Charges)
                {
                    Charge.gameObject.SetActive(false);
                }
                break;
            case 1:
                Charges[0].gameObject.SetActive(true);
                Charges[1].gameObject.SetActive(false);
                Charges[2].gameObject.SetActive(false);
                Charges[3].gameObject.SetActive(false);
                Charges[4].gameObject.SetActive(false);
                break;
            case 2:
                Charges[0].gameObject.SetActive(true);
                Charges[1].gameObject.SetActive(true);
                Charges[2].gameObject.SetActive(false);
                Charges[3].gameObject.SetActive(false);
                Charges[4].gameObject.SetActive(false);
                break;
            case 3:
                Charges[0].gameObject.SetActive(true);
                Charges[1].gameObject.SetActive(true);
                Charges[2].gameObject.SetActive(true);
                Charges[3].gameObject.SetActive(false);
                Charges[4].gameObject.SetActive(false);
                break;
            case 4:
                Charges[0].gameObject.SetActive(true);
                Charges[1].gameObject.SetActive(true);
                Charges[2].gameObject.SetActive(true);
                Charges[3].gameObject.SetActive(true);
                Charges[4].gameObject.SetActive(false);
                break;
            case 5:
                foreach (var Charge in Charges)
                {
                    Charge.gameObject.SetActive(true);
                }
                break;
        }
    }
}
