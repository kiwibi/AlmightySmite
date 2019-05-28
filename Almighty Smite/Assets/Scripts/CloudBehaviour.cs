using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudBehaviour : MonoBehaviour
{
    public Sprite[] CloudSprites;
    private SpriteRenderer[] CloudRenderer;
    private Vector2 Movement;
    private float Timer = 0.0f;
    private float ChangeDirection;
    Color CloudColor;

    void Start()
    {
        CloudRenderer = GetComponentsInChildren<SpriteRenderer>();
        int index = Random.Range(0, CloudSprites.Length - 1);
        foreach (var renderer in CloudRenderer)
        {
            renderer.sprite = CloudSprites[index];
            Color CloudColor = renderer.color;
            CloudColor.a = 0.0f;
            renderer.color = CloudColor;
        }
       
        //Movement = Random.insideUnitCircle.normalized;
        Movement = WindBehaviour.GetWindMovement();
        ChangeDirection = 0.5f;
    }

    void Update()
    {
        transform.Translate(Movement * Time.deltaTime);
        Timer += Time.deltaTime;
        if (Timer < 4.0f)
        {
            StartCoroutine("FadeIn");
        }
        if (Timer > 6.0f)
        {
            StartCoroutine("FadeOut");
            //Timer = 0.0f;
        }
        TurnHandling();
    }

    IEnumerator FadeIn()
    {
        for (float f = 0f; f <= 1.0f; f += 0.01f)
        {
            foreach (var renderer in CloudRenderer)
            {
                Color CloudColor = renderer.color;
                CloudColor.a = f;
                renderer.color = CloudColor;
                yield return null;
            }
        }
    }

    IEnumerator FadeOut()
    {
        for (float f = 1.0f; f > 0f; f -= 0.01f)
        {
            foreach (var renderer in CloudRenderer)
            {
                Color CloudColor = renderer.color;
                CloudColor.a = f;
                renderer.color = CloudColor;
                yield return null;
            }
        }
        Destroy(gameObject);
    }

    private void TurnHandling()
    {
        ChangeDirection -= Time.deltaTime;                                                 //timern ökar lite varje frame
        if (ChangeDirection < 0)                                                           //om timern har gått förbi noll
        {
            Movement = Vector3.Lerp(Movement, WindBehaviour.GetWindMovement(), Mathf.SmoothStep(0f, 1f, .3f));                   //börjar röra från startpunkten(Direction) till den nya vindriktningen(WindBehaviour.GetWindMovement)
                                                                                                                                    //mathf.smoothstep är ungefär likadan men den går snabbare i början och långsamare i slutet
            ChangeDirection = 1f;                                                                                                    //sätter igång timern igen så att den inte kör klart rotationen
        }
    }
}
