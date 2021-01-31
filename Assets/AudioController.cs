using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class AudioController : Singleton<AudioController>
{

    public StringStringDictionary soundClipList; //Key: clip name | Value: eventPath

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

}
