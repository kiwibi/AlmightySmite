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
    private float CurrentDmg;
    private float MaxDmg;
    private bool moving;
    private bool growing;

    void Start()
    {
        Direction = WindBehaviour.GetWindMovement();                                       //tar rikningen vinden är när den spawnas
        TornadoSprite = GetComponentInChildren<SpriteRenderer>();                          //tar spriten för tornado objectets child tornadosprite
        moving = false;                                                                    //den börjar med att inte röra sig då den antas laddas upp även om det är 0.1 sekund
        growing = true;
        TornadoCollider = GetComponent<CircleCollider2D>();                                //tar en circlecollider på objektet
        DmgDealer = GetComponent<DamageDealer>();
        CurrentDmg = DmgDealer.DamageAmount;
        MaxDmg = CurrentDmg;
        EventManager.TriggerEvent("Tornado");
    }

    void Update()
    {
        if (TornadoSpeed < 0.2f || TornadoSprite.transform.localScale.x <= 0.00001f)
        {
            Destroy(gameObject);
        }
        if (AbilitiesInput.Charging == true)                                                //kollar om tornadon laddas
        {
            ChargingUpdate();
            Direction = WindBehaviour.GetWindMovement();                                       //tar rikningen vinden är när den spawnas
            UpdateSize(true, AbilitiesInput.TornadoIncrease() / 100);
        }
        else
        {
            DmgDealer.DamageAmount = Mathf.RoundToInt(CurrentDmg);
            moving = true;                                                                 //den rör på sig när den inte laddas så det sätts till true
            CurrentDmg = Mathf.MoveTowards(CurrentDmg, MaxDmg, 5 * Time.deltaTime);
            UpdateSize(false, 0.001f);
        }
        if(moving == true)
            MovingUpdate();
    }

    private void ChargingUpdate()
    {
        TornadoMaxSpeed += Time.deltaTime;                                                //farten på tornadon ökas varje frame med deltatime så länge den laddas
        MaxDmg += 2;
    }

    private void MovingUpdate()
    {
        transform.Translate(Direction * Time.deltaTime * TornadoSpeed);                //flyttar tornadon åt riktningen(Direction) i deltatime så att den inte är beroende på framecount och farten beroende på hastigheten
        if (CurrentDmg >= MaxDmg && growing == true)
            growing = false;

        //if (growing == true)
        //    UpdateSize(true);
        //else
        //    UpdateSize(false);

        TurnHandling();                                                                    //kallar funktionen turnhandling() som kollar om den ska svänga om lite
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

    private void UpdateSize(bool up, float increase)
    {
        if (increase < 0)
            increase *= -1;
        float SizeIncrement = increase;
        float RadiusIncrement = 0.1f;
        if (up == true)
        {
            TornadoSprite.transform.localScale += new Vector3(SizeIncrement / 2, SizeIncrement / 2);                 //spriten skalas upp med 0.1 varje frame den laddar
            TornadoCollider.radius += ((RadiusIncrement / 2) / 10);                                                  //Då radiusen är en tiondel av startskalan tornadon börjar växer den en tiondel så snabbt
            CurrentDmg = Mathf.MoveTowards(CurrentDmg, MaxDmg, 2);
            TornadoSpeed = Mathf.MoveTowards(TornadoSpeed, TornadoMaxSpeed, Time.deltaTime);
        }
        else
        {
            TornadoSprite.transform.localScale -= new Vector3(SizeIncrement, SizeIncrement);                 //spriten skalas upp med 0.1 varje frame den laddar
            TornadoCollider.radius -= (RadiusIncrement / 10);                                                  //Då radiusen är en tiondel av startskalan tornadon börjar växer den en tiondel så snabbt
            TornadoSpeed -= 0.2f * Time.deltaTime;                                                //farten på tornadon ökas varje frame med deltatime så länge den laddas
            MaxDmg -= 0.2f * Time.deltaTime;
        }
    }

    void OnDestroy()
    {
        EventManager.TriggerEvent("StopSound");
        AbilitiesInput.TornadoSpawned = false;                                              //när tornadon dör ut så sätts boolen till false för det finns ingen tornado spawnad längre                                              
    }
}
