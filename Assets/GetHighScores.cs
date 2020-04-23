using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.UI;

public class GetHighScores : MonoBehaviour
{
    //game object to store the high score text object
    public GameObject highScoreText;

    //this will hold the data that is retrieved from the server as a list
    [System.Serializable]
    public class DataTable
    {
        public List<DataRetrieved> hsData;
    }

    //the data retrieved for highscore only using score and username
    [System.Serializable]
    public class DataRetrieved
    {
        public int score;
        public string username;
    }

    //this will post the json request when it is called
    //only need to pass url as it gets the highest scores no specific data required to know
    IEnumerator PostRequestJSON(string url)
    {
        //setting up the request
        var uwr = new UnityWebRequest(url, "POST");
        uwr.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");

        yield return uwr.SendWebRequest();
        if (uwr.isNetworkError)
        {
            Debug.Log("error sending request");
        }
        else
        {

            //display the highscores into the text component
            DataTable hsd = JsonUtility.FromJson<DataTable>(uwr.downloadHandler.text);
            

            highScoreText.GetComponent<Text>().text = "1: " + hsd.hsData[0].username + "\t\t" + hsd.hsData[0].score + "\n" + "2: " + hsd.hsData[1].username + "\t\t" + hsd.hsData[1].score + "\n" + "3: " + hsd.hsData[2].username + "\t\t" + hsd.hsData[2].score + "\n" + "4: " + hsd.hsData[3].username + "\t\t" + hsd.hsData[3].score + "\n" + "5: " + hsd.hsData[4].username + "\t\t" + hsd.hsData[4].score + "\n";
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //post the json request
        StartCoroutine(PostRequestJSON("https://vesta.uclan.ac.uk/~diqbal/UnityScripts/getHS.php"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
