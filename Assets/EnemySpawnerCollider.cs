using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerCollider : MonoBehaviour
{
    public GameObject bee;
    System.Random rnd = new System.Random();

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Contains("Player"))
        {
            /*GameObject go = Instantiate(bee);
            float randX = Random.Range(-10f, 10f);
            float randY = Random.Range(-10f, 10f);
            //go.transform.position = new Vector3(randX, randY, randX);
            go.transform.position = new Vector3(-0.72f, -3.2f, 0f);*/
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
