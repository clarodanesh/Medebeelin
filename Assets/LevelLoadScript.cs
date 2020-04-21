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
                PlayerPrefs.SetString("level", "Level2");
                PlayerPrefs.SetInt("upgrade", LevelManager.timesUpgraded);
                PlayerPrefs.SetString("skin", LevelManager.spriteType);
                PlayerPrefs.SetInt("score", LevelManager.score);
                PlayerPrefs.SetInt("health", PlayerScript.health);
                PlayerPrefs.SetInt("speed", PlayerScript.speed);
                PlayerPrefs.SetInt("nectarpoints", NectarPickup.nectarValue);
                PlayerPrefs.SetInt("bosshealth", PlayerPrefs.GetInt("bosshealth"));

                SceneManager.LoadScene("Level2");
            }
            else if (LevelManager.level == "Level2")
            {
                PlayerPrefs.SetString("level", "BossLevel");
                PlayerPrefs.SetInt("upgrade", LevelManager.timesUpgraded);
                PlayerPrefs.SetString("skin", LevelManager.spriteType);
                PlayerPrefs.SetInt("score", LevelManager.score);
                PlayerPrefs.SetInt("health", PlayerScript.health);
                PlayerPrefs.SetInt("speed", PlayerScript.speed);
                PlayerPrefs.SetInt("nectarpoints", NectarPickup.nectarValue);
                PlayerPrefs.SetInt("bosshealth", PlayerPrefs.GetInt("bosshealth"));

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
