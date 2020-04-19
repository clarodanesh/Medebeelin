using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    Rigidbody2D rb;
    public GameObject player;
    public GameObject topLeftTrigger;
    public GameObject topRightTrigger;
    public GameObject bottomLeftTrigger;
    public GameObject bottomRightTrigger;

    GameObject selectedTrigger;

    int triggerHit;

    bool shooting;
    bool moving;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //WANT TO DELETE THE GAMEOBJECT HERE AND UPDATE THE SCORE
        if (collision.tag.Contains("TopLeftTrigger"))
        {
            triggerHit = 1;
            FaceObject(player);
            shooting = true;
            moving = false;
            StartCoroutine(TurnToTrigger(10f));
            //after boss has shot the projectiles it moves to the next object first by facing it
            //FaceObject(topRightTrigger);
        }
        if (collision.tag.Contains("TopRightTrigger"))
        {
            triggerHit = 2;
            FaceObject(player);
            shooting = true;
            moving = false;
            StartCoroutine(TurnToTrigger(10f));
        }
        if (collision.tag.Contains("BottomLeftTrigger"))
        {
            triggerHit = 3;
            FaceObject(player);
            shooting = true;
            moving = false;
            StartCoroutine(TurnToTrigger(10f));
        }
        if (collision.tag.Contains("BottomRightTrigger"))
        {
            triggerHit = 4;
            FaceObject(player);
            shooting = true;
            moving = false;
            StartCoroutine(TurnToTrigger(10f));
        }
    }

    /*
        I have been using Invoke as a method to delay things in the game
        Using Invoke I cant pass parameters to the function that I call in Invoke
        so I needed a solution that would allow me to delay rotating the enemy ai and pass 
        parameters to the function I was calling
        
        I created a solution with aid from the unity forums
        https://answers.unity.com/questions/788124/invoking-a-method-that-takes-variables.html 
    */
    IEnumerator TurnToTrigger(float delay)
    {
        yield return new WaitForSeconds(delay);
        shooting = false;
        moving = true;
        System.Random rnd = new System.Random();
        int randomNumber = Random.Range(1, 5);

        FaceObject(TriggerToFace(randomNumber));
    }

    // Start is called before the first frame update
    void Start()
    {
        /*rb = GetComponent<Rigidbody2D>();

        Vector3 targ = player.transform.position;
        targ.z = 0f;

        Vector3 objectPos = transform.position;
        targ.x = targ.x - objectPos.x;
        targ.y = targ.y - objectPos.y;

        float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));*/
        FaceObject(topRightTrigger);


        shooting = false;
        moving = true;
    }

    GameObject TriggerToFace(int rnd)
    {
        if (rnd == 1 && triggerHit != 1)
        {
            return topLeftTrigger;
        }
        else if(rnd == 2 && triggerHit != 2)
        {
            return topRightTrigger;
        }
        else if (rnd == 3 && triggerHit != 3)
        {
            return bottomLeftTrigger;
        }
        else if (rnd == 4 && triggerHit != 4)
        {
            return bottomRightTrigger;
        }
        else
        {
            System.Random rand = new System.Random();
            int randomNumber = Random.Range(1, 5);
            return TriggerToFace(randomNumber);
        }
    }

    void FaceObject(GameObject otf)
    {
        selectedTrigger = otf;

        Vector3 targ = otf.transform.position;
        targ.z = 0f;

        Vector3 objectPos = transform.position;
        targ.x = targ.x - objectPos.x;
        targ.y = targ.y - objectPos.y;

        float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    // Update is called once per frame
    void Update()
    {
        /*Vector3 targ = player.transform.position;
        targ.z = 0f;

        Vector3 objectPos = transform.position;
        targ.x = targ.x - objectPos.x;
        targ.y = targ.y - objectPos.y;

        float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));*/

        //transform.position += transform.right * 2 * Time.deltaTime;
        if (shooting)
        {
            //follows the player if done in update
            FaceObject(player);
        }else if (moving)
        {
            //will move until told that its shooting
            FaceObject(selectedTrigger);
            transform.position += transform.right * 10 * Time.deltaTime;
        }
    }
}
