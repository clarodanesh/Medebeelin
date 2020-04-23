using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    //enemy starts off moving leftward
    static public string pos = "left";
    public Rigidbody2D enemyRigidBody;

    //checking for collisions with the player and destroying enemy when it happens and increasing score
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            LevelManager.score = LevelManager.score + 20;
            //remove the enemy
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        enemyRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //check the direction of the enemy controlled in another script and move accrodingly
        if (pos == "left")
        {
            enemyRigidBody.velocity = -gameObject.transform.right * 1;
        }
        if (pos == "right")
        {
            enemyRigidBody.velocity = gameObject.transform.right * 1;
        }
    }
}
