using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCityBehaviour : MonoBehaviour
{
    enum CityType
    {
        NORMAL,
        EARTHQUAKE,
        WATER,
        LIGHTNING,
        TORNADO
    }

    private CityType CurrentType;
    public DamageType[] DamageKinds;
    private float CurrentHealth;
    public float MaxHealth;
    private readonly float TickTime = 1.0f;
    private float Timer = 0.0f;
    private DamageType LastAttackedBy;
    private DamageDealer dmgDealer;
    private ProgressbarBehaviour Pool;
    private CityMaster CitiesAlive;
    private SpriteRenderer CityRenderer;
    private CircleCollider2D CityCollider;

    public Transform Minimap;
    public Transform Damage01;
    public Transform Damage02;
    public Transform Damage03;
    private Animator CityAnimator;
    private Transform Alive;
    private Transform Dead;
    public ParticleSystem dirtSplatter;
    public AudioSource citySoundPlayer;
    public AudioClip[] citySoundClips;

    void Start()
    {
        Pool = GameObject.Find("GameUI").GetComponent<ProgressbarBehaviour>();
        CitiesAlive = GameObject.Find("City Master").GetComponent<CityMaster>();
        CityRenderer = GetComponentInChildren<SpriteRenderer>();
        CityCollider = GetComponentInChildren<CircleCollider2D>();
        CurrentHealth = MaxHealth;
        Alive = transform.GetChild(0);
        Dead = transform.GetChild(1);
        CityAnimator = GetComponentInChildren<Animator>();
        Dead.gameObject.SetActive(false);
        dmgDealer = GetComponent<DamageDealer>();
        ChooseType();
        //citySoundPlayer.clip = citySoundClips[0];
        //citySoundPlayer.Play();
        
    }

    void Update()
    {
        Debug.Log(CurrentHealth);
        Timer += Time.deltaTime;
        if (Alive.gameObject.activeSelf == true)                                                                                          //om objektet Alive är aktivt i scenen gör funktionen AliveUpdate
        {
            AliveUpdate();
            Minimap.gameObject.SetActive(true);
            if (Timer > TickTime)
            {
                if (CitiesAlive.CitiesAlive < 17)
                {
                    Pool.ProgressPool += 0.0015f;
                }
                else { Pool.ProgressPool += 0; }
                Timer = 0.0f;
            }

            if (CurrentHealth < MaxHealth)
            {
                Damage01.gameObject.SetActive(true);
                if (CurrentHealth < MaxHealth / 1.5)
                {
                    Damage02.gameObject.SetActive(true);
                    if (CurrentHealth < MaxHealth / 2)
                    {
                        Damage03.gameObject.SetActive(true);
                    }
                }
            }
            else
            {
                Damage01.gameObject.SetActive(false);
                Damage02.gameObject.SetActive(false);
                Damage03.gameObject.SetActive(false);
            }
        }
        else
        {
            Minimap.gameObject.SetActive(false);
        }
    }

    private void AliveUpdate()
    {
        if (CurrentHealth <= 0)
        {
            //citySoundPlayer.clip = citySoundClips[1];
            //citySoundPlayer.Play();
            SwitchState();
        }
    }

    private void SwitchState()
    {
        if (Alive.gameObject.activeSelf == true)
        {
            Alive.gameObject.SetActive(false);
            Dead.gameObject.SetActive(true);
            transform.GetChild(2).localScale = new Vector3(0.7f, 0.7f);
            transform.GetChild(2).GetComponent<Animator>().SetBool("Dead", true);
            CityAnimator.SetBool("Upgrade1", false);
            CityAnimator.SetBool("Upgrade1", false);
        }
        else
        {
            transform.GetChild(2).GetComponent<Animator>().SetBool("Dead", false);
            transform.GetChild(2).localScale = new Vector3(0.27f, 0.27f);
            Alive.gameObject.SetActive(true);
            Dead.gameObject.SetActive(false);
            CurrentType = ChooseType();
            citySoundPlayer.clip = citySoundClips[0];
            citySoundPlayer.Play();
        }
    }

    private CityType ChooseType()
    {
        CityType TmpType = CityType.NORMAL;
        if (LastAttackedBy == null)
        {
            return TmpType;
        }
        else if (LastAttackedBy == DamageKinds[0])
        {
            TmpType = CityType.EARTHQUAKE;
            dmgDealer.damageType = DamageKinds[4];
        }
        else if (LastAttackedBy == DamageKinds[1])
        {
            TmpType = CityType.LIGHTNING;
            dmgDealer.damageType = DamageKinds[5];
        }
        else if (LastAttackedBy == DamageKinds[2])
        {
            TmpType = CityType.TORNADO;
            dmgDealer.damageType = DamageKinds[6];
        }
        else if (LastAttackedBy == DamageKinds[3])
        {
            TmpType = CityType.WATER;
            dmgDealer.damageType = DamageKinds[7];
        }
        return TmpType;
    }

    public void DealDamage(int DamageAmount, DamageType AttackType)
    {
        CurrentHealth -= DamageAmount;                                                                                                  //minskar health med så mycket dmg attacken hade
        LastAttackedBy = AttackType;                                                                                                    //va den sist blev attackerad av
        Instantiate(dirtSplatter, transform);
    }
}
