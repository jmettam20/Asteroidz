
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{
    //Audio clip - .wav/.mp4 ect
    public AudioClip clip; 

    //Name
    public string name;

    //Volume
    [Range(0f,1f)]
    public float volume;

    //Pitch
    [Range(.1f, 3f)]
    public float pitch;

    //Audio source
    [HideInInspector]
    public AudioSource source; 
}
