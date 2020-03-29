using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioManagerScript : MonoBehaviour
{
    static public AudioSource playerAudioSource;
    // Start is called before the first frame update
    void Start()
    {
        playerAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
