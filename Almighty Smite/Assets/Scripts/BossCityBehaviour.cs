using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCityBehaviour : MonoBehaviour
{
    enum CityWeakness
    {
        EARTHQUAKE,
        LIGHTNING,
        TORNADO
    }

    private CityWeakness CurrentWeakness;
    public DamageType[] DamageKinds;
    private float CurrentHealth;
    public float MaxHealth;
    private readonly float TickTime = 1.0f;
    private float Timer = 0.0f;
    private DamageDealer dmgDealer;
    private ProgressbarBehaviour Pool;
    private CityMaster CitiesAlive;
    private SpriteRenderer CityRenderer;
    private CircleCollider2D CityCollider;
    public float minWeaknessTime;
    public float maxWeaknessTime;
    private float Weaknesstimer;

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
        if(Weaknesstimer < Time.time)
        {
            ChooseType();
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
            CurrentWeakness = ChooseType();
            citySoundPlayer.clip = citySoundClips[0];
            citySoundPlayer.Play();
        }
    }

    private CityWeakness ChooseType()
    {
        CityWeakness TmpType = CityWeakness.EARTHQUAKE;
        int index = Random.Range(3, 6);
        switch(index)
        {
            case 3:
                TmpType = CityWeakness.EARTHQUAKE;
                dmgDealer.damageType = DamageKinds[3];
                break;
            case 4:
                TmpType = CityWeakness.LIGHTNING;
                dmgDealer.damageType = DamageKinds[4];
                break;
            case 5:
                TmpType = CityWeakness.TORNADO;
                dmgDealer.damageType = DamageKinds[5];
                break;
        }
        Weaknesstimer = Time.time + Random.Range(minWeaknessTime, maxWeaknessTime);
        return TmpType;
    }

    public void DealDamage(int DamageAmount, DamageType AttackType)
    {
        CurrentHealth -= DamageAmount;                                                                                                  //minskar health med så mycket dmg attacken hade
        Instantiate(dirtSplatter, transform);
    }

    public void NegativeEffects()
    {

    }
}
