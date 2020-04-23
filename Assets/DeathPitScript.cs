using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathPitScript : MonoBehaviour
{
    //trigger checks if the player hit it then kill the player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Contains("Player"))
        {
            DeathAudioManager.deathAudioSource.Play();
            //PlayerScript.health = 0;
            Invoke("RemovePlayerHealth", 1);
        }
    }

    //kill the player
    void RemovePlayerHealth()
    {
        PlayerScript.health = 0;
        LevelManager.score = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
