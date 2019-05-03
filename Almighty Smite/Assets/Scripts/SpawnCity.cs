using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCity : MonoBehaviour
{
    public GameObject City;                                                                                                                                         //Objekt som ska spawnas
    public float MaxTimer;                                                                                                                                          //hur lång som timern ska vara
    public float SpawnBoarder;                                                                                                                                      //hur långt från kanten ska saker spawnas
    private float Timer;                                                                                                                                            //timer för spawning
    private bool Spawnable;                                                                                                                                         //kan någonting spawnas                                                                                                                                      
    private Vector3 SpawnLocation;                                                                                                                                  //plats som objektet spawnas
    GameObject World;                                                                                                                                               //objekt som håller alla zoner saker kan spawnas
    private List<GameObject> Cities;                                                                                                                                //lista för att hålla koll på allting som spawnats
    private bool CanSpawn;                                                                                                                                          //kollar ifall någonting kan spawnas på en specifik lokation
    Collider2D[] TotalColliderAmount;                                                                                                                               //mängden zoner som saker kan spawnas i

    void Start()
    {
        Timer = MaxTimer;                                                                                                                                           //timer på max
        Spawnable = false;                                                                                                                                          //inget spawnas direkt
        World = GameObject.FindGameObjectWithTag("World");                                                                                                          //hitta objektet med alla zoner
        Cities = new List<GameObject>();                                                                                                                            //ger en tom lista

        TotalColliderAmount = World.GetComponentsInChildren<Collider2D>();                                                                                          //sätter hur många zoner det va i världen
    }

    // Update is called once per frame
    void Update()
    {
        SpawnCountDown();                                                                                                                                           //callar fúnktionen SpawnCountdown

        if (Spawnable == true)                                                                                                                                      //om nåt kan spawnas
            SpawnHouses();                                                                                                                                          //spawna ett hus/city
    }

    private void SpawnCountDown()
    {
        Timer -= 1 * Time.deltaTime;                                                                                                                                //timer går ned 1 sekund
        if (Timer <= 0)                                                                                                                                             //om den är mindre eller likamed 0
        {
            Spawnable = true;                                                                                                                                       //det kan spawnas
            Timer = MaxTimer;                                                                                                                                       // timer sätts till max
        }
    }

    private void SpawnHouses()
    {
        ChooseLocation();                                                                                                                                           //hittar en ledig plats i världen
        Spawnable = false;                                                                                                                                          //inget ska spawnas direkt igen
    }

    private void ChooseLocation()
    {
        int index = Random.Range(0, TotalColliderAmount.Length-1);                                                                                                  //väljer en random zone mellan 0 och max mängden colliders
        Collider2D currentCol = TotalColliderAmount[index];                                                                                                         //hämtar all info om den nuvarande zonen
        SpawnLocation.Set(currentCol.bounds.center.x , Random.Range(currentCol.bounds.min.y + SpawnBoarder, currentCol.bounds.max.y - SpawnBoarder), 0);
                                                                                                                                                                    //sätter en preliminär lokation att spawna mellan min & max i zonen
        
        if(Cities.Count == 0)                                                                                                                                       //om det är den första staden
        {
            var clone = Instantiate(City, SpawnLocation, Quaternion.identity);                                                                                      //spawna staden
            Cities.Add(clone);                                                                                                                                      //lägg till i listan
            clone.GetComponent<CityBehaviour>().SetSpawnIndex(index);                                                                                               //sätter så att staden vet vilken zone den är i
            return;                                                                                                                                                 //hoppa ur och bryr sig inte om resten av scriptet
        }
        for(int i = 0; i < TotalColliderAmount.Length; i++)                                                                                                         //kolla r alla zoner för lediga platser
        {
            if (IsChunkFree(i))                                                                                                                                     //om zonen är ledig
            {
                CanSpawn = true;                                                                                                                                    //det kan spawnas
                index = i;                                                                                                                                          //sätter index till vart det va ledigt
            }
        }
        if (CanSpawn == true)                                                                                                                                       //om det fanns en ledig zone
        {
            currentCol = TotalColliderAmount[index];                                                                                                                //tar informationen om den zonen
            SpawnLocation.Set(currentCol.bounds.center.x , Random.Range(currentCol.bounds.min.y + SpawnBoarder, currentCol.bounds.max.y - SpawnBoarder), 0);           //sätter en ny lokation i den zonen
            var clone = Instantiate(City, SpawnLocation, Quaternion.identity);                                                                                      //Spawna staden
            Cities.Add(clone);                                                                                                                                      //lägg till i listan
            clone.GetComponent<CityBehaviour>().SetSpawnIndex(index);                                                                                               //sätter så att staden vet vilken zone den är i
            CanSpawn = false;                                                                                                                                       //kan inte spawna
        }
    }

    private bool IsChunkFree(int Index)
    {
        bool FreeChunk = false;                                                                                                                                    //temporär boll sätts till false
        foreach(var City in Cities)                                                                                                                                //kollar alla index där det finns städer redan
        {
            int tmp = City.GetComponent<CityBehaviour>().GetSpawnIndex();                                                                                          //temporär variabel som håller spawnindexet
            if (Index == tmp)                                                                                                                                      //om staden försöker spawna i samma zone
            {
                FreeChunk = false;                                                                                                                                 //inte ledigt
                break;                                                                                                                                             //hoppa ur foreach { }
            }
            else                                                                                                                                                   //annars
            {
                FreeChunk = true;                                                                                                                                  //det är en ledig zone
            }
        }
        return FreeChunk;                                                                                                                                          //säger om det va en ledig zone
    }
}
