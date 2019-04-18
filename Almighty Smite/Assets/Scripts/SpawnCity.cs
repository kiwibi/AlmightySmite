using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCity : MonoBehaviour
{
    public GameObject City;
    public float MaxTime;
    public float SpawnBoarder;
    private float Timer;
    private bool Spawnable;
    private int Continents;
    private Vector3 SpawnLocation;
    GameObject[] LandMasses;
    public List<GameObject> Cities;
    private bool CanSpawn;

    void Start()
    {
        Timer = MaxTime;
        Spawnable = false;
        LandMasses = GameObject.FindGameObjectsWithTag("Land");
        Cities = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        SpawnCountDown();

        if (Spawnable == true)
            SpawnHouses();
    }

    private void SpawnCountDown()
    {
        Timer -= Time.deltaTime;
        if (Timer <= 0)
        {
            Spawnable = true;
            Timer = MaxTime;
        }
    }

    private void SpawnHouses()
    {
        ChooseLocation();
        Spawnable = false;
    }

    private void ChooseLocation()
    {
        Continents = Random.Range(0, LandMasses.Length);
        Collider2D[] tmp = LandMasses[Continents].GetComponents<Collider2D>();
        int index = Random.Range(0, tmp.Length-1);
        Collider2D currentCol = tmp[index];
        SpawnLocation.Set(Random.Range(currentCol.bounds.min.x + SpawnBoarder, currentCol.bounds.max.x - SpawnBoarder), Random.Range(currentCol.bounds.min.y + SpawnBoarder, currentCol.bounds.max.y - SpawnBoarder), 0);
        
        if(Cities.Count == 0)
        {
            var clone = Instantiate(City, SpawnLocation, Quaternion.identity);
            Cities.Add(clone);
            clone.GetComponent<CityBehaviour>().SetSpawnIndex(index);
            return;
        }
        for(int i = 0; i < tmp.Length; i++)
        {
            if (IsChunkFree(i))
            {
                CanSpawn = true;
                index = i;
            }
        }
        if (CanSpawn == true)
        {
            currentCol = tmp[index];
            SpawnLocation.Set(Random.Range(currentCol.bounds.min.x + SpawnBoarder, currentCol.bounds.max.x - SpawnBoarder), Random.Range(currentCol.bounds.min.y + SpawnBoarder, currentCol.bounds.max.y - SpawnBoarder), 0);
            var clone = Instantiate(City, SpawnLocation, Quaternion.identity);
            Cities.Add(clone);
            clone.GetComponent<CityBehaviour>().SetSpawnIndex(index);
            CanSpawn = false;
        }
    }

    private bool IsChunkFree(int Index)
    {
        bool FreeChunk = false;
        foreach(var City in Cities)
        {
            if (City == null)
            {
                
                continue;
            }
            int tmp = City.GetComponent<CityBehaviour>().GetSpawnIndex();
            if (Index == tmp)
            {
                FreeChunk = false;
                break;
            }
            else
            {
                FreeChunk = true;
            }
        }
        return FreeChunk;
    }
}
