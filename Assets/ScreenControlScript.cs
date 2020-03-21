using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScreenControlScript : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Level1");
    }

    public void OpenInstructions()
    {
        SceneManager.LoadScene("Instructions");
    }

    public void OpenMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Start is called before the first frame updates
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
