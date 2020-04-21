using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    public InputField unameIF;
    public InputField passIF;

    public Text error;

    [System.Serializable]
    public class DataToSend
    {
        public string uname;
        public string pass;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    public void UserLogin()
    {
        DataToSend userData = new DataToSend();
        userData.uname = unameIF.text;
        userData.pass = passIF.text;
        string jsonData = JsonUtility.ToJson(userData);

        StartCoroutine(PostRequestJSON("https://vesta.uclan.ac.uk/~diqbal/UnityScripts/login.php", jsonData));
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
            if(uwr.downloadHandler.text == "user already exists")
            {
                error.text = "Select a unique username or Enter the correct password";
            }
            else if(uwr.downloadHandler.text == "done")
            {
                SceneManager.LoadScene("MainMenu");
                PlayerPrefs.SetString("username", unameIF.text);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
