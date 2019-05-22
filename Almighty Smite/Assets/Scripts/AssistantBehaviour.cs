using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssistantBehaviour : MonoBehaviour
{
    public enum AssistantState
    {
        INTRO,
        ABILITIES,
    }
    private static AssistantBehaviour instance;
    private bool CurrentlyActive;
    private SpriteRenderer AssistantSprite;
    public Sprite[] AssitantLooks;
    public string Intro;
    private bool activated;
    private AssistantState currentState;
    public int TimeBeforeFadeOut;

    void Start()
    {
        instance = this;
        AssistantSprite = GetComponentInChildren<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        

        switch(currentState)
        {
            case AssistantState.INTRO:
                if(activated != true)
                {
                    if(AssistantSprite.color.a != 1)
                        StartCoroutine("FadeIn");
                }
                else
                {
                    if (AssistantSprite.color.a == 0)
                        return;



                }
                break;
            case AssistantState.ABILITIES:
                break;
        }
    }

    public static void ChangeState(AssistantState newState)
    {
        instance.currentState = newState;
        instance.activated = false;
    }

    IEnumerator FadeIn()
    {
        Color tmpColor;
        for (float f = 0f; f <= 1.0; f += 0.01f)
        {
            tmpColor = AssistantSprite.color;
            tmpColor.a = f;
            AssistantSprite.color = tmpColor;
            yield return null;
        }
        tmpColor = AssistantSprite.color;
        tmpColor.a = 1;
        AssistantSprite.color = tmpColor;
        Invoke("TakeAwayTimer", TimeBeforeFadeOut);
        activated = true;
    }

    IEnumerator FadeOut()
    {
        Color tmpColor;
        for (float f = 0f; f <= 1.0; f -= 0.01f)
        {
            tmpColor = AssistantSprite.color;
            tmpColor.a = f;
            AssistantSprite.color = tmpColor;
            yield return null;
        }
        tmpColor = AssistantSprite.color;
        tmpColor.a = 0;
        AssistantSprite.color = tmpColor;
    }

    private void TakeAwayTimer()
    {
        StartCoroutine("FadeOut");
    }
}
