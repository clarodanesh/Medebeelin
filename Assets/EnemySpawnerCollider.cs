using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerCollider : MonoBehaviour
{
    public GameObject bee;
    System.Random rnd = new System.Random();
    private bool firstSpawned, secondSpawned, thirdSpawned;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Contains("Player"))
        {
            Debug.Log("hittin gthe player should show");
            Debug.Log(" " + gameObject.transform.position.x);
            Debug.Log(firstSpawned);
            if (gameObject.transform.position.x == 97.47f && firstSpawned == false)
            {
                Debug.Log("inside first");
                SpawnEnemy(98.57f, 0.699f, 0f);
                firstSpawned = true;
            }
            else if(gameObject.transform.position.x == 115.967f && secondSpawned == false)
            {
                Debug.Log("inside second");
                SpawnEnemy(117.53f, 0.62f, 0f);
                secondSpawned = true;
            }
            else if (gameObject.transform.position.x == 142.46f && thirdSpawned == false)
            {
                Debug.Log("inside third");
                SpawnEnemy(146.051f, -1.493f, 0f);
                thirdSpawned = true;
            }
        }
    }

    void SpawnEnemy(float x, float y, float z)
    {
        Debug.Log("XP" + gameObject.transform.position.x);
        GameObject go = Instantiate(bee);
        float randX = Random.Range(-10f, 10f);
        float randY = Random.Range(-10f, 10f);
        //go.transform.position = new Vector3(randX, randY, randX);
        go.transform.position = new Vector3(x, y, z);
        go.transform.GetChild(0).transform.localPosition = new Vector2(-0.1405144f, 0.5059581f);
    }

    // Start is called before the first frame update
    void Start()
    {
        firstSpawned = false;
        secondSpawned = false;
        thirdSpawned = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
