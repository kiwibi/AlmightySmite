using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssistantBehaviour : MonoBehaviour
{
    private bool CurrentlyActive;
    private SpriteRenderer AssistantSprite;
    public Sprite[] AssitantLooks;

    void Start()
    {
        AssistantSprite = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
