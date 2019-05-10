using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMaster : MonoBehaviour
{
    Transform MiniMap;

    void Awake()
    {
        MiniMap = transform.Find("Minimapmap");
        MiniMap.gameObject.SetActive(true);
    }
}
