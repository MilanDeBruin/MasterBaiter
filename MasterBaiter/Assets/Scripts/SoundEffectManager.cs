using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectManager : MonoBehaviour
{
    public static SoundEffectManager Instance;

    [SerializeField] private AudioSource m_AudioSource;

    [SerializeField] private AudioClip CastAudio;
    [SerializeField] private AudioClip CaughtAudio;


    private void Awake()
    {
        Instance = this;
    }

    public void InvokeCaught()
    {
        m_AudioSource.clip = CaughtAudio;
        m_AudioSource.Play();
    }

    public void InvokeCast()
    {
        m_AudioSource.clip = CastAudio;
        m_AudioSource.Play();
    }
}
