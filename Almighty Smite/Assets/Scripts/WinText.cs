using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinText : MonoBehaviour
{
    public Image WinTxt;
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
            Saturation = WinTxt.color;
            Saturation.a = f;
            WinTxt.color = Saturation;
            yield return null;
        }
    }
}
