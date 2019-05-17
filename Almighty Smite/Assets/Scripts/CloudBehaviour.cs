using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudBehaviour : MonoBehaviour
{
    public Sprite[] CloudSprites;
    private SpriteRenderer CloudRenderer;
    private Vector3 Movement;
    private float Timer = 0.0f;
    Color CloudColor;

    void Start()
    {
        CloudRenderer = GetComponent<SpriteRenderer>();
        Color CloudColor = CloudRenderer.material.color;
        CloudRenderer.material.color = CloudColor;
        CloudRenderer.sprite = CloudSprites[Random.Range(0, CloudSprites.Length - 1)];
        //Movement = Random.insideUnitCircle.normalized;
        Movement = new Vector3(Random.Range(-3.0f, 3.0f), 0, 0);
        CloudColor.a = 0.0f;
        CloudRenderer.material.color = CloudColor;
    }

    void Update()
    {
        transform.position += Movement * Time.deltaTime;
        Timer += Time.deltaTime;
        if (Timer < 4.0f)
        {
            StartCoroutine("FadeIn");
        }
        if (Timer > 6.0f)
        {
            StartCoroutine("FadeOut");
            Timer = 0.0f;
        }
    }

    IEnumerator FadeIn()
    {
        for (float f = 0f; f <= 1.0; f += 0.01f)
        {
            Color CloudColor = CloudRenderer.material.color;
            CloudColor.a = f;
            CloudRenderer.material.color = CloudColor;
            yield return null;
        }
    }

    IEnumerator FadeOut()
    {
        for (float f = 1.0f; f >= 0; f -= 0.005f)
        {
            Color CloudColor = CloudRenderer.material.color;
            CloudColor.a = f;
            CloudRenderer.material.color = CloudColor;
            yield return null;
        }
    }
}
