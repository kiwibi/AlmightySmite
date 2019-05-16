using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private static MusicPlayer instance = null;
    public static MusicPlayer Instance { get { return instance; } }
    private AudioSource audioSource;

    void Awake()
    {
        if(Instance != this)
        {
            if (Instance == null)
                instance = this;
            else
            {
                Destroy(gameObject);
                return;
            }
        }
        audioSource = GetComponent<AudioSource>();
        instance = this;
        DontDestroyOnLoad(gameObject);
    }


    public void Play()
    {
        audioSource.Play();
    }


    public void Stop()
    {
        audioSource.Stop();
    }
}