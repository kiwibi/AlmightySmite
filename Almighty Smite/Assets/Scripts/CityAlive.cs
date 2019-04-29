﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityAlive : MonoBehaviour
{
    private DamageDealer parentScript;
    private float Timer;

    void Start()
    {
        parentScript = GetComponentInParent<DamageDealer>();
        Timer = 0.0f;
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag != "Land" && col.gameObject.tag != "MainCamera")
        {
            if(col.gameObject.name == "Lightning")
            {
                parentScript.OnChildTriggerEnter2D(col);
                return;
            }
            Timer += 1;
            if (Timer > 5)
            {
                parentScript.OnChildTriggerEnter2D(col);
                Timer = 0.0f;
            }
        }
    }
}
