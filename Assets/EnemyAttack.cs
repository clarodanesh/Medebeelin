using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            Debug.Log("Hitting");
            if (LevelManager.level == "Level1")
            {
                PlayerScript.health = PlayerScript.health - 5;
            }
            else if(LevelManager.level == "Level2")
            {
                PlayerScript.health = PlayerScript.health - 25;
            }
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
