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
    private CanvasGroup TutorialAlpha;
    private Text text;
    public Sprite[] AssitantLooks;
    [Tooltip("All the lines used add & for linebreak")]
    public string Intro;
    public string Earthquake;
    public string Tornado;
    public string Lightning;
    public string Strength;
    public string SuperCity;
    private bool activated;
    private AssistantState currentState;
    public int TimeBeforeFadeOut;
    public static bool Respawned;

    void Start()
    {
        instance = this;
        AssistantSprite = GetComponentInChildren<Image>();
        TutorialAlpha = GetComponent<CanvasGroup>();
        text = GetComponentInChildren<Text>();
        instance.currentState = AssistantState.INTRO;
        AssistantBehaviour.Respawned = false;
    }

    // Update is called once per frame
    void Update()
    {
        

        switch(currentState)
        {
            case AssistantState.INTRO:
                if(activated != true)
                {
                    if(TutorialAlpha.alpha != 1)
                        StartCoroutine("FadeIn");
                    text.text = AddLineBreak(Intro);
                    StartCoroutine(ChangeState(AssistantState.EARTHQUAKE, 3));
                }
                break;
            case AssistantState.EARTHQUAKE:
                if(activated != true)
                {
                    text.text = AddLineBreak(Earthquake);
                    activated = true;
                }
                else
                {
                    if (AbilitiesInput.EarthquakeSpawned == true)
                        StartCoroutine(ChangeState(AssistantState.TORNADO, 0));
                }
                break;
            case AssistantState.TORNADO:
                if (activated != true)
                {
                    text.text = AddLineBreak(Tornado);
                    activated = true;
                }
                else
                {
                    if (AbilitiesInput.TornadoSpawned == true)
                        StartCoroutine(ChangeState(AssistantState.LIGHTNING, 0));
                }
                break;
            case AssistantState.LIGHTNING:
                if (activated != true)
                {
                    text.text = AddLineBreak(Lightning);
                    activated = true;
                }
                else
                {
                    if (AbilitiesInput.LightningSpawned == true && TutorialAlpha.alpha != 0)
                        StartCoroutine("FadeOut");

                    if (AssistantBehaviour.Respawned == true)
                        StartCoroutine(ChangeState(AssistantState.STRENGTH, 1));
                }
                break;
            case AssistantState.STRENGTH:
                if (activated != true)
                {
                    if (TutorialAlpha.alpha != 1)
                        StartCoroutine("FadeIn");
                    text.text = AddLineBreak(Strength);
                }
                break;
            case AssistantState.SUPERCITY:
                if (activated != true)
                {
                    if (TutorialAlpha.alpha != 1)
                        StartCoroutine("FadeIn");
                    text.text = AddLineBreak(SuperCity);
                }
                break;
        }
    }

    public static IEnumerator ChangeState(AssistantState newState,float delay)
    {
        yield return new WaitForSeconds(delay);
        instance.currentState = newState;
        instance.activated = false;
    }

    IEnumerator FadeIn()
    {
        float tmpColor;
        for (float f = 0f; f <= 1.0; f += 0.01f)
        {
            tmpColor = f;
            TutorialAlpha.alpha = tmpColor;
            yield return null;
        }
        tmpColor = 1;
        TutorialAlpha.alpha = tmpColor;
        activated = true;
    }

    IEnumerator FadeOut()
    {
        float tmpColor;
        for (float f = 1f; f >= 0; f -= 0.01f)
        {
            tmpColor = f;
            TutorialAlpha.alpha = tmpColor;
            yield return null;
        }
        tmpColor = 0;
        TutorialAlpha.alpha = tmpColor;
    }

    private void TakeAwayTimer()
    {
        StartCoroutine("FadeOut");
    }

    private string AddLineBreak(string toReplace)
    {
        string tmpString;
        tmpString = toReplace.Replace('&', '\n');
        return tmpString;
    }
}
