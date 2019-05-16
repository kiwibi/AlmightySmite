using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveBehaviour : MonoBehaviour
{
    private Vector3 CurrentScale;
    private bool up;
    private float RandomSize;
    private float currentX;
    // Start is called before the first frame update
    void Start()
    {
        up = false;
        RandomSize = Random.Range(0.2f, 0.5f);
        CurrentScale = new Vector3(RandomSize, 0, 1);
        transform.localScale = CurrentScale;
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = CurrentScale;
        if(up == false)
            CurrentScale.y += 0.01f * Time.timeScale;
        else
        {
            CurrentScale.y -= 0.01f * Time.timeScale;
            if (CurrentScale.y <= 0.1f)
                Destroy(gameObject);
        }
        if (CurrentScale.y >= RandomSize)
            up = true;
        
       
    }
}
