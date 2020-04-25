using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Facebook.Unity;

public class Login : MonoBehaviour
{
    //holds the input fields for the username and password
    public InputField unameIF;
    public InputField passIF;

    public Text error;

    //need to make a serializab;e object to use for the login
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

    //do the user login with the input details
    public void UserLogin()
    {
        DataToSend userData = new DataToSend();
        //get the text and set it into variables
        userData.uname = unameIF.text;
        userData.pass = passIF.text;
        
        //details need to be filled
        if (userData.uname == "" || userData.pass == "")
        {
            error.text = "Dont leave the inputs blank";
        }
        else
        {
            string jsonData = JsonUtility.ToJson(userData);

            //send the login request to the server
            StartCoroutine(PostRequestJSON("https://vesta.uclan.ac.uk/~diqbal/UnityScripts/login.php", jsonData));
        }
    }

    //used to post the request and handle it
    IEnumerator PostRequestJSON(string url, string json)
    {
        //setup web request
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
            //if user exists display to screen
            if(uwr.downloadHandler.text == "user already exists")
            {
                error.text = "Select a unique username or Enter the correct password";
            }
            else if(uwr.downloadHandler.text == "done")
            {
                //if user was added then open game
                SceneManager.LoadScene("MainMenu");
                PlayerPrefs.SetString("username", unameIF.text);
                PlayerPrefs.SetInt("isFB", 0);
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

        }
        else
        {

        }
    }

    //try to login with facebook
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
        //add the user to the db too using their email
        PlayerInfo playerInfo = JsonUtility.FromJson<PlayerInfo>(result.RawResult);
        DataToSend userData = new DataToSend();
        userData.uname = playerInfo.email;
        userData.pass = "facebookLogin";

        string jsonData = JsonUtility.ToJson(userData);

        StartCoroutine(PostRequestJSONFB("https://vesta.uclan.ac.uk/~diqbal/UnityScripts/login.php", jsonData, userData.uname));

    }

    //playerinfo object contains the json result
    public class PlayerInfo
    {
        public string name;
        public string email;
        public string id;
    }

    //post request using fb
    IEnumerator PostRequestJSONFB(string url, string json, string email)
    {
        //setup web request
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
            if (uwr.downloadHandler.text == "user already exists")
            {
                error.text = "Select a unique username or Enter the correct password";
            }
            else if (uwr.downloadHandler.text == "done")
            {
                //login was success so load mein menu
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
