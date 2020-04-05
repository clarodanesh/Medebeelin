using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NectarPickup : MonoBehaviour
{
    public static int nectarValue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //WANT TO DELETE THE GAMEOBJECT HERE AND UPDATE THE SCORE
        if (collision.tag.Contains("Player"))
        {
            //remove the coin
            Destroy(gameObject);
            //you should also have a manager class where you store your score and other useful stuff
            LevelManager.score = LevelManager.score + nectarValue;
            AudioManagerScript.nectarAudioSource.Play();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("from the nectar pcikup script " + NectarPickup.nectarValue);
    }
}
