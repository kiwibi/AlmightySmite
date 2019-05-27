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
    private CameraController CamController;
    public float damageGrowth;
    public float damageRoof;
    private Vector2 IndicatorDir;
    public SpriteRenderer IndicatorRenderer;
    private bool Charged;
    private bool OnCd;

    void Start()
    {
        OnCd = true;
        Charged = false;
        CamController = GameObject.Find("CameraController").GetComponent<CameraController>();
        Direction = WindBehaviour.GetWindMovement();                                       //tar rikningen vinden är när den spawnas
        TornadoSprite = GetComponentInChildren<SpriteRenderer>();                          //tar spriten för tornado objectets child tornadosprite
        moving = false;                                                                    //den börjar med att inte röra sig då den antas laddas upp även om det är 0.1 sekund
        growing = true;
        TornadoCollider = GetComponent<CircleCollider2D>();                                //tar en circlecollider på objektet
        DmgDealer = GetComponent<DamageDealer>();
        CurrentDmg = DmgDealer.DamageAmount;
        MaxDmg = CurrentDmg;
        EventManager.TriggerEvent("Tornado");
        StartCoroutine(TornadoSpawnable(4));
    }

    void Update()
    {
        if (TornadoSpeed < 0.2f || TornadoSprite.transform.localScale.x <= 0.00001f)
        {
            Destroy(gameObject);
        }
        SetIndicator();
        if(transform.position.x <= CamController.MinX)
        {
            transform.position = new Vector3(transform.position.x + 57.58f, transform.position.y, 0);
        }
        else if(transform.position.x >= CamController.MaxX)
        {
            transform.position = new Vector3(transform.position.x - 57.58f, transform.position.y, 0);
        }
        if (AbilitiesInput.Charging == true && Charged == false)                                                //kollar om tornadon laddas
        {
            ChargingUpdate();
            Direction = WindBehaviour.GetWindMovement();                                       //tar rikningen vinden är när den spawnas
            if(AbilitiesInput.TornadoIncrease() != 0)
                UpdateSize(true, AbilitiesInput.TornadoIncrease() / 100);
        }
        else
        {
            Charged = true;
            DmgDealer.DamageAmount = Mathf.RoundToInt(MaxDmg);
            moving = true;                                                                 //den rör på sig när den inte laddas så det sätts till true
            CurrentDmg = Mathf.MoveTowards(MaxDmg, CurrentDmg, 5 * Time.deltaTime);
            UpdateSize(false, 0.001f);
        }
        if(moving == true)
            MovingUpdate();
    }

    private void ChargingUpdate()
    {
        TornadoMaxSpeed += 0.5f * Time.deltaTime;                                                //farten på tornadon ökas varje frame med deltatime så länge den laddas
        MaxDmg += damageGrowth;
        if (MaxDmg >= damageRoof)
            MaxDmg = damageRoof;
    }

    private void MovingUpdate()
    {
        transform.Translate(Direction * Time.deltaTime * TornadoSpeed * 1.2f);                //flyttar tornadon åt riktningen(Direction) i deltatime så att den inte är beroende på framecount och farten beroende på hastigheten
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
            Direction = Vector3.Lerp(Direction, WindBehaviour.GetWindMovement(), Mathf.SmoothStep(0f, 1f, 0.5f));                   //börjar röra från startpunkten(Direction) till den nya vindriktningen(WindBehaviour.GetWindMovement)
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
            TornadoCollider.radius += ((RadiusIncrement/ 10));                                                  //Då radiusen är en tiondel av startskalan tornadon börjar växer den en tiondel så snabbt
            IndicatorRenderer.transform.localScale += new Vector3(SizeIncrement * 4, SizeIncrement * 4);
            CurrentDmg = Mathf.MoveTowards(CurrentDmg, MaxDmg, 2);
            TornadoSpeed = Mathf.MoveTowards(TornadoSpeed, TornadoMaxSpeed, Time.deltaTime);
        }
        else
        {
            TornadoSprite.transform.localScale -= new Vector3(SizeIncrement / 6, SizeIncrement / 6);                 //spriten skalas upp med 0.1 varje frame den laddar
            IndicatorRenderer.transform.localScale -= new Vector3(SizeIncrement, SizeIncrement);
            TornadoCollider.radius -= (RadiusIncrement / 40);                                                  //Då radiusen är en tiondel av startskalan tornadon börjar växer den en tiondel så snabbt
            TornadoSpeed -= 0.2f * Time.deltaTime;                                                //farten på tornadon ökas varje frame med deltatime så länge den laddas
            MaxDmg -= 1 * Time.deltaTime;
        }
    }

    void OnDestroy()
    {
        if (gameObject != null)
        {
            EventManager.TriggerEvent("StopSound");
            if (OnCd == true)
                AbilitiesInput.TornadoSpawned = false;
        }
    }

    void SetIndicator()
    {
        IndicatorDir = WindBehaviour.GetWindMovement();
        if (IndicatorDir != Vector2.zero)
        {
            IndicatorDir.Normalize();
            Vector3 up = new Vector3(0, 0, -1);
            Quaternion rotation = Quaternion.LookRotation(IndicatorDir, up);
            rotation.x = 0;
            rotation.y = 0;
            IndicatorRenderer.transform.rotation = rotation;
        }
    }

    IEnumerator TornadoSpawnable(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        AbilitiesInput.TornadoSpawned = false;
        OnCd = false;
    }

    void OnApplicationQuit()
    {
        EventManager.TriggerEvent("StopSound");
    }
}
