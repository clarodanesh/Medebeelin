using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Networking;

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

    public GameObject gameMenuPanel;

    public GameObject showMenuBtnObj;
    public GameObject saveBtnObj;
    public GameObject quitBtnObj;
    Button btn;
    Button saveBtn;
    Button quitBtn;

    GameObject attackText;
    GameObject evadeText;
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
        gameMenuPanel.gameObject.SetActive(false);

        showMenuBtnObj = GameObject.Find("ShowGameMenuBtn");
        btn = showMenuBtnObj.GetComponent<Button>();
        showMenuBtnObj.gameObject.SetActive(true);

        //attackText.gameObject.SetActive(false);
        //evadeText.gameObject.SetActive(false);

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
       

        GetLevelNumber(level);

        if (level == "Level1" || level == "Level2")
        {
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
        
        NectarPickup.nectarValue = PlayerPrefs.GetInt("nectarpoints");
        dismissIncrement = 100;
        timesUpgraded = PlayerPrefs.GetInt("upgrade");
        clipPlayedAmount = 0;
        showInstruction = false;
        updateIsShown = false;
        spriteType = PlayerPrefs.GetString("skin");
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
    }

    public void SaveGame()
    {

        /*
         * 
         * PlayerPrefs.SetString("level", "Level2");
                PlayerPrefs.SetInt("upgrade", LevelManager.timesUpgraded);
                PlayerPrefs.SetString("skin", LevelManager.spriteType);
                PlayerPrefs.SetInt("score", LevelManager.score);
                PlayerPrefs.SetInt("health", PlayerScript.health);
                PlayerPrefs.SetInt("speed", PlayerScript.speed);
                PlayerPrefs.SetInt("nectarpoints", NectarPickup.nectarValue);
         */

        DataToSend progressData = new DataToSend();
        progressData.skin = LevelManager.spriteType;
        progressData.upgrade = LevelManager.timesUpgraded;
        progressData.level = level;
        progressData.score = LevelManager.score;
        if (level == "Level1" || level == "Level2")
        {
            progressData.health = PlayerScript.health;
            progressData.uname = PlayerPrefs.GetString("username");
            progressData.speed = PlayerScript.speed;
        }else if(level == "BossLevel")
        {
            progressData.health = BossLevelPlayerScript.health;
            progressData.uname = PlayerPrefs.GetString("username");
            progressData.speed = BossLevelPlayerScript.speed;
        }
        progressData.nectarpoints = NectarPickup.nectarValue;
        string jsonData = JsonUtility.ToJson(progressData);

        PlayerPrefs.SetString("level", progressData.level);
        PlayerPrefs.SetInt("upgrade", progressData.upgrade);
        PlayerPrefs.SetString("skin", progressData.skin);
        PlayerPrefs.SetInt("score", progressData.score);
        PlayerPrefs.SetInt("health", progressData.health);
        PlayerPrefs.SetInt("speed", progressData.speed);
        PlayerPrefs.SetInt("nectarpoints", progressData.nectarpoints);

        StartCoroutine(PostRequestJSON("https://vesta.uclan.ac.uk/~diqbal/UnityScripts/saveData.php", jsonData));
    }

    IEnumerator PostRequestJSON(string url, string json)
    {
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

    public void CloseGameMenu()
    {
        gameMenuPanel.gameObject.SetActive(false);
        showMenuBtnObj.gameObject.SetActive(true);
    }

    public void QuitGame()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ShowGameMenu()
    {
        gameMenuPanel.gameObject.SetActive(true);
        showMenuBtnObj.gameObject.SetActive(false);
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
