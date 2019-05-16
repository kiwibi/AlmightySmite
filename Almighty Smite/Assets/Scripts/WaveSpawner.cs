using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public GameObject Wave;
    private float SpawnDelay;
    GameObject Ocean;
    Collider2D[] TotalColliderAmount;
    private Vector3 SpawnLocation;

    // Start is called before the first frame update
    void Start()
    {
        SpawnDelay = 1;
        Ocean = GameObject.FindGameObjectWithTag("Ocean");                                                                                                          //hitta objektet med alla zoner
        TotalColliderAmount = Ocean.GetComponentsInChildren<Collider2D>();                                                                                          //sätter hur många zoner det va i världen
    }

    // Update is called once per frame
    void Update()
    {
        SpawnDelay -= 1 * Time.timeScale;
        if (SpawnDelay <= 0)
            SpawnWave();
    }

    private void SpawnWave()
    {
        int index = Random.Range(0, TotalColliderAmount.Length - 1);                                                                                                  //väljer en random zone mellan 0 och max mängden colliders
        Collider2D currentCol = TotalColliderAmount[index];                                                                                                         //hämtar all info om den nuvarande zonen
        SpawnLocation.Set(Random.Range(currentCol.bounds.min.x, currentCol.bounds.max.y), Random.Range(currentCol.bounds.min.y, currentCol.bounds.max.y), 0);
        Instantiate(Wave, SpawnLocation, Quaternion.identity);
        SpawnDelay = 1;
    }
}
