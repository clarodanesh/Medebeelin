using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    //set the gameobject to the player -- set in the inspector
    public GameObject player;

    //using trigger to check if the small stinger/bullet has hit the player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Contains("Player"))
        {
            //if hit the player then take away health and play the hit/death sound
            BossLevelPlayerScript.health -= 5;
            DeathAudioManager.deathAudioSource.Play();
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //get the gameobject by name
        player = GameObject.Find("pablo-spritesheet_0");
        //face the player on start only as the small stinger/bullets should not follow the player
        FacePlayer(player);
        //these are spawned prefabs so may cause performance issue if they stay in the game without hitting the player so destroy them after 10 seconds
        //safe to say small stinger is out of bounds by then
        StartCoroutine(DelaySelfDestruct(10f));
    }

    /*used unity forums to aid with this
     * needed the ability to rotate and face an object
     * https://answers.unity.com/questions/1350050/how-do-i-rotate-a-2d-object-to-face-another-object.html 
    */
    //this will make the small stinger/bullet face the player
    void FacePlayer(GameObject objecttToFace)
    {
        //get the position of the target and set z axis to 0
        Vector3 target = objecttToFace.transform.position;
        target.z = 0f;

        //get this objects (bullets) position and set the x and y
        Vector3 objectPosition = transform.position;
        target.x = target.x - objectPosition.x;
        target.y = target.y - objectPosition.y;

        //rotate the bullet by the angle calculated
        float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    //this will create a delay then destroy the bullet
    IEnumerator DelaySelfDestruct(float delay)
    {
        //wait for the passed amount of time
        yield return new WaitForSeconds(delay);
        //destroy this gameobject (bullet/stinger)
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //shoot the bullet/stinger in the direction of the player and keep going even past the player
        transform.position += transform.right * 15 * Time.deltaTime;
    }
}
