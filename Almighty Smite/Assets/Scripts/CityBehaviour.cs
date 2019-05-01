using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public DamageType[] DamageKinds;                                                                                                    //en array för att hålla alla olika sorters dmg den ska känna till
    public ParticleSystem[] Smokes;                                                                                                     //en arrat för att hålla alla sorters olika smoke så den vet vad som ska spawnas

    private int CurrentLevel;                                                                                                           //vilken lvl staden är. bestämmer vilka värden som används
    public float MaxHealth;                                                                                                             //max health för staden
    public float MaxUpgradeTimer;                                                                                                       //hur lång tid ska det ta att uppgradera stadens level
    private float UpgradeTimer;                                                                                                         //vart timer ligger just nu
    public float MaxRespawnTimer;                                                                                                       //hur lång tid ska det ta för att staden ska spawna igen
    private float RespawnTimer;                                                                                                         //vart tiden ligger just nu
    private float CurrentHealth;                                                                                                        //vart healthen ligger just nu
    private CityType CurrentType;                                                                                                       //vilken sorts stad den är just nu bestämt av enumens i början av scriptet
    public int SpawnIndex;                                                                                                              //i vilken collider den spawnade i
    private DamageType LastAttackedBy;                                                                                                  //vilken sorts damage attackerade staden sist
    private DamageDealer dmgDealer;                                                                                                     //scriptet för damagedealer

    private Transform Alive;                                                                                                            //child objektet CityAlive
    private Transform Dead;                                                                                                             //child objektet CityDead
    
    void Start()
    {
        CurrentLevel = 1;                                                                                                               //staden börjar på lvl 1
        UpgradeTimer = MaxUpgradeTimer;                                                                                                 //sätter alla timers + health till sina max values
        CurrentHealth = MaxHealth;
        RespawnTimer = MaxRespawnTimer;

        Alive = transform.GetChild(0);                                                                                                  //hämtar komponenterna från de två child objekten
        Dead = transform.GetChild(1);

        Dead.gameObject.SetActive(false);                                                                                               //sätter den döda till false
        dmgDealer = GetComponent<DamageDealer>();                                                                                       //hämtar komponenterna från damagedealer scriptet
        ChooseType();                                                                                                                   //callar funktionen ChooseType
    }

    
    void Update()
    {
       if(Alive.gameObject.activeSelf == true)                                                                                          //om objektet Alive är aktivt i scenen gör funktionen AliveUpdate
       {
            AliveUpdate();

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
            SwitchState();
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
            SwitchState();                                                                                                                 //byter state till alive
            ChooseType();                                                                                                                  //byter stad så att den är stark mot det den ska vara
        }
    }

    private void UpgradeCity()
    {
        UpgradeTimer -= 1* Time.deltaTime;                                                                                                 //timer går ner med en sekund     
        if (UpgradeTimer <= 0 && CurrentLevel < 3)                                                                                         //om timern är mindre eller likamed 0 och leveln inte är högre än 3
        {
            CurrentLevel++;                                                                                                                //öka lvln med 1
            CurrentHealth = MaxHealth * CurrentLevel;                                                                                      //öka health valuet
            UpgradeTimer = (MaxUpgradeTimer * CurrentLevel);                                                                               //sätt en ny uppgradetimer
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
        var smoke = GetComponentInChildren<ParticleSystem>();                                                                             //försöker hitta om det redan finns rök effekter
        Destroy(smoke);                                                                                                                   //förstör den 
        ParticleSystem clone;                                                                                                             //skapa ett nytt particle system
        switch (CurrentType)                                                                                                              //går in i caset söm CityType är = med
        {
            case CityType.NORMAL:
                break;
            case CityType.EARTHQUAKE:
                clone = Instantiate(Smokes[0], transform);                                                                                //spawna röken
                break;
            case CityType.WATER:
                Instantiate(Smokes[3], transform);                                                                                        //spawna röken
                break;
            case CityType.LIGHTNING:
                Instantiate(Smokes[2], transform);                                                                                       //spawna röken
                break;
            case CityType.TORNADO:
                Instantiate(Smokes[1], transform);                                                                                       //spawna röken
                break;
        }

        return TmpType;                                                                                                                  //skicka tillbakla temporär variabeln
    }

    private void SwitchState()
    {
        if (Alive.gameObject.activeSelf == true)                                                                                         //om cityalive är aktiv i den nuvarande scenen
        {
            Alive.gameObject.SetActive(false);                                                                                           //gör ena enabled och den andra inte
            Dead.gameObject.SetActive(true);
        }
        else
        {
            Alive.gameObject.SetActive(true);                                                                                           //gör ena enabled och den andra inte
            Dead.gameObject.SetActive(false);
            CurrentType = ChooseType();                                                                                                 //skaffa en ny citytype nu när den spawnar
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
    }
}