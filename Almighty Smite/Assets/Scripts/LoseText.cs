using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoseText : MonoBehaviour
{
    public Image LoseTxt;
    Color Saturation;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    IEnumerator FadeIn()
    {
        for (float f = 0f; f <= 1.0; f += 0.01f)
        {
            Saturation = LoseTxt.color;
            Saturation.a = f;
            LoseTxt.color = Saturation;
            yield return null;
        }
    }
}
