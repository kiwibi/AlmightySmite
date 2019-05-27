﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityAlive : MonoBehaviour
{
    private DamageDealer parentScript;                                                                                                                        //scriptet där dmghandling händer
    private float Timer;                                                                                                                                      //timer för att allting inte ska göra dmg varje frame
    public Animator[] ChildAnims;
    public AudioSource StrengthAudio;
    public AudioClip[] StrengthClips;

    void Start()
    {
        parentScript = GetComponentInParent<DamageDealer>();                                                                                                  //hämta komponenter
        Timer = 0.0f;                                                                                                                                         //timern sätts till 0
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag != "Land" && col.gameObject.tag != "MainCamera" && col.gameObject.tag != "Ocean")                                                                               //om den inte krockar med någonting vi inte bryr oss om
        {
            if(col.gameObject.name == "Ligtning(Clone)")                                                                                                            //om det va lightning som träffade
            {
                parentScript.OnChildTriggerEnter2D(col);                                                                                                      //aktivera funktionen i parentscriptet
                Destroy(col.gameObject);
                return;                                                                                                                                       //hoppa ur scriptet
            }
            Timer += 1;                                                                                                                                       //för varje frame öka timern med 1
            if (Timer > 10)                                                                                                                                    //när det har gått mer än 5 frames
            {
                parentScript.OnChildTriggerEnter2D(col);                                                                                                      //aktivera funktionen i parentscriptet                                                                      
                Timer = 0.0f;                                                                                                                                 //timern sätts till 0 igen
            }   
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {

        DamageDealer damageDealer = col.gameObject.GetComponent<DamageDealer>();
        if (damageDealer != null)
        {
            if (!parentScript.damageType.TakesDamageFrom.Contains(damageDealer.damageType))                                                                               //om den inte krockar med någonting vi inte bryr oss om
            {
                if(gameObject.tag == "BossCity")
                {
                    if(ChildAnims[0].GetBool("TornadoCity") == true && damageDealer.damageType.name == "Tornado")
                    {
                        ChildAnims[0].SetBool("Active", true);
                    }
                    if (ChildAnims[0].GetBool("LightningCity") == true && damageDealer.damageType.name == "Lightning")
                    {
                        ChildAnims[0].SetBool("Active", true);
                    }
                    if (ChildAnims[0].GetBool("EarthquakeCity") == true && damageDealer.damageType.name == "EarthQuake")
                    {
                        ChildAnims[0].SetBool("Active", true);
                    }
                    if (ChildAnims[1].GetBool("TornadoCity") == true && damageDealer.damageType.name == "Tornado")
                    {
                        ChildAnims[1].SetBool("Active", true);
                    }
                    if (ChildAnims[1].GetBool("LightningCity") == true && damageDealer.damageType.name == "Lightning")
                    {
                        ChildAnims[1].SetBool("Active", true);
                    }
                    if (ChildAnims[1].GetBool("EarthquakeCity") == true && damageDealer.damageType.name == "EarthQuake")
                    {
                        ChildAnims[1].SetBool("Active", true);
                    }
                }
                else
                    ChildAnims[0].SetBool("Active", true);
            }
            if(parentScript.damageType.name == "TornadoCity")
            {
                StrengthAudio.clip = StrengthClips[0];
                StrengthAudio.Play();
            }
            else if (parentScript.damageType.name == "LightningCity")
            {
                StrengthAudio.clip = StrengthClips[1];
                StrengthAudio.Play();
            }
            //else if (parentScript.damageType.name == "EarthQuakeCity")
            //{
            //    StrengthAudio.clip = StrengthClips[2];
            //    StrengthAudio.Play();
            //}
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag != "Land" && col.gameObject.tag != "MainCamera" && col.gameObject.tag != "Ocean")                                                                               //om den inte krockar med någonting vi inte bryr oss om
        {
            ChildAnims[0].SetBool("Active", false);
            if (gameObject.tag == "BossCity")
                ChildAnims[1].SetBool("Active", false);
        }
    }
}
