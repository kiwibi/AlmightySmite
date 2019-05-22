using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    public GameObject Cloud;
    private float SpawnDelay;
    GameObject CloudArea;
    Collider2D[] TotalColliderAmount;
    private Vector3 SpawnLocation;
    private Transform ParentSpawn;

    void Start()
    {
        ParentSpawn = GameObject.Find("Map Master").transform;
        SpawnDelay = 3;
        CloudArea = GameObject.FindGameObjectWithTag("Ocean");                                                                                                          //hitta objektet med alla zoner
        TotalColliderAmount = CloudArea.GetComponentsInChildren<Collider2D>();                                                                                          //sätter hur många zoner det va i världen
    }

    void Update()
    {
        SpawnDelay -= 1 * Time.timeScale;
        if (SpawnDelay <= 0)
            SpawnCloud();
    }

    private void SpawnCloud()
    {
        int index = Random.Range(0, TotalColliderAmount.Length - 1);                                                                                                  //väljer en random zone mellan 0 och max mängden colliders
        Collider2D currentCol = TotalColliderAmount[index];                                                                                                         //hämtar all info om den nuvarande zonen
        SpawnLocation.Set(Random.Range(currentCol.bounds.min.x, currentCol.bounds.max.y), Random.Range(currentCol.bounds.min.y, currentCol.bounds.max.y), 0);
        var clone = Instantiate(Cloud, SpawnLocation, Quaternion.identity, ParentSpawn);
        SpawnDelay = 120;
    }
}
