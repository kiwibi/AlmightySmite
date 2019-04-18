using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityBehaviour : MonoBehaviour
{
    enum CityType
    {
        NORMAL,
        EARTHQUAKE,
        WATER,
        LIGHTNING,
        TORNADO
    }
    public DamageType[] DamageKinds;
    
    private int CurrentLevel;
    public float MaxHealth;
    public float MaxUpgradeTimer;
    private float UpgradeTimer;
    public float MaxRespawnTimer;
    private float RespawnTimer;
    private float CurrentHealth;
    private CityType CurrentType;
    public int SpawnIndex;
    private DamageType LastAttackedBy;

    private Transform Alive;
    private Transform Dead;
    // Start is called before the first frame update
    void Start()
    {
        CurrentLevel = 1;
        UpgradeTimer = MaxUpgradeTimer;
        CurrentHealth = MaxHealth;

        Alive = transform.GetChild(0);
        Dead = transform.GetChild(1);

        Dead.gameObject.SetActive(false);
        ChooseType();
    }

    // Update is called once per frame
    void Update()
    {
       if(Alive.gameObject.activeSelf == true)
       {
            AliveUpdate();
       }
       else
       {
            DeadUpdate();
       }
    }

    private void AliveUpdate()
    {  
        if(CurrentHealth <= 0)
        {
            SwitchState();
        }
        UpgradeCity();
    }

    private void DeadUpdate()
    {
        RespawnTimer -= Time.deltaTime;
        if (RespawnTimer <= 0)
        {
            CurrentHealth = MaxHealth;
            RespawnTimer = MaxUpgradeTimer /* * CurrentLevel*/;
            CurrentLevel = 1;
            SwitchState();
        }
    }

    private void UpgradeCity()
    {
        UpgradeTimer -= Time.deltaTime;
        if(UpgradeTimer <= 0 && CurrentLevel < 3)
        {
            CurrentLevel++;
            CurrentHealth = MaxHealth * CurrentLevel;
            UpgradeTimer = (MaxUpgradeTimer * CurrentLevel);
        }
    }

    private CityType ChooseType()
    {
        CityType TmpType = CityType.NORMAL;
        if(LastAttackedBy == null)
        {
            return TmpType;
        }
        else if(LastAttackedBy == DamageKinds[0])
        {
            TmpType = CityType.EARTHQUAKE;
            //dmgdealer.damagetype = DamageKinds[1]
        }
        else if (LastAttackedBy == DamageKinds[1])
        {
            TmpType = CityType.LIGHTNING;
            //dmgdealer.damagetype = DamageKinds[1]
        }
        else if (LastAttackedBy == DamageKinds[2])
        {
            TmpType = CityType.TORNADO;
            //dmgdealer.damagetype = DamageKinds[1]
        }
        else if (LastAttackedBy == DamageKinds[3])
        {
            TmpType = CityType.WATER;
            //dmgdealer.damagetype = DamageKinds[1]
        }

        return TmpType;
    }

    private void SwitchState()
    {
        if (Alive.gameObject.activeSelf == true)
        {
            Alive.gameObject.SetActive(false);
            Dead.gameObject.SetActive(true);
        }
        else
        {
            Alive.gameObject.SetActive(true);
            Dead.gameObject.SetActive(false);
            CurrentType = ChooseType();
        }
    }
    
    public void SetSpawnIndex(int index)
    {
        SpawnIndex = index;
    }

    public int GetSpawnIndex()
    {
        return SpawnIndex;
    }

    public void DealDamage(int DamageAmount, DamageType AttackType)
    {
        CurrentHealth -= DamageAmount;
        LastAttackedBy = AttackType;
    }
}