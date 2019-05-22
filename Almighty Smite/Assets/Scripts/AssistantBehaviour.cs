using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AssistantBehaviour : MonoBehaviour
{
    public enum AssistantState
    {
        INTRO,
        EARTHQUAKE,
        TORNADO,
        LIGHTNING,
        STRENGTH,
        SUPERCITY,
    }
    private static AssistantBehaviour instance;
    private bool CurrentlyActive;
    private Image AssistantSprite;
    private Text text;
    public Sprite[] AssitantLooks;
    public string Intro;
    private bool activated;
    private AssistantState currentState;
    public int TimeBeforeFadeOut;

    void Start()
    {
        instance = this;
        AssistantSprite = GetComponentInChildren<Image>();
        text = GetComponentInChildren<Text>();
        instance.currentState = AssistantState.INTRO;
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
                    text.text = Intro;
                }
                else
                {
                    if (AssistantSprite.color.a == 0)
                        return;



                }
                break;
            case AssistantState.EARTHQUAKE:
                break;
            case AssistantState.TORNADO:
                break;
            case AssistantState.LIGHTNING:
                break;
            case AssistantState.STRENGTH:
                break;
            case AssistantState.SUPERCITY:
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
