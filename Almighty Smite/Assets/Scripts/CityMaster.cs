using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityMaster : MonoBehaviour
{
    private float SpawnTimer;
    private Transform[] Cities;
    private int Index = 0;
    public int CitiesAlive = 0;
    private static readonly int AmmountOfCities = 25;
    private bool Wave01 = false;
    private bool Wave02 = false;

    void Start()
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

        SpawnTimer = Random.Range(1.0f, 4.0f);
        Wave01 = true;

        for (int i = 0; i < AmmountOfCities; i++)
        {
            Cities[i].gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (CitiesAlive < 6 && Index == 12)
        {
            Wave02 = true;
        }
        if (Index == 25)
        {
            Wave02 = false;
        }

        if (Index < 12)
        {
            SpawnTimer -= Time.deltaTime;
            if (SpawnTimer < 0)
            {
                SpawnCity();
            }
        } else { Wave01 = false; Wave02 = true; }

        if (Index < 25 && Wave02 == true)
        {
            SpawnTimer -= Time.deltaTime;
            if (SpawnTimer < 0)
            {
                SpawnCity();
            }
        }
    }

    private void SpawnCity()
    {
        if (Wave01 == true)
        {
            Cities[Index].gameObject.SetActive(true);
            SpawnTimer = Random.Range(1.0f, 4.0f);
            CitiesAlive++;
            Index++;
        }
        if (Wave02 == true)
        {
            Cities[Index].gameObject.SetActive(true);
            SpawnTimer = Random.Range(0.0f, 1.0f);
            CitiesAlive++;
            Index++;
        }
    }
}
