using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GetHighScores : MonoBehaviour
{
    public GameObject highScoreText;

    [System.Serializable]
    public class DataTable
    {
        public List<DataRetrieved> hsData;
    }

    [System.Serializable]
    public class DataRetrieved
    {
        public int score;
        public string username;
    }

    IEnumerator PostRequestJSON(string url/*, string json*/)
    {
        var uwr = new UnityWebRequest(url, "POST");
        //byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        //uwr.uploadHandler = (UploadHandler)new UploadHandlerRaw(jsonToSend);
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
            DataTable hsd = JsonUtility.FromJson<DataTable>(uwr.downloadHandler.text);
            Debug.Log(hsd);
            Debug.Log("----------------------");
            Debug.Log(hsd.hsData[0].username);
            Debug.Log(hsd.hsData[0].score);
            Debug.Log("----------------------");

            highScoreText.GetComponent<Text>().text = "1: " + hsd.hsData[0].username + "\t\t" + hsd.hsData[0].score + "\n" + "2: " + hsd.hsData[1].username + "\t\t" + hsd.hsData[1].score + "\n" + "3: " + hsd.hsData[2].username + "\t\t" + hsd.hsData[2].score + "\n" + "4: " + hsd.hsData[3].username + "\t\t" + hsd.hsData[3].score + "\n" + "5: " + hsd.hsData[4].username + "\t\t" + hsd.hsData[4].score + "\n";
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PostRequestJSON("https://vesta.uclan.ac.uk/~diqbal/UnityScripts/getHS.php"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
