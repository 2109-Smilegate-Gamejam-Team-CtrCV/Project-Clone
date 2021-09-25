using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    private IReadOnlyDictionary<string, AudioSource> _bgmDictionary;
    private IReadOnlyDictionary<string, AudioSource> _vfxDictionary;
    private AudioSource _nowPlayingBGM;
    
    protected override void OnAwake()
    {
        _bgmDictionary = Resources.LoadAll<AudioSource>("BGM")
            .Select(audioSource => Instantiate(audioSource, transform))
            .ToDictionary(value => value.name);
        
        _vfxDictionary = Resources.LoadAll<AudioSource>("VFX")
            .Select(audioSource => Instantiate(audioSource, transform))
            .ToDictionary(value => value.name);

        foreach (var audioSource in _bgmDictionary.Values)
        {
            audioSource.loop = true;
        }
    }

    public void PlayBGM(string title)
    {
        if (_bgmDictionary.TryGetValue(title, out var audioSource))
        {
            audioSource.Play();
            _nowPlayingBGM?.Stop();
            _nowPlayingBGM = audioSource;
        }
    }

    public void PlayVFX(string name)
    {
        if (_vfxDictionary.TryGetValue(name, out var audioSource))
        {
            audioSource.Play();
        }
    }
}
