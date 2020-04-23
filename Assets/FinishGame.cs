using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class FinishGame : MonoBehaviour
{
    //serializable object to send to the server
    [System.Serializable]
    public class DataToSend
    {
        public string uname;
    }

    //will need to post the username to make the request and check if it succeeded
    IEnumerator PostUsername(string url, string json)
    {
        //setting up the web request
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
        //setting the data into the object
        DataToSend progressData = new DataToSend();
        progressData.uname = PlayerPrefs.GetString("username");
        string jsonData = JsonUtility.ToJson(progressData);

        //post the username to the server to delete the data
        StartCoroutine(PostUsername("https://vesta.uclan.ac.uk/~diqbal/UnityScripts/deleteData.php", jsonData));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
