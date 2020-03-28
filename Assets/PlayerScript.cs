using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    public static int health;
    public bool isGrounded;
    public Rigidbody2D playerRigidBody;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            if (EnemyScript.pos == "left")
            {
                EnemyScript.pos = "right";
            }
            else if(EnemyScript.pos == "right")
            {
                EnemyScript.pos = "left";
            }
        }
    }
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
            SceneManager.LoadScene("GameOver");
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
                //isGrounded = false;
            }
        }

        CheckPlayerHealth();
    }
}
