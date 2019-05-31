using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndTitleVoid : MonoBehaviour
{
    public Image Void;
    Color Saturation;

    void Start()
    {
        Saturation.a = 0.0f;
    }

    void Update()
    {
        
    }

    IEnumerator FadeIn()
    {
        for (float f = 0f; f <= 1.0; f += 0.01f)
        {
            Saturation = Void.color;
            Saturation.a = f;
            Void.color = Saturation;
            yield return null;
        }
    }
}
