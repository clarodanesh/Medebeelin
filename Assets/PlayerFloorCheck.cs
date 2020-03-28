using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFloorCheck : MonoBehaviour
{
    //PascalCase for functions
    //camelCase for variables;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (transform.parent.GetComponent<PlayerScript>().isGrounded == false)
        {
            transform.parent.GetComponent<PlayerScript>().isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (transform.parent.GetComponent<PlayerScript>().isGrounded == true)
        {
            transform.parent.GetComponent<PlayerScript>().isGrounded = false;
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
