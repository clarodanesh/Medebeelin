using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public static int health;
    public bool isGrounded;
    public Rigidbody2D playerRigidBody;
    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
    }

    void CheckPlayerHealth()
    {
        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            playerRigidBody.velocity = gameObject.transform.right * 7;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            playerRigidBody.velocity = -gameObject.transform.right * 7;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (isGrounded)
            {
                playerRigidBody.velocity = gameObject.transform.up * 40;
                isGrounded = false;
            }
        }

        CheckPlayerHealth();
    }
}
