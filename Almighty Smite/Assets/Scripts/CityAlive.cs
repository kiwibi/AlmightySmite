using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityAlive : MonoBehaviour
{
    private DamageDealer parentScript;                                                                                                                        //scriptet där dmghandling händer
    private float Timer;                                                                                                                                      //timer för att allting inte ska göra dmg varje frame
        
    void Start()
    {
        parentScript = GetComponentInParent<DamageDealer>();                                                                                                  //hämta komponenter
        Timer = 0.0f;                                                                                                                                         //timern sätts till 0
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag != "Land" && col.gameObject.tag != "MainCamera" && col.gameObject.tag != "Ocean")                                                                               //om den inte krockar med någonting vi inte bryr oss om
        {
            if(col.gameObject.name == "Lightning")                                                                                                            //om det va lightning som träffade
            {
                parentScript.OnChildTriggerEnter2D(col);                                                                                                      //aktivera funktionen i parentscriptet
                return;                                                                                                                                       //hoppa ur scriptet
            }
            Timer += 1;                                                                                                                                       //för varje frame öka timern med 1
            if (Timer > 5)                                                                                                                                    //när det har gått mer än 5 frames
            {
                parentScript.OnChildTriggerEnter2D(col);                                                                                                      //aktivera funktionen i parentscriptet                                                                      
                Timer = 0.0f;                                                                                                                                 //timern sätts till 0 igen
            }   
        }
    }
}
