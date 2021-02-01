using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class AudioController : Singleton<AudioController>
{
    public StringStringDictionary musicClipList; //Key: clip name | Value: eventPath

    public StringStringDictionary soundClipList; //Key: clip name | Value: eventPath

    private StudioEventEmitter musicEmitter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void PlayOneshotClip(string clipName)
    {
        RuntimeManager.PlayOneShot(soundClipList[clipName]);
    }


    public void PlaySound(string clipName)
    {
        var soundInstance = RuntimeManager.CreateInstance(soundClipList[clipName]);

        soundInstance.start();
        soundInstance.release();
    }

    public void PlayMusic(string songName)
    {
        if(musicEmitter == null)
        {
            musicEmitter = gameObject.AddComponent<StudioEventEmitter>();
        }
        musicEmitter.Event = musicClipList[songName];

        musicEmitter.Play();
    } 

    public void StopMusic(bool fadeOut)
    {
        musicEmitter.AllowFadeout = fadeOut;
        musicEmitter.Stop();
    }
}
