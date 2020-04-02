using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    static public string pos = "left";
    public Rigidbody2D enemyRigidBody;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            Debug.Log("Hitting");
            LevelManager.score = LevelManager.score + 20;
            //remove the coin
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
