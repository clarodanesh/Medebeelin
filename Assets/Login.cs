using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Facebook.Unity;

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
        if (userData.uname == "" || userData.pass == "")
        {
            error.text = "Dont leave the inputs blank";
        }
        else
        {
            string jsonData = JsonUtility.ToJson(userData);

            StartCoroutine(PostRequestJSON("https://vesta.uclan.ac.uk/~diqbal/UnityScripts/login.php", jsonData));
        }
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


    //FACEBOOK INTEGRATION
    void Awake()
    {
        if (!FB.IsInitialized)
        {
            FB.Init(InitCallback, OnHideUnity);
        }
        else
        {
            FB.ActivateApp();
        }
    }

    private void InitCallback()
    {
        if (FB.IsInitialized)
        {
            FB.ActivateApp();
        }
        else
        {
            Debug.Log("Failed to initialize the facebook sdk");
        }
    }

    private void OnHideUnity(bool isGameShown)
    {
        if (!isGameShown)
        {
            //Time.timescale = 0;
        }
        else
        {
            ///Time.timescale = 1;
        }
    }

    public void FBLoginPressed()
    {
        var perms = new List<string>() { "public_profile", "email" };
        FB.LogInWithReadPermissions(perms, AuthCallback);
    }

    private void AuthCallback(ILoginResult result)
    {
        if (FB.IsLoggedIn)
        {
            var aToken = Facebook.Unity.AccessToken.CurrentAccessToken;
        }
        else
        {
            Debug.Log("User cancelled login");
        }
        FB.API("/me?fields=id,name,email", HttpMethod.GET, DealWithInfoResponse);
    }

    private void DealWithInfoResponse(IGraphResult result)
    {
        PlayerInfo playerInfo = JsonUtility.FromJson<PlayerInfo>(result.RawResult);
        // nameTextArea.GetComponent<Text>().text = playerInfo.name;
        // emailTextArea.GetComponent<Text>().text = playerInfo.email;
        DataToSend userData = new DataToSend();
        userData.uname = playerInfo.email;
        userData.pass = "facebookLogin";

        string jsonData = JsonUtility.ToJson(userData);

        StartCoroutine(PostRequestJSONFB("https://vesta.uclan.ac.uk/~diqbal/UnityScripts/login.php", jsonData, userData.uname));

    }

    public class PlayerInfo
    {
        public string name;
        public string email;
        public string id;
    }

    IEnumerator PostRequestJSONFB(string url, string json, string email)
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
            if (uwr.downloadHandler.text == "user already exists")
            {
                error.text = "Select a unique username or Enter the correct password";
            }
            else if (uwr.downloadHandler.text == "done")
            {
                SceneManager.LoadScene("MainMenu");
                PlayerPrefs.SetString("username", email);
                PlayerPrefs.SetInt("isFB", 1);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
