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
    private CircleCollider2D TornadoCollider;                                              //variabel för att förvara en collider
    private DamageDealer DmgDealer;
    private float IncreasingDmg;
    private bool moving;
    private float DestructionTime;

    void Start()
    {
        Direction = WindBehaviour.GetWindMovement();                                       //tar rikningen vinden är när den spawnas
        TornadoSprite = GetComponentInChildren<SpriteRenderer>();                          //tar spriten för tornado objectets child tornadosprite
        moving = false;                                                                    //den börjar med att inte röra sig då den antas laddas upp även om det är 0.1 sekund
        TornadoCollider = GetComponent<CircleCollider2D>();                                //tar en circlecollider på objektet
        DmgDealer = GetComponent<DamageDealer>();
        IncreasingDmg = DmgDealer.DamageAmount;
        DestructionTime = GetComponent<DestroyAfterSecond>().DestroyAfter;
    }

    void Update()
    {
        if(TornadoSpeed < 0.2f || gameObject.transform.localScale == new Vector3(0.1f,0.1f))
        {
            Destroy(gameObject);
        }
        TurnHandling();                                                                    //kallar funktionen turnhandling() som kollar om den ska svänga om lite
        if(AbilitiesInput.Charging == true)                                                //kollar om tornadon laddas
        {
            
            UpdateSize(true);                                                                  //Kallar funktionen som updaterar storlek på bilden och storleken på collidern
            if (TornadoSpeed > TornadoMaxSpeed)                                            //kollar om den nuvarande farten är över maxfarten
            {
                DmgDealer.DamageAmount = Mathf.RoundToInt(IncreasingDmg);
                TornadoSpeed = TornadoMaxSpeed;                                            //sätter den nuvarande farten för att försäkra att den aldrig är snabbare än vad vi vill
                AbilitiesInput.Charging = false;                                            //sätter boolen charging till false så att den inte kan laddas mer
            }
        }
        else                                                                               //ifall den inte chargas gör det inom nästa { }
        {
            DmgDealer.DamageAmount = Mathf.RoundToInt(IncreasingDmg);
            moving = true;                                                                 //den rör på sig när den inte laddas så det sätts till true
            GetComponent<DestroyAfterSecond>().enabled = true;                             //sätter igång scriptet som förstör objektet efter en viss tid
        }
        if (moving == true)                                                                 //om tornadon "rör" på sig
        {
            transform.Translate(Direction * Time.deltaTime * TornadoSpeed);                //flyttar tornadon åt riktningen(Direction) i deltatime så att den inte är beroende på framecount och farten beroende på hastigheten
            UpdateSize(false);
        }
        
    }

    private void TurnHandling()
    {
        ChangeDirection -= Time.deltaTime;                                                 //timern ökar lite varje frame
        if (ChangeDirection < 0)                                                           //om timern har gått förbi noll
        {
            Direction = Vector3.Lerp(Direction, WindBehaviour.GetWindMovement(), Mathf.SmoothStep(0f, 1f, 0.3f));                   //börjar röra från startpunkten(Direction) till den nya vindriktningen(WindBehaviour.GetWindMovement)
                                                                                                                                    //mathf.smoothstep är ungefär likadan men den går snabbare i början och långsamare i slutet
            ChangeDirection = 0.5f;                                                                                                    //sätter igång timern igen så att den inte kör klart rotationen
        }
    }

    private void UpdateSize(bool up)
    {
        float SizeIncrement = 0.1f;
        if (up == true)
        {
            TornadoSprite.transform.localScale += new Vector3(SizeIncrement, SizeIncrement);                 //spriten skalas upp med 0.1 varje frame den laddar
            TornadoCollider.radius += (SizeIncrement / 10);                                                  //Då radiusen är en tiondel av startskalan tornadon börjar växer den en tiondel så snabbt
            TornadoSpeed += Time.deltaTime;                                                //farten på tornadon ökas varje frame med deltatime så länge den laddas
            IncreasingDmg += Time.deltaTime;
        }
        else
        {
            TornadoSprite.transform.localScale -= new Vector3(SizeIncrement / 2, SizeIncrement / 2);                 //spriten skalas upp med 0.1 varje frame den laddar
            TornadoCollider.radius -= ((SizeIncrement / 2) / 10);                                                  //Då radiusen är en tiondel av startskalan tornadon börjar växer den en tiondel så snabbt
            TornadoSpeed -= 0.2f * Time.deltaTime;                                                //farten på tornadon ökas varje frame med deltatime så länge den laddas
            IncreasingDmg -= 0.2f * Time.deltaTime;
        }
    }

    void OnDestroy()
    {
        AbilitiesInput.TornadoSpawned = false;                                              //när tornadon dör ut så sätts boolen till false för det finns ingen tornado spawnad längre                                              
    }
}
