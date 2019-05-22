using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrackBeahaviour : MonoBehaviour
{
    private SpriteRenderer crackRenderer;
    float currentOpacity;
    float timer;
    private Color newColor;
    // Start is called before the first frame update
    void Start()
    {
        crackRenderer = GetComponent<SpriteRenderer>();
        newColor = crackRenderer.color;
        currentOpacity = 1;
        timer = 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            if (currentOpacity > 0)
                currentOpacity -= 0.2f;
            else
            {
                currentOpacity = 0;
                Destroy(gameObject);
            }
            timer = 1;
        }
        newColor.a = currentOpacity;
        crackRenderer.color = newColor;
    }
}
