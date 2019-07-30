using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager _audioManager;
    
    [SerializeField] private List<Audio> clips = new List<Audio>();
    public Dictionary<string, AudioClip> clipsDictionary = new Dictionary<string, AudioClip>();
    public AudioSource soundEffect;
        
    // Start is called before the first frame update
    void Start()
    {
        if (_audioManager)
        {
            Destroy(gameObject);
        }
        else
        {
            _audioManager = this;
            foreach (var clip in clips)
            {
                clipsDictionary.Add(clip._name, clip.clip);
            }
            DontDestroyOnLoad(this);
        }
    }

    public void Play(string clipName)
    {
        PlayClip(clipsDictionary[clipName]);
    }
    
    void PlayClip(AudioClip clip)
    {
        soundEffect.PlayOneShot(clip);
    }
}

[Serializable]
public class Audio
{
    public string _name;
    public AudioClip clip;
}
