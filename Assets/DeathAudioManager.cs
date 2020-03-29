using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAudioManager : MonoBehaviour
{
    static public AudioSource deathAudioSource;
    // Start is called before the first frame update
    void Start()
    {
        deathAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
