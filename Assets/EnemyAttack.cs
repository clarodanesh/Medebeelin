using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    //checking for collision with the player and checking the level, level means higher or lower health taken from enemy hits
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            Debug.Log("Hitting");
            if (LevelManager.level == "Level1")
            {
                //level 1 is easier so less health taken
                PlayerScript.health = PlayerScript.health - 5;
            }
            else if(LevelManager.level == "Level2")
            {
                //level 2 harder so more health taken
                PlayerScript.health = PlayerScript.health - 25;
            }
            //play the hit/death sound
            DeathAudioManager.deathAudioSource.Play();
        }
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
