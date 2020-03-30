using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInstructionScript : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Contains("Player"))
        {
            LevelManager.showInstruction = true;
            LevelManager.instructionTextValue = "Jump on the head of an enemy to destroy them and collect more points.";
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
