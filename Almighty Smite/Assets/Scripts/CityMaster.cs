using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityMaster : MonoBehaviour
{
    private float SpawnTimer;
    private Transform[] Cities;
    private int Index = 0;
    private static readonly int AmmountOfCities = 12;

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

        SpawnTimer = Random.Range(1.0f, 4.0f);

        for (int i = 0; i < AmmountOfCities; i++)
        {
            Cities[i].gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (Index < AmmountOfCities)
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
        Cities[Index].gameObject.SetActive(true);
        SpawnTimer = Random.Range(1.0f, 4.0f);
        Index++;
    }
}
