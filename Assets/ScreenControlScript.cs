using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ScreenControlScript : MonoBehaviour
{
    public GameObject rbtn;
    Button btn;

    //serializable object used to send data to server
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

    //serializable object to hold highscore uname and score
    [System.Serializable]
    public class HighScore
    {
        public string uname;
        public int score;
    }

    //seriazlizable object used to get the saved data as a list
    [System.Serializable]
    public class DataTable
    {
        public List<DataRetrieved> progressData;
    }

    //the data retreieved object that will be put intot a list
    [System.Serializable]
    public class DataRetrieved
    {
        public int id;
        public string skin;
        public int upgrade;
        public string level;
        public int score;
        public int health;
        public string username;
        public int speed;
        public int nectarpoints;
        public int bosshealth;
    }

    //the username to send to the server
    [System.Serializable]
    public class username
    {
        public string uname;
    }

    //starts a new game
    public void StartGame()
    {
        //need to load level1 as a new game
        SceneManager.LoadScene("Level1");
        DataToSend progressData = new DataToSend();
        //set the progress data to new data
        progressData.skin = "normal";
        progressData.upgrade = 0;
        progressData.level = "Level1";
        progressData.score = 0;
        if(PlayerPrefs.GetInt("isFB") == 1)
        {
            progressData.health = 150;
            progressData.score = 50;
        }
        else
        {
            progressData.health = 100;
            progressData.score = 0;
        }
        progressData.uname = PlayerPrefs.GetString("username");
        progressData.speed = 7;
        progressData.nectarpoints = 1;
        progressData.bosshealth = 400;
        string jsonData = JsonUtility.ToJson(progressData);

        //set the new data to indexeddb too
        PlayerPrefs.SetString("level", progressData.level);
        PlayerPrefs.SetInt("upgrade", progressData.upgrade);
        PlayerPrefs.SetString("skin", progressData.skin);
        PlayerPrefs.SetInt("score", progressData.score);
        PlayerPrefs.SetInt("health", progressData.health);
        PlayerPrefs.SetInt("speed", progressData.speed);
        PlayerPrefs.SetInt("nectarpoints", progressData.nectarpoints);
        PlayerPrefs.SetInt("bosshealth", progressData.bosshealth);

        //post the dta to the server to save it
        StartCoroutine(PostRequestJSON("https://vesta.uclan.ac.uk/~diqbal/UnityScripts/saveData.php", jsonData));
    }

    //json request used to handle the save data
    IEnumerator PostRequestJSON(string url, string json)
    {
        //create web request
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

    //post the username andg get the saved data back
    IEnumerator PostUsername(string url, string json)
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
            if (uwr.downloadHandler.text == "no")
            {
                btn.interactable = false;
            }
            else
            {
                DataTable pd = JsonUtility.FromJson<DataTable>(uwr.downloadHandler.text);

                //set the reqtrieve saved data into indexed db to get on other scenes

                PlayerPrefs.SetString("level", pd.progressData[0].level);
                PlayerPrefs.SetInt("upgrade", pd.progressData[0].upgrade);
                PlayerPrefs.SetString("skin", pd.progressData[0].skin);
                PlayerPrefs.SetInt("score", pd.progressData[0].score);
                PlayerPrefs.SetInt("health", pd.progressData[0].health);
                PlayerPrefs.SetInt("speed", pd.progressData[0].speed);
                PlayerPrefs.SetInt("nectarpoints", pd.progressData[0].nectarpoints);
                PlayerPrefs.SetInt("bosshealth", pd.progressData[0].bosshealth);

                //load the level that the user resumed from
                SceneManager.LoadScene(pd.progressData[0].level);
            }
        }
    }

    //opens instructions scene
    public void OpenInstructions()
    {
        SceneManager.LoadScene("Instructions");
    }

    //opens the highscore  scense
    public void OpenHS()
    {
        SceneManager.LoadScene("HighScores");
    }

    //resumes the game by sending a request to the server for the saved data
    public void ResumeGame()
    {
        username uname = new username();
        uname.uname = PlayerPrefs.GetString("username");
        string jsonData = JsonUtility.ToJson(uname);

        StartCoroutine(PostUsername("https://vesta.uclan.ac.uk/~diqbal/UnityScripts/getSaveData.php", jsonData));
    }

    //opent he main menu scnee
    public void OpenMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    //upload the highscore to the db
    public void UploadHighScore()
    {
        DataToSend progressData = new DataToSend();
        progressData.score = PlayerPrefs.GetInt("score");
        progressData.uname = PlayerPrefs.GetString("username");
        string jsonData = JsonUtility.ToJson(progressData);

        //save the highscore
        StartCoroutine(PostHS("https://vesta.uclan.ac.uk/~diqbal/UnityScripts/saveHS.php", jsonData));

        //open the highscore scnee
        OpenHS();
    }

    //posts high score to server
    IEnumerator PostHS(string url, string json)
    {
        //make a web request
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
            
            Debug.Log(uwr.downloadHandler.text);
        }
    }

    // Start is called before the first frame updates
    void Start()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            rbtn = GameObject.Find("ResumeGameBtn");
            btn = rbtn.GetComponent<Button>();
            //ResumeGame();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
