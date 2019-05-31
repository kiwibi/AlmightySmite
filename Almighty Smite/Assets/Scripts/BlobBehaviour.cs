using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlobBehaviour : MonoBehaviour
{
    private ProgressbarBehaviour WorldProgress;
    private bool desaturated = false;
    public Image Blob;
    Color BlobSaturation;

    void Start()
    {
        WorldProgress = GameObject.Find("GameUI").GetComponent<ProgressbarBehaviour>();
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
        if (WorldProgress.ProgressPool < 0.45)
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
            Color BlobSaturation = Blob.color;
            BlobSaturation.a = f;
            Blob.color = BlobSaturation;
            yield return null;
        }
    }

    IEnumerator FadeOut()
    {
        for (float f = 1.0f; f >= 0; f -= 0.005f)
        {
            Color BlobSaturation = Blob.color;
            BlobSaturation.a = f;
            Blob.color = BlobSaturation;
            yield return null;
        }
    }
}
