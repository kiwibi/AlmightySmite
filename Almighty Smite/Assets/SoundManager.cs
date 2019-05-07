using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip EarthquakeSound;
    public AudioClip TornadoSound;
    public AudioClip LightningSound;

    public static SoundManager instance = null;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        EventManager.StartListening("Tornado", tornadoSound);
        EventManager.StartListening("Lightning", lightningSound);
        EventManager.StartListening("Earthquake", earthquakeSound);
        EventManager.StartListening("StopSound", stopSound);
    }

    private void OnDisable()
    {
        EventManager.StopListening("Tornado", tornadoSound);
        EventManager.StopListening("Lightning", lightningSound);
        EventManager.StopListening("Earthquake", earthquakeSound);
        EventManager.StopListening("StopSound", stopSound);
    }

    private void tornadoSound()
    {
        audioSource.clip = TornadoSound;
        audioSource.Play();
    }

    private void lightningSound()
    {
        audioSource.clip = LightningSound;
        audioSource.Play();
    }

    private void earthquakeSound()
    {
        audioSource.clip = EarthquakeSound;
        audioSource.Play();
    }

    private void stopSound()
    {
        audioSource.Stop();
    }
}
