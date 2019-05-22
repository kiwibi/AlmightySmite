using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PostLightningSpawn : MonoBehaviour
{
    public GameObject postLightning;
    private GameObject clone;

    private void Start()
    {
        clone = Instantiate(postLightning, transform.position, Quaternion.identity);
    }

    private void OnDestroy()
    {
        clone.transform.GetChild(0).GetComponent<Animator>().SetBool("Disperse", true);
    }
}
