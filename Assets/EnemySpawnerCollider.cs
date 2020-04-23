using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemySpawnerCollider : MonoBehaviour
{
    //will use this to instantiate enemy bees
    public GameObject bee;

    //check if enemies have been spawned before trying to spawn them
    private bool firstSpawned, secondSpawned, thirdSpawned;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //checking if player enters trigger
        if (collision.tag.Contains("Player"))
        {
            //need to check if the scene is level 1 and instantiate bees accordingly
            if (SceneManager.GetActiveScene().name == "Level1")
            {
                if (gameObject.transform.position.x == 97.47f && firstSpawned == false)
                {
                    //spawn the enemy and set the bool to true to show its spawned
                    SpawnEnemy(98.57f, 0.699f, 0f);
                    firstSpawned = true;
                }
                else if (gameObject.transform.position.x == 115.967f && secondSpawned == false)
                {
                    //spawn the enemy and set the bool to true to show its spawned
                    SpawnEnemy(117.53f, 0.62f, 0f);
                    secondSpawned = true;
                }
                else if (gameObject.transform.position.x == 142.46f && thirdSpawned == false)
                {
                    //spawn the enemy and set the bool to true to show its spawned
                    SpawnEnemy(146.051f, -1.493f, 0f);
                    thirdSpawned = true;
                }
            }else if(SceneManager.GetActiveScene().name == "Level2")
            {
                //check if the scene is level 2 as bee positions are different
                if (gameObject.transform.position.x == 67.48f && firstSpawned == false)
                {
                    //spawn the enemy and set the bool to true to show its spawned
                    SpawnEnemy(74.32f, 2.01f, 0f);
                    firstSpawned = true;
                }
                else if (gameObject.transform.position.x == 152.94f && secondSpawned == false)
                {
                    //spawn the enemy and set the bool to true to show its spawned
                    SpawnEnemy(157.588f, -3f, 0f);
                    secondSpawned = true;
                }
                else if (gameObject.transform.position.x == 94.44f && thirdSpawned == false)
                {
                    //spawn the enemy and set the bool to true to show its spawned
                    SpawnEnemy(99.16f, -3f, 0f);
                    thirdSpawned = true;
                }
            }
        }
    }

    //function used to spawn the enemy bee
    void SpawnEnemy(float x, float y, float z)
    {
        GameObject beeEnemy = Instantiate(bee);
        beeEnemy.transform.position = new Vector3(x, y, z);
        beeEnemy.transform.GetChild(0).transform.localPosition = new Vector2(-0.1405144f, 0.5059581f);
    }

    // Start is called before the first frame update
    void Start()
    {
        //nothing has spwned on start
        firstSpawned = false;
        secondSpawned = false;
        thirdSpawned = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
