﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftNodeScript : MonoBehaviour
{
    //left node user for enemy position
    //if enemy hits this then make it go rightward
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Contains("Enemy"))
        {
            EnemyScript.pos = "right";
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
