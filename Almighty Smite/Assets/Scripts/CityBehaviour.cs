using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CityBehaviour : MonoBehaviour
{
    enum CityType                                                                                                                       //Enum som ger stringen av bokstäver en siffra      
    {
        NORMAL,                                                                                                                         //0
        EARTHQUAKE,                                                                                                                     //1
        WATER,                                                                                                                          //2
        LIGHTNING,                                                                                                                      //3
        TORNADO                                                                                                                         //4
    }
    private float TickTime = 0.9f;
    private float Timer = 0.0f;
    public DamageType[] DamageKinds;                                                                                                    //en array för att hålla alla olika sorters dmg den ska känna till                                                                                                 //en arrat för att hålla alla sorters olika smoke så den vet vad som ska spawnas
    public Sprite[] DifferentCities;
    private SpriteRenderer CityRenderer;
    private CircleCollider2D CityCollider;
    private ProgressbarBehaviour Pool;
    private CityMaster CitiesAlive;
    private int CurrentLevel;                                                                                                           //vilken lvl staden är. bestämmer vilka värden som används
    public float MaxHealth;                                                                                                             //max health för staden
    private float MaxUpgradeTimer;                                                                                                       //hur lång tid ska det ta att uppgradera stadens level
    private float UpgradeTimer;                                                                                                         //vart timer ligger just nu
    private float MaxRespawnTimer;                                                                                                       //hur lång tid ska det ta för att staden ska spawna igen
    private float RespawnTimer;                                                                                                         //vart tiden ligger just nu
    private float CurrentHealth;                                                                                                        //vart healthen ligger just nu
    private CityType CurrentType;                                                                                                       //vilken sorts stad den är just nu bestämt av enumens i början av scriptet
    public int SpawnIndex;                                                                                                              //i vilken collider den spawnade i
    private DamageType LastAttackedBy;                                                                                                  //vilken sorts damage attackerade staden sist
    private DamageDealer dmgDealer;                                                                                                     //scriptet för damagedealer

    private bool respawnSet;
    public Transform Minimap01;
    public Transform Minimap02;
    public Transform Minimap03;
    public Transform Damage01;
    public Transform Damage02;
    public Transform Damage03;
    private Animator CityAnimator;
    private Transform Alive;                                                                                                            //child objektet CityAlive
    private Transform Dead;                                                                                                             //child objektet CityDead
    public GameObject FloatingScore;
    public ParticleSystem dirtSplatter;
    public ParticleSystem rippleEffect;
    public AudioSource citySoundPlayer;
    private int AddToScore;
    public AudioClip[] citySoundClips;
    
    void Start()
    {
        AddToScore = 0;
        respawnSet = false;
        MaxUpgradeTimer = Random.Range(8.0f, 15.0f);
        MaxRespawnTimer = Random.Range(15f, 60.0f);
        Pool = GameObject.Find("GameUI").GetComponent<ProgressbarBehaviour>();
        CitiesAlive = GameObject.Find("City Master").GetComponent<CityMaster>();
        CityRenderer = GetComponentInChildren<SpriteRenderer>();
        CityCollider = GetComponentInChildren<CircleCollider2D>();
        CurrentLevel = 1;                                                                                                               //staden börjar på lvl 1
        UpgradeTimer = MaxUpgradeTimer;                                                                                                 //sätter alla timers + health till sina max values
        CurrentHealth = MaxHealth;
        RespawnTimer = MaxRespawnTimer;

        Alive = transform.GetChild(0);                                                                                                  //hämtar komponenterna från de två child objekten
        Dead = transform.GetChild(1);
        CityAnimator = GetComponentInChildren<Animator>();

        Dead.gameObject.SetActive(false);                                                                                               //sätter den döda till false
        dmgDealer = GetComponent<DamageDealer>();                                                                                       //hämtar komponenterna från damagedealer scriptet
        CityRenderer.sprite = DifferentCities[CurrentLevel - 1];
        ChooseType();                                                                                                                   //callar funktionen ChooseType
        citySoundPlayer.clip = citySoundClips[0];
        citySoundPlayer.Play();
        Instantiate(rippleEffect, transform);
    }

    
    void Update()
    {
        Timer += Time.deltaTime;
        SetTickRate();
        if(Alive.gameObject.activeSelf == true)                                                                                          //om objektet Alive är aktivt i scenen gör funktionen AliveUpdate
        {
            AliveUpdate();
            if (CurrentLevel == 1)
            {
                if (Timer > TickTime)
                {
                    if (AssistantBehaviour.Tutorial == false)
                    {
                        if (EndGame.CommenceTheEndGaame == false)
                        {
                            if (CitiesAlive.CitiesAlive < 17)
                            {
                                Pool.ProgressPool += 0.0013f;
                            }
                            else { Pool.ProgressPool += 0.0015f; }
                        }
                        Pool.ProgressPool += 0.0001f;
                    }
                    Timer = 0.0f;
                }
                Minimap01.gameObject.SetActive(true);
            } else { Minimap01.gameObject.SetActive(false); }
            if (CurrentLevel == 2)
            {
                if (Timer > TickTime)
                {
                    if (AssistantBehaviour.Tutorial == false)
                    {
                        if (EndGame.CommenceTheEndGaame == false)
                        {
                            if (CitiesAlive.CitiesAlive < 17)
                            {
                                Pool.ProgressPool += 0.0018f;
                            }
                            else { Pool.ProgressPool += 0.0015f; }
                        } 
                        else
                        {
                            Pool.ProgressPool += 0.0001f;
                        }
                    }
                    Timer = 0.0f;
                }
                Minimap02.gameObject.SetActive(true);
            } else { Minimap02.gameObject.SetActive(false); }
            if (CurrentLevel == 3)
            {
                if (Timer > TickTime)
                {
                    if (AssistantBehaviour.Tutorial == false)
                    {
                        if (EndGame.CommenceTheEndGaame == false)
                        {
                            if (CitiesAlive.CitiesAlive < 17)
                            {
                                Pool.ProgressPool += 0.0023f;
                            }
                            else { Pool.ProgressPool += 0.0004f * Timer; }
                        }
                        else
                        {
                            Pool.ProgressPool += 0.0001f;
                        }
                    }
                    Timer = 0.0f;
                }
                Minimap03.gameObject.SetActive(true);
            } else { Minimap03.gameObject.SetActive(false); }

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
            } else
            {
                Damage01.gameObject.SetActive(false);
                Damage02.gameObject.SetActive(false);
                Damage03.gameObject.SetActive(false);
            }
        }
        else                                                                                                                             //annars gör deadUpdate
        {
            DeadUpdate();
        }
    }

    private void AliveUpdate()                                                                                                      
    {  
        if(CurrentHealth <= 0)                                                                                                          //om stadens health är mindre eller likamed 0 byt till död
        {
            if (AssistantBehaviour.Tutorial == false)
            {
                if (CurrentLevel == 1)
                {
                    ScoreManaging.AddScore(AddToScore);
                    if (EndGame.CommenceTheEndGaame == false)
                    {
                        if (CitiesAlive.CitiesAlive > 8)
                        {
                            AddToScore = 75;
                            Pool.ProgressPool -= 0.033f;
                        }
                        else
                        {
                            AddToScore = 50;
                            Pool.ProgressPool -= 0.037f;
                        }
                    }
                    else
                    {
                        Pool.ProgressPool -= 0.05f;
                    }
                } else if (CurrentLevel == 2)
                {
                    ScoreManaging.AddScore(AddToScore);
                    if (EndGame.CommenceTheEndGaame == false)
                    {
                        if (CitiesAlive.CitiesAlive > 8)
                        {
                            AddToScore = 150;
                            Pool.ProgressPool -= 0.045f;
                        }
                        else
                        {
                            AddToScore = 100;
                            Pool.ProgressPool -= 0.065f;
                        }
                    }
                    else
                    {
                        Pool.ProgressPool -= 0.075f;
                    }
                } else if (CurrentLevel == 3)
                {
                    ScoreManaging.AddScore(AddToScore);
                    if (EndGame.CommenceTheEndGaame == false)
                    {
                        if (CitiesAlive.CitiesAlive > 8)
                        {
                            AddToScore = 300;
                            Pool.ProgressPool -= 0.1f;
                        }
                        else
                        {
                            AddToScore = 200;
                            Pool.ProgressPool -= 0.114f;
                        }
                    }
                    else
                    {
                        Pool.ProgressPool -= 0.1f;
                    }
                }
            }
            citySoundPlayer.clip = citySoundClips[1];
            citySoundPlayer.Play();
            SwitchState();
            CitiesAlive.CitiesAlive--;
            return;
        }
        UpgradeCity();                                                                                                                  //callar funktionen upgradecity
    }

    private void DeadUpdate()
    {
        RespawnTimer -= 1 * Time.deltaTime;                                                                                                //timer går ner med en sekund     
        if (RespawnTimer <= 0)                                                                                                             //om timern är mindre eller likamed 0 
        {
            CurrentHealth = MaxHealth;                                                                                                     //health går till max
            RespawnTimer = MaxRespawnTimer /* * CurrentLevel*/;                                                                            //respawntimer resetas
            CurrentLevel = 1;                                                                                                              //level sätts till 1
            UpgradeTimer = MaxUpgradeTimer;
            SwitchState();                                                                                                                 //byter state till alive
            CitiesAlive.CitiesAlive++;
            ChooseType();                                                                                                                  //byter stad så att den är stark mot det den ska vara
            CityCollider.radius = 0.55f;
            AssistantBehaviour.Respawned = true;
        }
    }

    private void UpgradeCity()
    {
        UpgradeTimer -= 1* Time.deltaTime;                                                                                                 //timer går ner med en sekund     
        if (UpgradeTimer <= 0 && CurrentLevel < 3)                                                                                         //om timern är mindre eller likamed 0 och leveln inte är högre än 3
        {
            CurrentLevel++;                                                                                                                //öka lvln med 1
            float lostHealth = MaxHealth - CurrentHealth;
            CurrentHealth = (MaxHealth * CurrentLevel) - lostHealth;                                                                       //öka health valuet
            UpgradeTimer = (MaxUpgradeTimer * CurrentLevel);                                                                               //sätt en ny uppgradetimer
            if(CurrentLevel == 2)
            {
                CityAnimator.SetBool("Upgrade1", true);
                CityCollider.radius = 0.9f;
            }
            else if(CurrentLevel == 3)
            {
                CityAnimator.SetBool("Upgrade2", true);
                CityCollider.radius = 1.0f;
            }
        }
    }

    private CityType ChooseType()   
    {
        CityType TmpType = CityType.NORMAL;                                                                                                //sätter en temporär variabel till normal
        if(LastAttackedBy == null)
        {
            return TmpType;                                                                                                                //om den inte har blivit attackerad går tillbaka med citytype.normal
        }
        else if(LastAttackedBy == DamageKinds[0])                                                                                          //om det va en earthquake
        {
            TmpType = CityType.EARTHQUAKE;                                                                                                 //sätt till rätta variabler till type och dmgtype
            dmgDealer.damageType = DamageKinds[4];
        }
        else if (LastAttackedBy == DamageKinds[1])                                                                                        //om det va lightning
        {
            TmpType = CityType.LIGHTNING;                                                                                                 //sätt till rätta variabler till type och dmgtype
            dmgDealer.damageType = DamageKinds[5];
        }
        else if (LastAttackedBy == DamageKinds[2])                                                                                        //om det va en tornado
        {
            TmpType = CityType.TORNADO;                                                                                                   //sätt till rätta variabler till type och dmgtype
            dmgDealer.damageType = DamageKinds[6];
        }
        else if (LastAttackedBy == DamageKinds[3])                                                                                        //om det va en våg
        {
            TmpType = CityType.WATER;                                                                                                     //sätt till rätta variabler till type och dmgtype
            dmgDealer.damageType = DamageKinds[7];
        }
        return TmpType;                                                                                                                  //skicka tillbakla temporär variabeln
    }

    public void SwitchState()
    {
        if (Alive.gameObject.activeSelf == true)                                                                                         //om cityalive är aktiv i den nuvarande scenen
        {
            Alive.gameObject.SetActive(false);                                                                                           //gör ena enabled och den andra inte
            Dead.gameObject.SetActive(true);
            transform.GetChild(2).localScale = new Vector3(0.7f, 0.7f);
            transform.GetChild(2).GetComponent<Animator>().SetBool("Dead", true);
            CityAnimator.SetBool("Upgrade1", false);
            CityAnimator.SetBool("Upgrade1", false);
            var clone = Instantiate(FloatingScore, Camera.main.WorldToScreenPoint(transform.position), Quaternion.identity, GameObject.Find("GameUI").transform);
            clone.GetComponent<floatingScore>().setParent(transform);
            clone.GetComponent<Text>().text = "+" + AddToScore.ToString();
            if (AssistantBehaviour.Tutorial == false)
                SetRespawnTime();
        }
        else
        {
            transform.GetChild(2).GetComponent<Animator>().SetBool("Dead", false);
            transform.GetChild(2).localScale = new Vector3(0.27f, 0.27f);
            Alive.gameObject.SetActive(true);                                                                                           //gör ena enabled och den andra inte
            Dead.gameObject.SetActive(false);
            CurrentType = ChooseType();                                                                                                 //skaffa en ny citytype nu när den spawnar
            citySoundPlayer.clip = citySoundClips[0];
            Instantiate(rippleEffect, transform);
            citySoundPlayer.Play();
            respawnSet = false;
        }
    }
    
    public void SetSpawnIndex(int index)
    {
        SpawnIndex = index;                                                                                                             //sätter en int för i vilken zone den spawnade i
    }

    public int GetSpawnIndex()
    {
        return SpawnIndex;                                                                                                              //ger vilket spawnindex den hade
    }

    public void DealDamage(int DamageAmount, DamageType AttackType)
    {
        CurrentHealth -= DamageAmount;                                                                                                  //minskar health med så mycket dmg attacken hade
        LastAttackedBy = AttackType;                                                                                                    //va den sist blev attackerad av
        Instantiate(dirtSplatter, transform);
    }

    public bool IsAlive()
    {
        bool tmp;
        if(Alive.gameObject.activeSelf == true)
        {
            tmp = true;
        }
        else
        {
            tmp = false;
        }
        return tmp;
    }

    public void SetRespawnTime()
    {
        if(Dead.gameObject.activeSelf == true && respawnSet == false)
        {
            RespawnTimer = CityMaster.getRespawnTime();
            UpgradeTimer = CityMaster.getUpgradeTimer();
            respawnSet = true;
        } 
    }

    private void SetTickRate()
    {
        switch(CityMaster.currentWave)
        {
            case 1:
                TickTime = 1.3f;
                break;
            case 2:
                TickTime = 1.25f;
                break;
            case 3:
                TickTime = 1.2f;
                break;
            case 4:
                TickTime = 1.15f;
                break;
            case 5:
                TickTime = 1.1f;
                break;
        }
    }
}