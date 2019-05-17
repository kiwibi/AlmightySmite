using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudBehaviour : MonoBehaviour
{
    public Sprite[] CloudSprites;
    private SpriteRenderer CloudRenderer;
    private Vector3 Movement;
    // Start is called before the first frame update
    void Start()
    {
        CloudRenderer = GetComponent<SpriteRenderer>();
        CloudRenderer.sprite = CloudSprites[Random.Range(0, CloudSprites.Length - 1)];
        Movement = Random.insideUnitCircle.normalized;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Movement * Time.deltaTime;
    }
}
