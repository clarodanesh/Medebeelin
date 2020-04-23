using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileScript : MonoBehaviour
{
    public GameObject player;

    //check if the missile has hit the player
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Contains("Player"))
        {
            //if the missile hit the player then take 13 health away and play the audioclip
            BossLevelPlayerScript.health -= 13;
            DeathAudioManager.deathAudioSource.Play();
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        //get the gameobject by name
        player = GameObject.Find("pablo-spritesheet_0");
        FacePlayer(player);
        //need to self destruct missiles after approx 4 seconds
        StartCoroutine(DelaySelfDestruct(4f));
    }

    /*used unity forums to aid with this
     * needed the ability to rotate and face an object
     * https://answers.unity.com/questions/1350050/how-do-i-rotate-a-2d-object-to-face-another-object.html 
    */
    //need to face the player and not somewher else
    void FacePlayer(GameObject objectToFace)
    {
        //get the pos of the target and set z axis to 0
        Vector3 target = objectToFace.transform.position;
        target.z = 0f;

        //calc x and y for the target based on current obj pos
        Vector3 objectPos = transform.position;
        target.x = target.x - objectPos.x;
        target.y = target.y - objectPos.y;

        //set rotation by calculating an angle
        float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    //self destructs the missile after delay
    IEnumerator DelaySelfDestruct(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        //heat seeking missile effect (always follows the player until hit or self destruct)
        FacePlayer(player);
        //travel to player pos
        transform.position += transform.right * 8 * Time.deltaTime;
    }
}
