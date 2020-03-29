using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeAudioManager : MonoBehaviour
{
    static public AudioSource upgradeAudioSource;
    // Start is called before the first frame update
    void Start()
    {
        upgradeAudioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
