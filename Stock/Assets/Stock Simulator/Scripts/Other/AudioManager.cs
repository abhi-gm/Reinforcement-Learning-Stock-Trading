//Poylgon Planet - Contact. https://polygonplanet.com/contact/
//Copyright © 2016-2018 Polygon Planet. All rights reserved. https://polygonplanet.com/privacy-policy/
//This source file is subject to Unity Technologies Asset Store Terms of Service. https://unity3d.com/legal/as_terms

#pragma warning disable 0168 //Variable declared, but not used.
#pragma warning disable 0219 //Variable assigned, but not used.
#pragma warning disable 0414 //Private field assigned, but not used.
#pragma warning disable 0649 //Variable asisgned to, and will always have default value.

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour 
{
    [Header("Sound")]
    public AudioSource uiClickSound;

    [Header("Music")]
    public AudioSource musicSource;
    public List<AudioClip> soundTracks;

    //Singleton
    public static AudioManager instance;

    //Variables
    private int currentSoundTrack;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start()
    {
        StartCoroutine(PlayMusic());
    }

    public void PlayAudioClip(AudioSource source)
    {
        source.Play();
    }

    private IEnumerator PlayMusic()
    {
        musicSource.clip = soundTracks[currentSoundTrack];
        musicSource.Play();
        currentSoundTrack++;
        if (currentSoundTrack >= soundTracks.Count)
            currentSoundTrack = 0;
        yield return new WaitUntil(() => musicSource.isPlaying == false);
        StartCoroutine(PlayMusic());
    }
}