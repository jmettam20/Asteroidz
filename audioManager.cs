using System; 
using UnityEngine;
using Unity.Audio;

public class audioManager : MonoBehaviour
{

    public Sound[] sounds;

    void Awake() {
        //Gather sounds
        foreach (Sound s in sounds) {
           s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch; 
        }
    }

    //Function to play a sound
    public void Play(string name) {
      Sound s =   Array.Find(sounds, sound => sound.name == name);
        s.source.Play(); 
    }

    //Function to stop a sound 
    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Stop();
    }
}
