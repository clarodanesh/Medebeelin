using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoadScript : MonoBehaviour
{
    //trigger user to check if player is going to new level as reached end of current one
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Contains("Player"))
        {
            if (LevelManager.level == "Level1")
            {
                //set the data to the indexeddb so can retrieve it when new scene is opened
                PlayerPrefs.SetString("level", "Level2");
                PlayerPrefs.SetInt("upgrade", LevelManager.timesUpgraded);
                PlayerPrefs.SetString("skin", LevelManager.spriteType);
                PlayerPrefs.SetInt("score", LevelManager.score);
                PlayerPrefs.SetInt("health", PlayerScript.health);
                PlayerPrefs.SetInt("speed", PlayerScript.speed);
                PlayerPrefs.SetInt("nectarpoints", NectarPickup.nectarValue);
                PlayerPrefs.SetInt("bosshealth", PlayerPrefs.GetInt("bosshealth"));

                //load the scene
                SceneManager.LoadScene("Level2");
            }
            else if (LevelManager.level == "Level2")
            {
                //set the data to the indexeddb so can retrieve it when new scene is opened
                PlayerPrefs.SetString("level", "BossLevel");
                PlayerPrefs.SetInt("upgrade", LevelManager.timesUpgraded);
                PlayerPrefs.SetString("skin", LevelManager.spriteType);
                PlayerPrefs.SetInt("score", LevelManager.score);
                PlayerPrefs.SetInt("health", PlayerScript.health);
                PlayerPrefs.SetInt("speed", PlayerScript.speed);
                PlayerPrefs.SetInt("nectarpoints", NectarPickup.nectarValue);
                PlayerPrefs.SetInt("bosshealth", PlayerPrefs.GetInt("bosshealth"));

                //load the scene
                SceneManager.LoadScene("BossLevel");
            }
            else
            {
                //load the main menu scene
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
