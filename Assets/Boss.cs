using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    Rigidbody2D rb;
    public static int bossHealth;
    public GameObject player;
    public GameObject topLeftTrigger;
    public GameObject topRightTrigger;
    public GameObject bottomLeftTrigger;
    public GameObject bottomRightTrigger;
    

    GameObject selectedTrigger;

    public GameObject bullet;
    public GameObject missile;


    int bulletDelayCounter;

    int triggerHit;

    bool shooting;
    bool moving;
    public static int shooter;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //WANT TO DELETE THE GAMEOBJECT HERE AND UPDATE THE SCORE
        if (collision.tag.Contains("TopLeftTrigger"))
        {
            ChooseRandomShooter();
            triggerHit = 1;
            FaceObject(player);
            shooting = true;
            moving = false;
            if (shooter == 1 || shooter == 2)
            {
                StartCoroutine(TurnToTrigger(10f));
            }else if(shooter == 3)
            {
                StartCoroutine(TurnToTrigger(5f));
            }
            //after boss has shot the projectiles it moves to the next object first by facing it
            //FaceObject(topRightTrigger);
        }
        if (collision.tag.Contains("TopRightTrigger"))
        {
            ChooseRandomShooter();
            triggerHit = 2;
            FaceObject(player);
            shooting = true;
            moving = false;
            if (shooter == 1 || shooter == 2)
            {
                StartCoroutine(TurnToTrigger(10f));
            }
            else if (shooter == 3)
            {
                StartCoroutine(TurnToTrigger(5f));
            }
        }
        if (collision.tag.Contains("BottomLeftTrigger"))
        {
            ChooseRandomShooter();
            triggerHit = 3;
            FaceObject(player);
            shooting = true;
            moving = false;
            if (shooter == 1 || shooter == 2)
            {
                StartCoroutine(TurnToTrigger(10f));
            }
            else if (shooter == 3)
            {
                StartCoroutine(TurnToTrigger(5f));
            }
        }
        if (collision.tag.Contains("BottomRightTrigger"))
        {
            ChooseRandomShooter();
            triggerHit = 4;
            FaceObject(player);
            shooting = true;
            moving = false;
            if (shooter == 1 || shooter == 2)
            {
                StartCoroutine(TurnToTrigger(10f));
            }
            else if (shooter == 3)
            {
                StartCoroutine(TurnToTrigger(5f));
            }
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
        bossHealth = 400;

        FaceObject(topRightTrigger);


        shooting = false;
        moving = true;
        shooter = 3;

        bulletDelayCounter = -1;
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

    //called as soon as boss faces the player
    void ChooseRandomShooter()
    {
        System.Random rand = new System.Random();
        int randomNumber = Random.Range(1, 3);

        if (shooter == 3)
        {
            if (randomNumber == 1) //1 is bullets
            {
                shooter = 1;
            }
            else if (randomNumber == 2) //2 is missile
            {
                shooter = 2;
            }
            else
            {
                shooter = 1;
            }
        }
        else if(shooter == 1 || shooter == 2)
        {
            shooter = 3;
        }
        
    }

    /*IEnumerator DelayBullet(float delay)
    {
        yield return new WaitForSeconds(delay);
        Shoot();
    }*/

    void Shoot()
    {
        if(shooter == 1)
        {
            SpawnBullet(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        }
        else if(shooter == 2)
        {
            SpawnMissile(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        }
        else if(shooter == 3)
        {

        }
    }

    void SpawnBullet(float x, float y, float z)
    {
        Debug.Log("SPAWNING A BULLET " + gameObject.transform.position.x);
        GameObject go = Instantiate(bullet);
        float randX = Random.Range(-10f, 10f);
        float randY = Random.Range(-10f, 10f);
        //go.transform.position = new Vector3(randX, randY, randX);
        go.transform.position = new Vector3(x, y, z);
        //go.transform.GetChild(0).transform.localPosition = new Vector2(-0.1405144f, 0.5059581f);
    }

    void SpawnMissile(float x, float y, float z)
    {
        Debug.Log("SPAWNING A MISSILE " + gameObject.transform.position.x);
        GameObject go = Instantiate(missile);
        float randX = Random.Range(-10f, 10f);
        float randY = Random.Range(-10f, 10f);
        //go.transform.position = new Vector3(randX, randY, randX);
        go.transform.position = new Vector3(x, y, z);
        //go.transform.GetChild(0).transform.localPosition = new Vector2(-0.1405144f, 0.5059581f);
    }

    void CheckBossHealth()
    {
        if (bossHealth <= 0)
        {
            Destroy(gameObject);
            SceneManager.LoadScene("GameFinished");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (shooting)
        {

            //StartCoroutine(DelayBullet(3f));
            //Shoot();
            //follows the player if done in update
            if (shooter == 1)
            {
                if (bulletDelayCounter == -1)
                {
                    Shoot();
                }
                else if (bulletDelayCounter == 30)
                {
                    Shoot();
                    bulletDelayCounter = 0;
                }
                else if (bulletDelayCounter > 30)
                {
                    Shoot();
                    bulletDelayCounter = 0;
                }

                bulletDelayCounter += 1;
            }
            else if(shooter == 2)
            {
                if (bulletDelayCounter == -1)
                {
                    Shoot();
                }
                else if (bulletDelayCounter == 90)
                {
                    Shoot();
                    bulletDelayCounter = 0;
                }
                else if (bulletDelayCounter > 90)
                {
                    Shoot();
                    bulletDelayCounter = 0;
                }

                bulletDelayCounter += 1;
            }

            FaceObject(player);
        }else if (moving)
        {
            //will move until told that its shooting
            FaceObject(selectedTrigger);
            transform.position += transform.right * 10 * Time.deltaTime;
        }

        CheckBossHealth();
    }
}
