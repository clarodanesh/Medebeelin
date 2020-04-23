using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    //rigid body for this object will use to create movement
    Rigidbody2D rb;

    //public variables, bosshealth static as using it in multiple scripts
    public static int bossHealth;

    //gameobjects set using inspector
    public GameObject player;
    public GameObject topLeftTrigger;
    public GameObject topRightTrigger;
    public GameObject bottomLeftTrigger;
    public GameObject bottomRightTrigger;

    //triggers dont need to be public as im getting them in the script
    GameObject selectedTrigger;

    //used for the bullet and missle, public because they are set in the inspector
    public GameObject bullet;
    public GameObject missile;

    //used to delaye the bullets as called in update
    int bulletDelayCounter;

    //used to check number of the trigger hit (4 locations boss can enter)
    int triggerHit;

    //need to know if boss is shooting or moving so can edit behaviour accordingly
    bool shooting;
    bool moving;

    //shooter 1 small stingers, shooter 2 missiles, shooter 3 empty 
    public static int shooter;

    //handling the boss entering one of the 4 zones
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //check the trigger entered by checking tag
        if (collision.tag.Contains("TopLeftTrigger"))
        {
            //select a random shooter 1, 2 or 3 - if it was 1 or 2 will become 3 and if already 3 will become 1 or 2
            ChooseRandomShooter();
            //trigger identifier to check which one was hit
            triggerHit = 1;
            //turn the enemy to face the player
            FaceObject(player);
            //boss is now shooting
            shooting = true;
            //not moving anymore
            moving = false;
            //if shooter is small stingers or missiles then stay there for 10 seconds otherwise stay there for 5 seconds
            if (shooter == 1 || shooter == 2)
            {
                StartCoroutine(TurnToTrigger(10f));
            }else if(shooter == 3)
            {
                StartCoroutine(TurnToTrigger(5f));
            }
        }
        if (collision.tag.Contains("TopRightTrigger"))
        {
            //SAME AS ABOVE FOR THE REST
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
            //SAME AS ABOVE FOR THE REST
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
            //SAME AS ABOVE FOR THE REST
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
    //Need to pass the delay time as float parameter
    IEnumerator TurnToTrigger(float delay)
    {
        //wait for the amount of time passed as parameter
        yield return new WaitForSeconds(delay);
        //not shooting anymore as boss is going to move now
        shooting = false;
        //boss needs to move to next trigger now so set to true
        moving = true;

        //select a random number between 1 and 4
        System.Random rnd = new System.Random();
        int randomNumber = Random.Range(1, 5);

        //face the random trigger
        FaceObject(TriggerToFace(randomNumber));
    }

    // Start is called before the first frame update
    void Start()
    {
        //get bosshealth from indexed db
        bossHealth = PlayerPrefs.GetInt("bosshealth");
        //face the top right trigger at the start
        FaceObject(topRightTrigger);
        //boss is not shooting and is moving
        shooting = false;
        moving = true;
        //not shooting - will now set randomly to 1 or 2 as alternates between randomly 1 or 2 and then 3
        shooter = 3;
        //bullet delay set to -1 so i can check later
        bulletDelayCounter = -1;
    }

    //returns gameobject of the trigger to face
    GameObject TriggerToFace(int rnd)
    {
        //check the random number and the trigger the boss is already in,
        //must choose a randomly between the 3 other triggers to fix bug where choosing the same trigger would stop the boss
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
            //if none of these select a random number again
            System.Random rand = new System.Random();
            int randomNumber = Random.Range(1, 5);
            return TriggerToFace(randomNumber);
        }
    }

    /*used unity forums to aid with this
     * needed the ability to rotate and face an object
     * https://answers.unity.com/questions/1350050/how-do-i-rotate-a-2d-object-to-face-another-object.html 
    */
    //turns the boss to the object passed as a parameter
    void FaceObject(GameObject objectToFace)
    {
        //make the selected trigger into the object to face
        selectedTrigger = objectToFace;

        //get the position of the target and set the z axis to 0 
        Vector3 target = objectToFace.transform.position;
        target.z = 0f;

        //using the object to face pos and boss pos to get calc an x and y for the target
        Vector3 objectPosition = transform.position;
        target.x = target.x - objectPosition.x;
        target.y = target.y - objectPosition.y;

        //rotate by the calculated angle
        float angle = Mathf.Atan2(target.y, target.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    //Chooses a random shooter type 1 and 2 are prefabs of stingers and missiles 3 is empty
    //called as soon as boss faces the player
    void ChooseRandomShooter()
    {
        //get a random number either 1 or 2
        System.Random rand = new System.Random();
        int randomNumber = Random.Range(1, 3);

        //this check is to alternate between shooting and non shooting state
        if (shooter == 3)
        {
            if (randomNumber == 1) //1 is small stingers
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

    //Will spawn the type of prefab for the shooter type
    void Shoot()
    {
        if(shooter == 1)
        {
            //spawn a small stinger or bullet for easier reference
            SpawnBullet(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        }
        else if(shooter == 2)
        {
            //spawn a bigger missile prefab
            SpawnMissile(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        }
        else if(shooter == 3)
        {
            //if shooter is 3 do nothing
            //better to not even add this but just added for better readability for assignment
        }
    }

    //spawns a small stinger/bullet at the given x y z pos
    void SpawnBullet(float x, float y, float z)
    {
        //spawn the bullet/small stinger
        GameObject smallStinger = Instantiate(bullet);
        smallStinger.transform.position = new Vector3(x, y, z);
    }

    //spawns a missile at the given x y z pos
    void SpawnMissile(float x, float y, float z)
    {
        //spawn the missile
        GameObject go = Instantiate(missile);
        go.transform.position = new Vector3(x, y, z);
    }

    //need to keep checking the health in update to check if boss is dead
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
        //if the boss is shooting
        if (shooting)
        {
            //shooter is 1 then shoot bullets around every 30 frames
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
                //shooter is 2 then shoot missiles around every 90 frames
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

            //rotates the boss to look at enemy every frame
            FaceObject(player);
        }else if (moving)
        {
            //will move until told that its shooting
            FaceObject(selectedTrigger);
            transform.position += transform.right * 10 * Time.deltaTime;
        }

        //need to check health to see when it falls to 0 or below
        CheckBossHealth();
    }
}
