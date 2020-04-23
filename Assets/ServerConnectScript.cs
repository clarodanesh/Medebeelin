using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ServerConnectScript : MonoBehaviour
{
    
    //CONNECTS TO THE SERVER ONLY USED TO DEMONSTRATE CONNECTIVITY

    string uname;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(PostRequest("https://vesta.uclan.ac.uk/~diqbal/UnityLab/scores.php"));
        uname = PlayerPrefs.GetString("username");
    }

    IEnumerator PostRequest(string uri)
    {
        WWWForm form = new WWWForm();
        form.AddField("name", "Danesh");

        UnityWebRequest uwr = UnityWebRequest.Post(uri, form);
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error while sending: " + uwr.error);
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("uname" + uname);
    }
}
