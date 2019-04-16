using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoBehaviour : MonoBehaviour
{
    public float TornadoSpeed;                                                             //variabel för hur snabbt tornadon ska röra sig
    public float TornadoMaxSpeed;                                                          //variabel som säger maxhastigheten tornadon kan röra sig i
    public float ChangeDirection;                                                          //timer variebel för hur ofta den ska kolla efter riktändring

    private Vector2 Direction;                                                             //lokal variabel för vilken riktning den åker mot
    private SpriteRenderer TornadoSprite;                                                  //variabel för att förvara spriten för att kunna agera med den

    private bool moving;

    void Start()
    {
        Direction = WindBehaviour.GetWindMovement();                                       //tar rikningen vinden är när den spawnas
        TornadoSprite = GetComponentInChildren<SpriteRenderer>();                          //tar spriten för tornado objectets child tornadosprite
        moving = false;                                                                    //den börjar med att inte röra sig då den antas laddas upp även om det är 0.1 sekund
    }

    void Update()
    {
        TurnHandling();                                                                    //kallar funktionen turnhandling() som kollar om den ska svänga om lite
        if(AbilitiesInput.Charging == true)                                                //kollar om tornadon laddas
        {
            TornadoSpeed += Time.deltaTime;                                                //farten på tornadon ökas varje frame med deltatime så länge den laddas
            TornadoSprite.transform.localScale += new Vector3(0.1f, 0.1f);                 //spriten skalas upp med 0.1 varje frame den laddar
            if (TornadoSpeed > TornadoMaxSpeed)                                            //kollar om den nuvarande farten är över maxfarten
            {
                TornadoSpeed = TornadoMaxSpeed;                                            //sätter den nuvarande farten för att försäkra att den aldrig är snabbare än vad vi vill
                AbilitiesInput.Charging = false;                                            //sätter boolen charging till false så att den inte kan laddas mer
            }
        }
        else                                                                               //ifall den inte chargas gör det inom nästa { }
        {
            moving = true;                                                                 //den rör på sig när den inte laddas så det sätts till true
            GetComponent<DestroyAfterSecond>().enabled = true;                             //sätter igång scriptet som förstör objektet efter en viss tid
        }
        if(moving == true)                                                                 //om tornadon "rör" på sig
            transform.Translate(Direction * Time.deltaTime * TornadoSpeed);                //flyttar tornadon åt riktningen(Direction) i deltatime så att den inte är beroende på framecount och farten beroende på hastigheten
    }

    private void TurnHandling()
    {
        ChangeDirection -= Time.deltaTime;                                                 //timern ökar lite varje frame
        if (ChangeDirection < 0)                                                           //om timern har gått förbi noll
        {
            Direction = Vector3.Lerp(Direction, WindBehaviour.GetWindMovement(), Mathf.SmoothStep(0f, 1f, 0.3f));                   //börjar röra från startpunkten(Direction) till den nya vindriktningen(WindBehaviour.GetWindMovement)
                                                                                                                                    //mathf.smoothstep är ungefär likadan men den går snabbare i början och långsamare i slutet
            ChangeDirection = 2;                                                                                                    //sätter igång timern igen så att den inte kör klart rotationen
        }
    }

    void OnDestroy()
    {
        AbilitiesInput.TornadoSpawned = false;                                              //när tornadon dör ut så sätts boolen till false för det finns ingen tornado spawnad längre                                              
    }
}
