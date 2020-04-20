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
    public Text levelText, scoreText, healthText, bossHealthText;
    public string levelNumber;
    public Button randomBtn, healthBtn, speedBtn, scoreBtn;
    public GameObject messagePanel;
    public GameObject instructionPanel;
    public GameObject openUpgradeMenuBtn;
    public GameObject upgradeMenuPanel;
    public GameObject customiseCharacterBtn;
    public GameObject customiseCharacterMenu;
    public GameObject instructionText;
    public static string spriteType;
    public int timesUpgraded;
    public int dismissIncrement;
    int clipPlayedAmount;
    static public bool showInstruction;
    public bool updateIsShown;

    // Start is called before the first frame update
    void Start()
    {
        /*if(check local storage value gameWasLoaded == true/false)
        {
            create a new game else load the game from the server
        }*/
        //upgradeBtn.gameObject.SetActive(false);
        messagePanel.gameObject.SetActive(false);
        instructionPanel.gameObject.SetActive(false);
        openUpgradeMenuBtn.gameObject.SetActive(false);
        upgradeMenuPanel.gameObject.SetActive(false);
        customiseCharacterBtn.gameObject.SetActive(false);
        customiseCharacterMenu.gameObject.SetActive(false);

        level = SceneManager.GetActiveScene().name;

        //Find the GameObject named Best in the scene
        GameObject levelNameGO = GameObject.Find("LevelText");
        GameObject scoreTextGO = GameObject.Find("ScoreText");
        GameObject healthTextGO = GameObject.Find("HealthText");
        GameObject bossHealthTextGO = GameObject.Find("BossHealthText");

        //Get the GUIText Component attached to that GameObject named Best
        levelText = levelNameGO.GetComponent<Text>();
        scoreText = scoreTextGO.GetComponent<Text>();
        healthText = healthTextGO.GetComponent<Text>();
        bossHealthText = bossHealthTextGO.GetComponent<Text>();

        GetLevelNumber(level);

        if (level == "Level1" || level == "Level2")
        {
            PlayerScript.health = 100;
            PlayerScript.speed = 7;
        }
        else if (level == "BossLevel")
        {
            //only if boss level
            BossLevelPlayerScript.health = 100;
            BossLevelPlayerScript.speed = 13;
        }
        
        NectarPickup.nectarValue = 1;
        dismissIncrement = 100;
        timesUpgraded = 0;
        clipPlayedAmount = 0;
        showInstruction = false;
        updateIsShown = false;
        spriteType = "normal";
        randomBtn.interactable = false;
        healthBtn.interactable = false;
        speedBtn.interactable = false;
        scoreBtn.interactable = false;
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

    //Button btn = customiseCharacterBtn.gameObject.GetComponent<Button>();
    //btn.interactable = false;

    public void ShowUpgradeMenu()
    {
        upgradeMenuPanel.gameObject.SetActive(true);
        openUpgradeMenuBtn.gameObject.SetActive(false);
        if(score > 70)
        {
            randomBtn.interactable = true;
        }
        if (score > 100)
        {
            healthBtn.interactable = true;
        }
        if (score > 120)
        {
            speedBtn.interactable = true;
        }
        if (score > 150)
        {
            scoreBtn.interactable = true;
        }
    }

    public void HideUpgradeMenu()
    {
        upgradeMenuPanel.gameObject.SetActive(false);
        openUpgradeMenuBtn.gameObject.SetActive(true);
    }

    public void RandomUpgrade()
    {
        System.Random rnd = new System.Random();
        int randomNumber = Random.Range(1, 4);

        Debug.Log("Random " + randomNumber);
        //int randomNumber = 3;

        openUpgradeMenuBtn.gameObject.SetActive(false);
        upgradeMenuPanel.gameObject.SetActive(false);
        customiseCharacterBtn.gameObject.SetActive(true);
        if(randomNumber == 1)
        {
            UpgradeHealth();
        }
        if(randomNumber == 2)
        {
            UpgradeSpeed();
        }
        if (randomNumber == 3)
        {
            UpgradeScore();
        }
    }

    public void UpgradeHealth()
    {
        Debug.Log("upgrading health");
        openUpgradeMenuBtn.gameObject.SetActive(false);
        upgradeMenuPanel.gameObject.SetActive(false);
        customiseCharacterBtn.gameObject.SetActive(true);
        spriteType = "upgraded";

        score = score - 50;
        PlayerScript.health = PlayerScript.health + 50;
        timesUpgraded++;
    }

    public void UpgradeSpeed()
    {
        Debug.Log("upgrading speed");
        openUpgradeMenuBtn.gameObject.SetActive(false);
        upgradeMenuPanel.gameObject.SetActive(false);
        customiseCharacterBtn.gameObject.SetActive(true);
        spriteType = "upgraded";

        score = score - 50;
        PlayerScript.speed = 9;
        timesUpgraded++;
    }

    public void UpgradeScore()
    {
        Debug.Log("upgrading score");
        openUpgradeMenuBtn.gameObject.SetActive(false);
        upgradeMenuPanel.gameObject.SetActive(false);
        customiseCharacterBtn.gameObject.SetActive(true);
        spriteType = "upgraded";

        score = score - 50;
        NectarPickup.nectarValue = NectarPickup.nectarValue + 1;
        timesUpgraded++;
    }

    public void ShowCharacterCustomiseMenu()
    {
        customiseCharacterBtn.gameObject.SetActive(false);
        customiseCharacterMenu.gameObject.SetActive(true);
    }

    public void HideCustomiseMenu()
    {
        customiseCharacterMenu.gameObject.SetActive(false);
        customiseCharacterBtn.gameObject.SetActive(true);
    }

    public void SetNormalSprite()
    {
        customiseCharacterMenu.gameObject.SetActive(false);
        customiseCharacterBtn.gameObject.SetActive(true);
        spriteType = "normal";
    }

    public void SetUpgradedSprite()
    {
        customiseCharacterMenu.gameObject.SetActive(false);
        customiseCharacterBtn.gameObject.SetActive(true);
        spriteType = "upgraded";
    }

    void DisplayUpgradeButton()
    {
        messagePanel.gameObject.SetActive(true);
        openUpgradeMenuBtn.gameObject.SetActive(true);
        updateIsShown = true;
        Invoke("DismissUpgrade", 1);
    }

    public void UpgradePlayer()
    {
        messagePanel.gameObject.SetActive(false);
        score = score - 50;
        PlayerScript.health = PlayerScript.health + 50;
        timesUpgraded++;
    }

    public void DismissUpgrade()
    {
        messagePanel.gameObject.SetActive(false);
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
        if (level == "BossLevel")
        {
            healthText.text = "Health: " + BossLevelPlayerScript.health.ToString();
            bossHealthText.text = "Boss Health: " + Boss.bossHealth.ToString();
        }
        else
        {
            healthText.text = "Health: " + PlayerScript.health.ToString();
        }

        if(score > 70 && timesUpgraded < 1 && !updateIsShown)
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
        Debug.Log("INTSCORE " + score);
        Debug.Log("from the level manager script " + NectarPickup.nectarValue);
        Debug.Log("LEVEL NAME ::::::::::::::: ");
    }
}
