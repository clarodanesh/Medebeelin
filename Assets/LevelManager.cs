using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static int score;
    public static string level;
    public Text levelText, scoreText, healthText;
    public string levelNumber;
    // Start is called before the first frame update
    void Start()
    {
        level = SceneManager.GetActiveScene().name;

        //Find the GameObject named Best in the scene
        GameObject levelNameGO = GameObject.Find("LevelText");
        GameObject scoreTextGO = GameObject.Find("ScoreText");
        GameObject healthTextGO = GameObject.Find("HealthText");

        //Get the GUIText Component attached to that GameObject named Best
        levelText = levelNameGO.GetComponent<Text>();
        scoreText = scoreTextGO.GetComponent<Text>();
        healthText = healthTextGO.GetComponent<Text>();

        GetLevelNumber(level);
        PlayerScript.health = 100;
    }

    void GetLevelNumber(string l)
    {
        if(l == "Level1")
        {
            levelNumber = "1";
        }
        else if(l == "Level2")
        {
            levelNumber = "2";
        }
        else if(l == "BossLevel")
        {
            levelNumber = "Boss Battle";
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("current score " + score);
        Debug.Log("current level " + PlayerPrefs.GetString("score", "null"));

        levelText.text = "Level: " + levelNumber;
        scoreText.text = "Score: " + score.ToString();
        healthText.text = "Health: " + PlayerScript.health.ToString();
    }
}
