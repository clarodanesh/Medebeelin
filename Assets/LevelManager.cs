using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;

public class LevelManager : MonoBehaviour
{
    //public variables used across scripts
    public static int score;
    public static string level;
    public static string instructionTextValue;

    //public variables set using the inspector
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
    public GameObject gameMenuPanel;
    public GameObject showMenuBtnObj;
    public GameObject saveBtnObj;
    public GameObject quitBtnObj;

    //will get these manually using the script
    Button btn;
    Button saveBtn;
    Button quitBtn;
    GameObject attackText;
    GameObject evadeText;
    //will use these across scripts
    public static string spriteType;
    public static int timesUpgraded;
    public int dismissIncrement;
    int clipPlayedAmount;
    static public bool showInstruction;
    public bool updateIsShown;

    bool saved;

    // Start is called before the first frame update
    void Start()
    {
        //need to set all of the ui elements to false that are called with trigger events and other events
        messagePanel.gameObject.SetActive(false);
        instructionPanel.gameObject.SetActive(false);
        openUpgradeMenuBtn.gameObject.SetActive(false);
        upgradeMenuPanel.gameObject.SetActive(false);
        customiseCharacterBtn.gameObject.SetActive(false);
        customiseCharacterMenu.gameObject.SetActive(false);
        gameMenuPanel.gameObject.SetActive(false);

        //set the menu button to active
        showMenuBtnObj = GameObject.Find("ShowGameMenuBtn");
        btn = showMenuBtnObj.GetComponent<Button>();
        showMenuBtnObj.gameObject.SetActive(true);

        //set the level name to use across scripts
        level = SceneManager.GetActiveScene().name;

        //Find the GameObjects by their name in the scene
        GameObject levelNameGO = GameObject.Find("LevelText");
        GameObject scoreTextGO = GameObject.Find("ScoreText");
        GameObject healthTextGO = GameObject.Find("HealthText");
        GameObject bossHealthTextGO = GameObject.Find("BossHealthText");

        //Get the GUIText Component attached to that GameObject
        levelText = levelNameGO.GetComponent<Text>();
        scoreText = scoreTextGO.GetComponent<Text>();
        healthText = healthTextGO.GetComponent<Text>();
       
        //need to get the level number so i can use it for ui later
        GetLevelNumber(level);

        if (level == "Level1" || level == "Level2")
        {
            //if loading the platformer levels then get the  indexeddb values
            LevelManager.score = PlayerPrefs.GetInt("score");
            PlayerScript.health = PlayerPrefs.GetInt("health");
            PlayerScript.speed = PlayerPrefs.GetInt("speed");
        }
        else if (level == "BossLevel")
        {
            //only if boss level
            bossHealthText = bossHealthTextGO.GetComponent<Text>();
            LevelManager.score = PlayerPrefs.GetInt("score");
            BossLevelPlayerScript.health = PlayerPrefs.GetInt("health");
            BossLevelPlayerScript.speed = 13;
        }
        
        //set the values from indexeddb
        NectarPickup.nectarValue = PlayerPrefs.GetInt("nectarpoints");
        dismissIncrement = 100;
        //get the amount of times upgraded from indexed db as player only alloweed 1 upgrade
        timesUpgraded = PlayerPrefs.GetInt("upgrade");
        clipPlayedAmount = 0;
        showInstruction = false;
        updateIsShown = false;
        //get the skin from indexeddb
        spriteType = PlayerPrefs.GetString("skin");
        //set the buttons to false as they will be made interactable when player gets enough score
        randomBtn.interactable = false;
        healthBtn.interactable = false;
        speedBtn.interactable = false;
        scoreBtn.interactable = false;
    }

    //set the level string into a variable to use for ui
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

    //this will show the upgrade menu on screen
    public void ShowUpgradeMenu()
    {
        //set the panel to active and the upgrade button to hidden
        upgradeMenuPanel.gameObject.SetActive(true);
        openUpgradeMenuBtn.gameObject.SetActive(false);
        //buttons are inteactable based on amount of score
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

    //used by button to hide the menu thats why its public
    public void HideUpgradeMenu()
    {
        upgradeMenuPanel.gameObject.SetActive(false);
        openUpgradeMenuBtn.gameObject.SetActive(true);
    }

    //this will get a random upgrade as player has less than 100 points
    public void RandomUpgrade()
    {
        System.Random rnd = new System.Random();
        int randomNumber = Random.Range(1, 4);

        //set the panel to hideen and show the customise character button instead
        openUpgradeMenuBtn.gameObject.SetActive(false);
        upgradeMenuPanel.gameObject.SetActive(false);
        customiseCharacterBtn.gameObject.SetActive(true);
        //check the rasndom value and upgrade accoridngly
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

    //button calls this to upgrade the health
    public void UpgradeHealth()
    {
        //set the panels and buttons to false and true
        openUpgradeMenuBtn.gameObject.SetActive(false);
        upgradeMenuPanel.gameObject.SetActive(false);
        customiseCharacterBtn.gameObject.SetActive(true);
        spriteType = "upgraded";

        //use score as user clicked upgrade
        score = score - 50;
        //increase health
        PlayerScript.health = PlayerScript.health + 50;
        timesUpgraded++;
    }

    //use the button to upgrade the speed
    public void UpgradeSpeed()
    {
        //set the panels and buttons to false and true
        openUpgradeMenuBtn.gameObject.SetActive(false);
        upgradeMenuPanel.gameObject.SetActive(false);
        customiseCharacterBtn.gameObject.SetActive(true);
        spriteType = "upgraded";

        //take the score away from the user
        score = score - 50;
        PlayerScript.speed = 9;
        timesUpgraded++;
    }

    //use the score button to upgrade the score
    public void UpgradeScore()
    {
        //set the panels and buttons to false and true
        openUpgradeMenuBtn.gameObject.SetActive(false);
        upgradeMenuPanel.gameObject.SetActive(false);
        customiseCharacterBtn.gameObject.SetActive(true);
        spriteType = "upgraded";

        //take the score away from the user
        score = score - 50;
        NectarPickup.nectarValue = NectarPickup.nectarValue + 1;
        timesUpgraded++;
    }

    //shows the menu on screen
    public void ShowCharacterCustomiseMenu()
    {
        customiseCharacterBtn.gameObject.SetActive(false);
        customiseCharacterMenu.gameObject.SetActive(true);
    }

    //hides the customise menu from the screen
    public void HideCustomiseMenu()
    {
        customiseCharacterMenu.gameObject.SetActive(false);
        customiseCharacterBtn.gameObject.SetActive(true);
    }

    //sets the sprite to normal
    public void SetNormalSprite()
    {
        customiseCharacterMenu.gameObject.SetActive(false);
        customiseCharacterBtn.gameObject.SetActive(true);
        spriteType = "normal";
    }

    //sets the sprite to the upgraded one
    public void SetUpgradedSprite()
    {
        customiseCharacterMenu.gameObject.SetActive(false);
        customiseCharacterBtn.gameObject.SetActive(true);
        spriteType = "upgraded";
    }

    //diplaye the upgrade button
    void DisplayUpgradeButton()
    {
        messagePanel.gameObject.SetActive(true);
        openUpgradeMenuBtn.gameObject.SetActive(true);
        updateIsShown = true;
        Invoke("DismissUpgrade", 1);
    }

    //upgrade the player
    public void UpgradePlayer()
    {
        messagePanel.gameObject.SetActive(false);
        score = score - 50;
        PlayerScript.health = PlayerScript.health + 50;
        timesUpgraded++;
    }

    //dismiss the upgrade option
    public void DismissUpgrade()
    {
        messagePanel.gameObject.SetActive(false);
        clipPlayedAmount = 0;
    }

    //dimiss an instruction
    void DismissInstruction()
    {
        instructionPanel.gameObject.SetActive(false);
    }

    //need to create a serializable object to save game data to db
    [System.Serializable]
    public class DataToSend
    {
        public string uname;
        public string skin;
        public int upgrade;
        public string level;
        public int score;
        public int health;
        public int speed;
        public int nectarpoints;
        public int bosshealth;
    }

    //used to save the current game progress
    public void SaveGame()
    {
        //create an instance of the serializable object
        DataToSend progressData = new DataToSend();
        
        /*
         * setting the data that needs to be saved to the corresponding variable in the object
         */
        progressData.skin = LevelManager.spriteType;
        progressData.upgrade = LevelManager.timesUpgraded;
        progressData.level = level;
        progressData.score = LevelManager.score;
        if (level == "Level1" || level == "Level2")
        {
            //level 1 and 2 saved data is different to boss level
            progressData.health = PlayerScript.health;
            progressData.uname = PlayerPrefs.GetString("username");
            progressData.speed = PlayerScript.speed;
            PlayerPrefs.SetInt("bosshealth", PlayerPrefs.GetInt("bosshealth"));
            progressData.bosshealth = PlayerPrefs.GetInt("bosshealth");
        }
        else if(level == "BossLevel")
        {
            //save the boss level data
            progressData.health = BossLevelPlayerScript.health;
            progressData.uname = PlayerPrefs.GetString("username");
            progressData.speed = BossLevelPlayerScript.speed;
            progressData.bosshealth = Boss.bossHealth;
        }
        progressData.nectarpoints = NectarPickup.nectarValue;
        string jsonData = JsonUtility.ToJson(progressData);

        //set the data in the indexed db
        PlayerPrefs.SetString("level", progressData.level);
        PlayerPrefs.SetInt("upgrade", progressData.upgrade);
        PlayerPrefs.SetString("skin", progressData.skin);
        PlayerPrefs.SetInt("score", progressData.score);
        PlayerPrefs.SetInt("health", progressData.health);
        PlayerPrefs.SetInt("speed", progressData.speed);
        PlayerPrefs.SetInt("nectarpoints", progressData.nectarpoints);
        PlayerPrefs.SetInt("bosshealth", progressData.bosshealth);

        //post the data to save to the server
        StartCoroutine(PostRequestJSON("https://vesta.uclan.ac.uk/~diqbal/UnityScripts/saveData.php", jsonData));
    }

    //post request handler
    IEnumerator PostRequestJSON(string url, string json)
    {
        //setup the request
        var uwr = new UnityWebRequest(url, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
        uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");

        yield return uwr.SendWebRequest();
        if (uwr.isNetworkError)
        {
            Debug.Log("error sending request");
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);
        }
    }

    //closes the game menu
    public void CloseGameMenu()
    {
        gameMenuPanel.gameObject.SetActive(false);
        showMenuBtnObj.gameObject.SetActive(true);
    }

    //quit the current game and go to main menu
    public void QuitGame()
    {
        SceneManager.LoadScene("MainMenu");
    }

    //show the game menu 
    public void ShowGameMenu()
    {
        gameMenuPanel.gameObject.SetActive(true);
        showMenuBtnObj.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        //set the level number, score and health to the ui
        levelText.text = "Level: " + levelNumber;
        scoreText.text = "Score: " + score.ToString();
        if (level == "BossLevel")
        {
            //if boss level then set boss health too
            healthText.text = "Health: " + BossLevelPlayerScript.health.ToString();
            bossHealthText.text = "Boss Health: " + Boss.bossHealth.ToString();
        }
        else
        {
            healthText.text = "Health: " + PlayerScript.health.ToString();
        }

        //if the score > 7- and not upgraded yet then show the upgrade
        if(score > 70 && timesUpgraded < 1 && !updateIsShown)
        {
            DisplayUpgradeButton();
            if (clipPlayedAmount == 0)
            {
                UpgradeAudioManager.upgradeAudioSource.Play();
            }
            clipPlayedAmount++;
        }

        //show the instructions
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
