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
    GameObject World;
    private List<GameObject> Cities;
    private bool CanSpawn;
    Collider2D[] TotalColliderAmount;

    void Start()
    {
        Timer = MaxTime;
        Spawnable = false;
        World = GameObject.FindGameObjectWithTag("World");
        Cities = new List<GameObject>();

        TotalColliderAmount = World.GetComponentsInChildren<Collider2D>();
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
        int index = Random.Range(0, TotalColliderAmount.Length-1);
        Collider2D currentCol = TotalColliderAmount[index];
        SpawnLocation.Set(Random.Range(currentCol.bounds.min.x + SpawnBoarder, currentCol.bounds.max.x - SpawnBoarder), Random.Range(currentCol.bounds.min.y + SpawnBoarder, currentCol.bounds.max.y - SpawnBoarder), 0);

        
        if(Cities.Count == 0)
        {
            var clone = Instantiate(City, SpawnLocation, Quaternion.identity);
            Cities.Add(clone);
            clone.GetComponent<CityBehaviour>().SetSpawnIndex(index);
            return;
        }
        for(int i = 0; i < TotalColliderAmount.Length; i++)
        {
            if (IsChunkFree(i))
            {
                CanSpawn = true;
                index = i;
            }
        }
        if (CanSpawn == true)
        {
            currentCol = TotalColliderAmount[index];
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
