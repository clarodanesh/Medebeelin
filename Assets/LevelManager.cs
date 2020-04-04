using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static int score;
    public static string level;
    public static string instructionTextValue;
    public Text levelText, scoreText, healthText;
    public string levelNumber;
    public Button upgradeBtn;
    public GameObject buttonPanel;
    public GameObject instructionPanel;
    public GameObject instructionText;
    public int timesUpgraded;
    public int dismissIncrement;
    int clipPlayedAmount;
    static public bool showInstruction;

    // Start is called before the first frame update
    void Start()
    {
        /*if(check local storage value gameWasLoaded == true/false)
        {
            create a new game else load the game from the server
        }*/
        //upgradeBtn.gameObject.SetActive(false);
        buttonPanel.gameObject.SetActive(false);
        instructionPanel.gameObject.SetActive(false);

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
        dismissIncrement = 100;
        timesUpgraded = 0;
        clipPlayedAmount = 0;
        showInstruction = false;
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

    void DisplayUpgradeButton()
    {
        buttonPanel.gameObject.SetActive(true);
    }

    public void UpgradePlayer()
    {
        buttonPanel.gameObject.SetActive(false);
        score = score - 50;
        PlayerScript.health = PlayerScript.health + 50;
        timesUpgraded++;
        dismissIncrement = dismissIncrement - 100;
    }

    public void DismissUpgrade()
    {
        buttonPanel.gameObject.SetActive(false);
        dismissIncrement = dismissIncrement + 100;
        clipPlayedAmount = 0;
    }


    void DismissInstruction()
    {
        instructionPanel.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("current score " + score);
        Debug.Log("current level " + PlayerPrefs.GetString("score", "null"));

        levelText.text = "Level: " + levelNumber;
        scoreText.text = "Score: " + score.ToString();
        healthText.text = "Health: " + PlayerScript.health.ToString();

        if(score > dismissIncrement && timesUpgraded < 1)
        {
            DisplayUpgradeButton();
            if (clipPlayedAmount == 0)
            {
                UpgradeAudioManager.upgradeAudioSource.Play();
            }
            clipPlayedAmount++;
        }

        if (showInstruction)
        {
            Debug.Log("Allowing");
            instructionPanel.gameObject.SetActive(true);
            instructionText.GetComponent<Text>().text = instructionTextValue; 
            Invoke("DismissInstruction", 5);
            showInstruction = false;
        }
    }
}
