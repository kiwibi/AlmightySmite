using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostLightningSpawn : MonoBehaviour
{
    public GameObject postLightning;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        Instantiate(postLightning, transform.position, Quaternion.identity);
    }
}
