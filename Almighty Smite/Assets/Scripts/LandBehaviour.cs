using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandBehaviour : MonoBehaviour
{
    public Sprite[] WorldSprite;
    public SpriteRenderer[] WorldRenderer;
    Color WorldSaturation;
    private ProgressbarBehaviour WorldProgress;
    private bool desaturated = false;

    void Start()
    {
        WorldProgress = GameObject.Find("GameUI").GetComponent<ProgressbarBehaviour>();
        int index = Random.Range(0, WorldSprite.Length - 1);
        foreach (var renderer in WorldRenderer)
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
            foreach (var renderer in WorldRenderer)
            {
                Color WorldSaturation = renderer.material.color;
                WorldSaturation.a = f;
                renderer.material.color = WorldSaturation;
                yield return null;
            }
        }
    }

    IEnumerator FadeOut()
    {
        for (float f = 1.0f; f >= 0; f -= 0.005f)
        {
            foreach (var renderer in WorldRenderer)
            {
                Color WorldSaturation = renderer.material.color;
                WorldSaturation.a = f;
                renderer.material.color = WorldSaturation;
                yield return null;
            }
        }
    }
}
