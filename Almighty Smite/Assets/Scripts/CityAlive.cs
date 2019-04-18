using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityAlive : MonoBehaviour
{
    private DamageDealer parentScript;
    private float Timer;
    // Start is called before the first frame update
    void Start()
    {
        parentScript = GetComponentInParent<DamageDealer>();
        Timer = 0.0f;
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag != "Land" && col.gameObject.tag != "MainCamera")
        {
            Timer += 1;
            Debug.Log(Timer);
            if (Timer > 5)
            {
                parentScript.OnChildTriggerEnter2D(col);
                Timer = 0.0f;
            }
        }
    }
}
