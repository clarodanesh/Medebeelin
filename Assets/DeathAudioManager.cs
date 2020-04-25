using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAudioManager : MonoBehaviour
{
    //static variable for the death audio source so can reference in other scripts
    static public AudioSource deathAudioSource;
    // Start is called before the first frame update
    void Start()
    {
        //get the audiosource component set in the inspector
        deathAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
