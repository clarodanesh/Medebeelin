using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoadScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //WANT TO DELETE THE GAMEOBJECT HERE AND UPDATE THE SCORE
        if (collision.tag.Contains("Player"))
        {
            if (LevelManager.level == "Level1")
            {
                SceneManager.LoadScene("Level2");
            }
            else if (LevelManager.level == "Level2")
            {
                SceneManager.LoadScene("BossLevel");
            }
            else
            {
                SceneManager.LoadScene("MainMenu");
            }
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
