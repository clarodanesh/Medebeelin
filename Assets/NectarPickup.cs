using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NectarPickup : MonoBehaviour
{
    public static int nectarValue;

    //if the player hits a nectar then destroy it and increase the score
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //WANT TO DELETE THE GAMEOBJECT HERE AND UPDATE THE SCORE
        if (collision.tag.Contains("Player"))
        {
            //increase scorea and play pickup sound
            LevelManager.score += nectarValue;
            
            AudioManagerScript.nectarAudioSource.Play();
            gameObject.SetActive(false);
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
