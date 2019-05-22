using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkFire : MonoBehaviour
{
    Vector3 sizeDecrease;
    float timer;
    // Start is called before the first frame update
    void Start()
    {
        timer = 3;
        sizeDecrease = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0)
        {
            if (transform.localScale.x > 0)
                sizeDecrease.Set(transform.localScale.x - 0.15f, transform.localScale.y - 0.15f, transform.localScale.z);
            else
                transform.localScale = Vector3.zero;
            timer = 1;
        }
        transform.localScale = sizeDecrease;
    }
}
