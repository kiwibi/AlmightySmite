using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobMaster : MonoBehaviour
{
    private Transform[] Blobs;
    private ProgressbarBehaviour WorldProgress;
    private bool desaturated = false;
    public SpriteRenderer[] BlobRenderer;
    Color BlobSaturation;

    void Start()
    {
        WorldProgress = GameObject.Find("GameUI").GetComponent<ProgressbarBehaviour>();
        Blobs = new Transform[7];
        Blobs[0] = transform.Find("Blob");
        Blobs[1] = transform.Find("Blob (1)");
        Blobs[2] = transform.Find("Blob (2)");
        Blobs[3] = transform.Find("Blob (3)");
        Blobs[4] = transform.Find("Blob (4)");
        Blobs[5] = transform.Find("Blob (5)");
        Blobs[6] = transform.Find("Blob (6)");

        foreach (var renderer in BlobRenderer)
        {
            Color WorldSaturation = renderer.material.color;
            WorldSaturation.a = 1.0f;
            renderer.material.color = WorldSaturation;
        }
    }

    void Update()
    {
        if (WorldProgress.ProgressPool > 0.6)
        {
            if (desaturated == false)
            {
                StartCoroutine("FadeOut");
                desaturated = true;
            }
        }
        if (WorldProgress.ProgressPool < 0.3)
        {
            if (desaturated == true)
            {
                StartCoroutine("FadeIn");
                desaturated = false;
            }
        }
    }

    IEnumerator FadeIn()
    {
        for (float f = 0f; f <= 1.0; f += 0.01f)
        {
            foreach (var renderer in BlobRenderer)
            {
                Color BlobSaturation = renderer.material.color;
                BlobSaturation.a = f;
                renderer.material.color = BlobSaturation;
                yield return null;
            }
        }
    }

    IEnumerator FadeOut()
    {
        for (float f = 1.0f; f >= 0; f -= 0.005f)
        {
            foreach (var renderer in BlobRenderer)
            {
                Color BlobSaturation = renderer.material.color;
                BlobSaturation.a = f;
                renderer.material.color = BlobSaturation;
                yield return null;
            }
        }
    }
}
