using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public Rigidbody2D playerRigidBody;
    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            playerRigidBody.velocity = gameObject.transform.right * 10;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            playerRigidBody.velocity = -gameObject.transform.right * 10;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            playerRigidBody.velocity = gameObject.transform.up * 10;
        }
    }
}
