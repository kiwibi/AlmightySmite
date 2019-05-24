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
    private bool SecondWave = false;
    private AssistantBehaviour Tutorial;

    void Start()
    {
        Tutorial = GameObject.Find("Textbox").GetComponent<AssistantBehaviour>();

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
    }

    void Update()
    {
        SpawnTimer -= Time.deltaTime;

        if (Tutorial.Tutorial == true)
        {
            if (Index < 6)
            {
                SpawnCity();
            }
        }
        else
        {
            if (Index < 12)
            {
                SpawnCity();
            }
            if (Index == 12 && CitiesAlive < 6)
            {
                SecondWave = true;
            }
            if (SecondWave == true && Index < 25)
            {
                SpawnCity();
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
