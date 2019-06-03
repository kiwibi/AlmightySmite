using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityMaster : MonoBehaviour
{
    public static CityMaster instance;

    private float SpawnTimer;
    private Transform[] Cities;
    //private Transform[] BossCities;
    private int Index = 0;
    public int CitiesAlive = 0;
    private static readonly int AmmountOfCities = 25;
    //private static readonly int AmmountOfBossCities = 3;
    private ProgressbarBehaviour Pool;
    public static int currentWave;
    float respawnTimer;
    float anotherTimer;
    //private bool SecondWave = false;
    //private bool ThirdWave = false;
    //private bool BossWave = false;
    //int BossSelection;
    //int BossSeed;

    void Start()
    {
        
        instance = this;
        currentWave = 1;
        Pool = GameObject.Find("GameUI").GetComponent<ProgressbarBehaviour>();
        //BossCities = new Transform[AmmountOfBossCities];
        //BossCities[0] = transform.Find("BossCity");
        //BossCities[1] = transform.Find("BossCity (1)");
        //BossCities[2] = transform.Find("BossCity (2)");

        {
            Cities = new Transform[AmmountOfCities];
            Cities[0] = transform.Find("CityParent");
            Cities[1] = transform.Find("CityParent (1)");
            Cities[2] = transform.Find("CityParent (2)");
            Cities[3] = transform.Find("CityParent (3)");
            Cities[4] = transform.Find("CityParent (4)");
            Cities[5] = transform.Find("CityParent (5)");
            Cities[6] = transform.Find("CityParent (6)");
            Cities[7] = transform.Find("CityParent (7)");
            Cities[8] = transform.Find("CityParent (8)");
            Cities[9] = transform.Find("CityParent (9)");
            Cities[10] = transform.Find("CityParent (10)");
            Cities[11] = transform.Find("CityParent (11)");
            Cities[12] = transform.Find("CityParent (12)");
            Cities[13] = transform.Find("CityParent (13)");
            Cities[14] = transform.Find("CityParent (14)");
            Cities[15] = transform.Find("CityParent (15)");
            Cities[16] = transform.Find("CityParent (16)");
            Cities[17] = transform.Find("CityParent (17)");
            Cities[18] = transform.Find("CityParent (18)");
            Cities[19] = transform.Find("CityParent (19)");
            Cities[20] = transform.Find("CityParent (20)");
            Cities[21] = transform.Find("CityParent (21)");
            Cities[22] = transform.Find("CityParent (22)");
            Cities[23] = transform.Find("CityParent (23)");
            Cities[24] = transform.Find("CityParent (24)");
        }
        SpawnTimer = 0;
        respawnTimer = 30;
        for (int i = 0; i < AmmountOfCities; i++)
        {
            Cities[i].gameObject.SetActive(false);
        }
        //for (int i = 0; i < AmmountOfBossCities; i++)
        //{
        //    BossCities[i].gameObject.SetActive(false);
        //}

        //BossSeed = Random.Range(1, 100);
        //if (BossSeed < 33)
        //{
        //    BossSelection = 0;
        //} else if (BossSeed > 33 && BossSeed < 66)
        //{
        //    BossSelection = 1;
        //} else
        //{
        //    BossSelection = 2;
        //}
        //Debug.Log(BossSeed);
        //Debug.Log(BossSelection);
    }

    void Update()
    {
        SpawnTimer -= Time.deltaTime;

        if (AssistantBehaviour.Tutorial == true)
        {
            if (Index < 4)
            {
                SpawnCity();
                //setRespawnTime(respawnTimer);
            }
        }
        else
        {
            switch(currentWave)
            {
                case 1:
                    respawnTimer = 25;
                    if (Index < 8)
                    {
                        SpawnCity();
                        //setRespawnTime(respawnTimer);
                    }
                    if(CitiesAlive < 3)
                    {
                        currentWave++;
                    }
                    break;
                case 2:
                    if (Index < 12)
                    {
                        SpawnCity();
                        //setRespawnTime(respawnTimer);
                    }
                    if (CitiesAlive < 6)
                    {
                        currentWave++;
                    }
                    break;
                case 3:
                    respawnTimer = 20;
                    if (Index < 16)
                    {
                        SpawnCity();
                        //setRespawnTime(respawnTimer);
                    }
                    if (CitiesAlive < 8)
                    {
                        currentWave++;
                    }
                    break;
                case 4:
                    if (Index < 20)
                    {
                        SpawnCity();
                        //setRespawnTime(respawnTimer);
                    }
                    if (CitiesAlive < 10)
                    {
                        currentWave++;
                    }
                    break;
                case 5:
                    if (Index < 25)
                    {
                        SpawnCity();
                        //setRespawnTime(respawnTimer);
                    }
                    if (CitiesAlive < 12)
                    {
                        currentWave++;
                    }
                    break;
                case 6:
                    if (CitiesAlive < 14 && anotherTimer < Time.time)
                    {
                        anotherTimer = Time.time + 3;
                        if(respawnTimer < 5)
                            respawnTimer -= 0.5f;
                        //setRespawnTime(respawnTimer);
                    }
                    break;
            }
            //if (Index == 8 && CitiesAlive < 4)
            //{
            //    SecondWave = true;
            //}
            //if (Index == 16 && CitiesAlive < 12)
            //{
            //    ThirdWave = true;
            //}
            //if (ThirdWave == true && Pool.ProgressBar.fillAmount <= 0.1f)
            //{
            //    BossWave = true;
            //}
            //if (BossWave == true)
            //{
            //    for (int i = 0; i < AmmountOfCities; i++)
            //    {
            //        Cities[i].gameObject.SetActive(false);
            //    }
            //    BossCities[BossSelection].gameObject.SetActive(true);
            //}
        }
    }

    private void SpawnCity()
    {
        Cities[Index].gameObject.SetActive(true);
        CitiesAlive++;
        Index++;
    }

    //private void setRespawnTime(float time)
    //{
    //    for(int i = 0; i < Index; i++)
    //    {
    //        if(Cities[i].gameObject.activeSelf == true)
    //            Cities[i].GetComponent<CityBehaviour>().SetRespawnTime(time);
    //    }
    //}

    public static float getRespawnTime()
    {

        return instance.respawnTimer;
    }

    public static void TutorialRespawn()
    {
        instance.respawnTimer = 0;
        for (int i = 0; i < instance.Index; i++)
        { 
               instance.Cities[i].GetComponent<CityBehaviour>().SetRespawnTime();
        }
    }
}
