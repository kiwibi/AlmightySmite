using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    public static bool CommenceTheEndGaame = false;
    private float GameTimer = 0;

    void Update()
    {
        GameTimer += Time.deltaTime;
        Debug.Log(GameTimer);

        if (GameTimer >= 180.0f)
        {
            CommenceTheEndGaame = true;
        }
        else
        {
            CommenceTheEndGaame = false;
        }
    }
}
