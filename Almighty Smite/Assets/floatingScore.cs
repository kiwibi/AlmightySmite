using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floatingScore : MonoBehaviour
{
    private Transform fakeParent;
    private Vector3 currentPos;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Camera.main.WorldToScreenPoint(currentPos);
        currentPos.y += 2.5f * Time.deltaTime;
    }

    public void setParent(Transform FP)
    {
        currentPos = FP.position;
    }
}
