﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobeRotate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, 3 * Time.deltaTime);                                                                                          //snurra objektet på z axeln
    }
}
