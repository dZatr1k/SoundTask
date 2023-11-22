using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    private AudioSource _audioSource;

    [SerializeField] private List<DataSound> _dataSounds = new List<DataSound>();

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void PlaySoundEffect(string clipName)
    {
        var audioClip = GetAudioClip(clipName);
        _audioSource.clip = audioClip;
        _audioSource.Play();
    }

    private AudioClip GetAudioClip(string clipName)
    {
        AudioClip clip = null;

        /*foreach (var sound in _dataSounds.Where(sound => sound.name == clipName))
        {
            clip = sound.audioClip;
        }*/
        
        foreach (var sound in _dataSounds)
        {
            if (sound.name == clipName)
            {
                clip = sound.audioClip;
            }
        }
        
        return clip;
    }
    
    [Serializable]
    private class DataSound
    {
        public string name;
        public AudioClip audioClip;
    }
}
