using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostLightningSpawn : MonoBehaviour
{
    public GameObject postLightning;
   
    private void OnDestroy()
    {
        Instantiate(postLightning, transform.position, Quaternion.identity);
    }
}
