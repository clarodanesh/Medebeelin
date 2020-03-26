using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static int score;
    public static string level;
    // Start is called before the first frame update
    void Start()
    {
        level = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("current score " + score);
        Debug.Log("current level " + level);
    }
}
