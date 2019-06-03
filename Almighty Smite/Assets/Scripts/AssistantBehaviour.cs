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
        FADEOUT,
    }
    public static bool Tutorial;
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
    private bool activated;
    private AssistantState currentState;
    public int TimeBeforeFadeOut;
    public static bool Respawned;
    private bool CR_running;

    void Start()
    {
        instance = this;
        CR_running = false;
        AssistantSprite = GetComponentInChildren<Image>();
        TutorialAlpha = GetComponent<CanvasGroup>();
        text = GetComponentInChildren<Text>();
        instance.currentState = AssistantState.INTRO;
        AssistantBehaviour.Respawned = false;
        AssistantBehaviour.Tutorial = true;
    }

    void Update()
    {
        if (Tutorial == true) {
            switch (currentState)
            {
                case AssistantState.INTRO:
                    if (activated != true)
                    {
                        if (TutorialAlpha.alpha != 1)
                            StartCoroutine("FadeIn");
                        text.text = AddLineBreak(Intro);
                        if (CR_running == false)
                            StartCoroutine(ChangeState(AssistantState.EARTHQUAKE, 2));
                    }
                    break;
                case AssistantState.EARTHQUAKE:
                    if (activated != true)
                    {
                        text.text = AddLineBreak(Earthquake);
                        AssistantSprite.sprite = AssitantLooks[3];
                        activated = true;
                    }
                    else
                    {
                        if (AbilitiesInput.EarthquakeSpawned == true && CR_running == false)
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
                        if (AbilitiesInput.TornadoSpawned == true && CR_running == false)
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
                        if (AbilitiesInput.LightningSpawned == true && TutorialAlpha.alpha == 1 && CR_running == false)
                            StartCoroutine("FadeOut");
                    }
                    break;
                case AssistantState.STRENGTH:
                    if (activated != true)
                    {
                        if (TutorialAlpha.alpha == 0)
                            StartCoroutine("FadeIn");
                        text.text = AddLineBreak(Strength);
                        activated = true;
                        AssistantBehaviour.Tutorial = false;
                    }
                    else
                    {
                        if (TutorialAlpha.alpha == 1 && CR_running == false)
                            StartCoroutine(ChangeState(AssistantState.FADEOUT, 3));
                    }
                    break;
                case AssistantState.FADEOUT:
                    if (TutorialAlpha.alpha != 0)
                        StartCoroutine("FadeOut");
                    break;
            }
        }
    }

    private IEnumerator ChangeState(AssistantState newState,float delay)
    {
        CR_running = true;
        yield return new WaitForSeconds(delay);
        activated = false;
        currentState = newState;
        CR_running = false;
        
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
        
        if (currentState == AssistantState.LIGHTNING)
        {
            CityMaster.TutorialRespawn();
            StartCoroutine(ChangeState(AssistantState.STRENGTH, 0));
        }
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
