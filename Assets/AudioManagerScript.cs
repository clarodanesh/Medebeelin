using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour
{
    //audio source, will set this using gameobject
    static public AudioSource nectarAudioSource;
    // Start is called before the first frame update
    void Start()
    {
        //get the audio source component from this object
        nectarAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
