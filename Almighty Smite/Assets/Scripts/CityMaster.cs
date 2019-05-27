using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityMaster : MonoBehaviour
{
    private float SpawnTimer;
    private Transform[] Cities;
    private Transform[] BossCities;
    private int Index = 0;
    public int CitiesAlive = 0;
    private static readonly int AmmountOfCities = 25;
    private static readonly int AmmountOfBossCities = 3;
    private ProgressbarBehaviour Pool;
    private bool SecondWave = false;
    private bool ThirdWave = false;
    private bool BossWave = false;
    int BossSelection;
    int BossSeed;

    void Start()
    {
        Pool = GameObject.Find("GameUI").GetComponent<ProgressbarBehaviour>();
        BossCities = new Transform[AmmountOfBossCities];
        BossCities[0] = transform.Find("BossCity");
        BossCities[1] = transform.Find("BossCity (1)");
        BossCities[2] = transform.Find("BossCity (2)");

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

        SpawnTimer = 0;

        for (int i = 0; i < AmmountOfCities; i++)
        {
            Cities[i].gameObject.SetActive(false);
        }
        for (int i = 0; i < AmmountOfBossCities; i++)
        {
            BossCities[i].gameObject.SetActive(false);
        }

        BossSeed = Random.Range(1, 100);
        if (BossSeed < 33)
        {
            BossSelection = 0;
        } else if (BossSeed > 33 && BossSeed < 66)
        {
            BossSelection = 1;
        } else
        {
            BossSelection = 2;
        }
        //Debug.Log(BossSeed);
        //Debug.Log(BossSelection);
    }

    void Update()
    {
        SpawnTimer -= Time.deltaTime;

        if (AssistantBehaviour.Tutorial == true)
        {
            if (Index < 6)
            {
                SpawnCity();
            }
        }
        else
        {
            if (Index == 10 && CitiesAlive < 6)
            {
                SecondWave = true;
            }
            if (Index == 16 && CitiesAlive < 12)
            {
                ThirdWave = true;
            }
            if (ThirdWave == true && Pool.ProgressBar.fillAmount <= 0.1f)
            {
                BossWave = true;
            }

            if (Index < 10)
            {
                SpawnCity();
            }
            if (SecondWave == true && Index < 16)
            {
                SpawnCity();
            }
            if (ThirdWave == true && Index < 25)
            {
                SpawnCity();
            }
            if (BossWave == true)
            {
                for (int i = 0; i < AmmountOfCities; i++)
                {
                    Cities[i].gameObject.SetActive(false);
                }
                BossCities[BossSelection].gameObject.SetActive(true);
            }
        }
    }

    private void SpawnCity()
    {
        Cities[Index].gameObject.SetActive(true);
        CitiesAlive++;
        Index++;
    }
}
