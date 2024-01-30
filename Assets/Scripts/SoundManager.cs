using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    private bool _muteBackgroundMusic = false;
    private bool _muteSoundFx = false;
    public static SoundManager instance;

    private AudioSource _audioSource;

    private void Awake()
    {
        if(instance==null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
                Destroy(this);
        }
    }
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.Play();
    }
    public void ToggleBackGroundMusic()

    {
        _muteBackgroundMusic= !_muteBackgroundMusic;
        if(_muteBackgroundMusic )
        {
            _audioSource.Stop();
        }
        else
        {
            _audioSource.Play();
        }
    }
    public void ToggleSoundFx()
    {
        _muteSoundFx= !_muteSoundFx;
        GameEvents.OnToggleSoundFxMethod();
    }
    public bool IsBackGroundMusicMuted()
    {
        return _muteBackgroundMusic;
    }
    public bool IsSoundFxMuted()
    {
        return _muteSoundFx;
    }
    public void SilenceBackGroundMusic(bool silence)
    {
        if(_muteBackgroundMusic==false)
        {
            if (silence)
                _audioSource.volume = 0f;

            else
            _audioSource.volume=1f;
            
        }
    }

    
}
