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

    [System.Serializable]
    public class DataTable
    {
        public List<DataRetrieved> progressData;
    }

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

    [System.Serializable]
    public class username
    {
        public string uname;
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
        DataToSend progressData = new DataToSend();
        progressData.skin = "normal";
        progressData.upgrade = 0;
        progressData.level = "Level1";
        progressData.score = 0;
        progressData.health = 100;
        progressData.uname = PlayerPrefs.GetString("username");
        progressData.speed = 7;
        progressData.nectarpoints = 1;
        progressData.bosshealth = 400;
        string jsonData = JsonUtility.ToJson(progressData);

        PlayerPrefs.SetString("level", progressData.level);
        PlayerPrefs.SetInt("upgrade", progressData.upgrade);
        PlayerPrefs.SetString("skin", progressData.skin);
        PlayerPrefs.SetInt("score", progressData.score);
        PlayerPrefs.SetInt("health", progressData.health);
        PlayerPrefs.SetInt("speed", progressData.speed);
        PlayerPrefs.SetInt("nectarpoints", progressData.nectarpoints);
        PlayerPrefs.SetInt("bosshealth", progressData.bosshealth);

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
                Debug.Log(uwr.downloadHandler.text);
                DataTable pd = JsonUtility.FromJson<DataTable>(uwr.downloadHandler.text);
                Debug.Log(pd);
                Debug.Log("----------------------");
                Debug.Log(pd.progressData[0].id);
                Debug.Log(pd.progressData[0].skin);
                Debug.Log(pd.progressData[0].upgrade);
                Debug.Log(pd.progressData[0].level);
                Debug.Log(pd.progressData[0].score);
                Debug.Log(pd.progressData[0].health);
                Debug.Log(pd.progressData[0].username);
                Debug.Log(pd.progressData[0].speed);
                Debug.Log(pd.progressData[0].nectarpoints);
                Debug.Log(pd.progressData[0].bosshealth);
                Debug.Log("----------------------");

                PlayerPrefs.SetString("level", pd.progressData[0].level);
                PlayerPrefs.SetInt("upgrade", pd.progressData[0].upgrade);
                PlayerPrefs.SetString("skin", pd.progressData[0].skin);
                PlayerPrefs.SetInt("score", pd.progressData[0].score);
                PlayerPrefs.SetInt("health", pd.progressData[0].health);
                PlayerPrefs.SetInt("speed", pd.progressData[0].speed);
                PlayerPrefs.SetInt("nectarpoints", pd.progressData[0].nectarpoints);
                PlayerPrefs.SetInt("bosshealth", pd.progressData[0].bosshealth);

                SceneManager.LoadScene(pd.progressData[0].level);
            }
        }
    }

    public void OpenInstructions()
    {
        SceneManager.LoadScene("Instructions");
    }

    

    public void ResumeGame()
    {
        username uname = new username();
        uname.uname = PlayerPrefs.GetString("username");
        string jsonData = JsonUtility.ToJson(uname);

        StartCoroutine(PostUsername("https://vesta.uclan.ac.uk/~diqbal/UnityScripts/getSaveData.php", jsonData));
    }

    public void OpenMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
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
