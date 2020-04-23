using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBulletScript : MonoBehaviour
{
    public GameObject boss;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Contains("Boss"))
        {
            if (Boss.shooter == 1 || Boss.shooter == 2) {
                
                
            }else if (Boss.shooter == 3)
            {
                Boss.bossHealth -= 3;
            }
            DeathAudioManager.deathAudioSource.Play();
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        boss = GameObject.Find("BossUp_0");
        FaceBoss(boss);
        StartCoroutine(DelaySelfDestruct(10f));
    }

    /*used unity forums to aid with this
     * needed the ability to rotate and face an object
     * https://answers.unity.com/questions/1350050/how-do-i-rotate-a-2d-object-to-face-another-object.html 
    */
    //player bullet needs to aim at the boss
    void FaceBoss(GameObject objectToFace)
    {
        //get the target pos and set its x axis to 0
        Vector3 target = objectToFace.transform.position;
        target.z = 0f;

        //set the object x and y calculated using current obj x and y
        Vector3 objectPosition = transform.position;
        target.x = target.x - objectPosition.x;
        target.y = target.y - objectPosition.y;

        //set the angle of rotation and rotate
        float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    //self destruct as maybe out of bounds
    IEnumerator DelaySelfDestruct(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.right * 15 * Time.deltaTime;
    }
}
