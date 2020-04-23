using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class DeleteDataScript : MonoBehaviour
{
    //Serializable object to send to the server as encoding and decoding
    [System.Serializable]
    public class DataToSend
    {
        public string uname;
    }

    //post the users username to delete data
    IEnumerator PostUsername(string url, string json)
    {
        //create a post unity web request
        //made with code from brendans lecture
        //all unity web request are made with help from brendans lectures
        var uwr = new UnityWebRequest(url, "POST");
        //set request header and the json data to send
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
        //using the serializable object to send the data to the server
        DataToSend progressData = new DataToSend();
        progressData.uname = PlayerPrefs.GetString("username"); //only need username to delete the data
        string jsonData = JsonUtility.ToJson(progressData);

        //need to set this data back to default in indexeddb
        PlayerPrefs.SetString("level", "Level1");
        PlayerPrefs.SetInt("upgrade", 0);
        PlayerPrefs.SetString("skin", "normal");
        if (PlayerPrefs.GetInt("isFB") == 1)
        {
            PlayerPrefs.SetInt("health", 150);
            PlayerPrefs.SetInt("score", 50);
        }
        else
        {
            PlayerPrefs.SetInt("health", 100);
            PlayerPrefs.SetInt("score", 0);
        }
        PlayerPrefs.SetInt("speed", 7);
        PlayerPrefs.SetInt("nectarpoints", 1);
        PlayerPrefs.SetInt("bosshealth", 400);

        //send the request to delete the data
        StartCoroutine(PostUsername("https://vesta.uclan.ac.uk/~diqbal/UnityScripts/deleteData.php", jsonData));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
