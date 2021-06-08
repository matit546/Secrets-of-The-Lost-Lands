using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager musicManagerInstance;
    public Sound[] sounds;
    int actualIndexMusic=0;
    void Awake()
    {


        if(musicManagerInstance == null)
        {
            musicManagerInstance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(this);
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.loop = s.loop;
        }
 

    }
    void Update()
    {

        //if (!sounds[actualIndexMusic].source.isPlaying)
        //{
     
        //    int index = GetRandomClip();
        //    actualIndexMusic = index;
        //    Debug.Log(index);
        //    sounds[index].source.Play();
        //}
    }

    public void PlaySound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound._name == name);
        if (s == null)
        {
            //Debug.LogWarning("sound: " + name + " not found");
            return;
        }
        s.source.Play();
    }


    public void StopSound(string name)
    {
        Sound s = Array.Find(sounds, sound => sound._name == name);
        if (s == null)
        {
            //Debug.LogWarning("sound: " + name + " not found");
            return;
        }
        s.source.Stop();
    }

    public void StopAllSound()
    {
        Array.ForEach(sounds, s => s.source.Stop());
    }

    public int GetRandomClip()
    {
        int random = UnityEngine.Random.Range(0, sounds.Length);
        return random;
    }

    public void SetVolume(float volume)
    {
        foreach (Sound s in sounds)
        {
   
                s.source.volume = volume;
            
        }
    }
}
