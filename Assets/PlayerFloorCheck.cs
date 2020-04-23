using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFloorCheck : MonoBehaviour
{
    //check if the player is touching the ground
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (transform.parent.GetComponent<PlayerScript>().isGrounded == false)
        {
            transform.parent.GetComponent<PlayerScript>().isGrounded = true;
        }
    }

    //check if player is not touching the ground
    private void OnCollisionExit2D(Collision2D collision)
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
