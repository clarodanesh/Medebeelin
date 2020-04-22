using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FinishGame : MonoBehaviour
{
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
            Debug.Log("Player Received: " + uwr.downloadHandler.text);
            Debug.Log(uwr.downloadHandler.text);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        DataToSend progressData = new DataToSend();
        progressData.uname = PlayerPrefs.GetString("username");
        string jsonData = JsonUtility.ToJson(progressData);

        StartCoroutine(PostUsername("https://vesta.uclan.ac.uk/~diqbal/UnityScripts/deleteData.php", jsonData));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
