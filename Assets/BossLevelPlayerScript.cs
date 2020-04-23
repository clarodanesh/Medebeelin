using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossLevelPlayerScript : MonoBehaviour
{

    //public variables particle system, integers, bool, animatorcontrollers, and a gameobject
    //these will be set using the inspector
    public ParticleSystem nectarHitParticles;
    public static int health;
    public static int speed;
    public bool isGrounded;
    public Rigidbody2D playerRigidBody;
    public Animator animator;
    public RuntimeAnimatorController rightAnim;
    public RuntimeAnimatorController leftAnim;
    public RuntimeAnimatorController stoppedRight;
    public RuntimeAnimatorController stoppedLeft;
    public RuntimeAnimatorController upRightAnim;
    public RuntimeAnimatorController upLeftAnim;
    public RuntimeAnimatorController upStoppedRight;
    public RuntimeAnimatorController upStoppedLeft;
    public GameObject playerBullet;

    //the direction is just set here
    private int direction;

    // Start is called before the first frame update
    void Start()
    {
        //set the rigidbody of the player
        playerRigidBody = GetComponent<Rigidbody2D>();
        //will use the animator for the different animation states of the player
        animator = GetComponent<Animator>();
        direction = 1;
    }

    //make player health and score 0 as they are dead
    void RemovePlayerHealth()
    {
        health = 0;
        LevelManager.score = 0;
    }

    //this keeps checking if the player health has dropped to 0 or below
    void CheckPlayerHealth()
    {
        if (health <= 0)
        {
            RemovePlayerHealth();
            Destroy(gameObject);
            SceneManager.LoadScene("GameOver");
        }
    }

    //spawn small stingers/bullets at the players position that will then shoot at the boss
    void SpawnBullet(float x, float y, float z)
    {
        GameObject bullet = Instantiate(playerBullet);
        bullet.transform.position = new Vector3(x, y, z);
    }

    // Update is called once per frame
    void Update()
    {
        //Check they ley being presses, supports arrow and wasd keys
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            direction = 1;
            //move the object in that direction
            playerRigidBody.velocity = gameObject.transform.right * speed;
            //check if the character is upgraded or not and set according sprite
            if (LevelManager.spriteType == "normal")
            {
                //using runtimeAnimatorController to change the spritesheets at runtime
                animator.runtimeAnimatorController = rightAnim as RuntimeAnimatorController;
            }
            else if (LevelManager.spriteType == "upgraded")
            {
                animator.runtimeAnimatorController = upRightAnim as RuntimeAnimatorController;
            }
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            //SAME AS ABOVE
            direction = 2;
            playerRigidBody.velocity = -gameObject.transform.right * speed;
            if (LevelManager.spriteType == "normal")
            {
                animator.runtimeAnimatorController = leftAnim as RuntimeAnimatorController;
            }
            else if (LevelManager.spriteType == "upgraded")
            {
                animator.runtimeAnimatorController = upLeftAnim as RuntimeAnimatorController;
            }
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            //SAME AS ABOVE
            playerRigidBody.velocity = gameObject.transform.up * speed;
            if (direction == 1)
            {
                if (LevelManager.spriteType == "normal")
                {
                    animator.runtimeAnimatorController = rightAnim as RuntimeAnimatorController;
                }
                else if (LevelManager.spriteType == "upgraded")
                {
                    animator.runtimeAnimatorController = upRightAnim as RuntimeAnimatorController;
                }
            }
            if (direction == 2)
            {
                if (LevelManager.spriteType == "normal")
                {
                    animator.runtimeAnimatorController = leftAnim as RuntimeAnimatorController;
                }
                else if (LevelManager.spriteType == "upgraded")
                {
                    animator.runtimeAnimatorController = upLeftAnim as RuntimeAnimatorController;
                }
            }
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            //SAME AS ABOVE
            playerRigidBody.velocity = -gameObject.transform.up * speed;
            if (direction == 1)
            {
                if (LevelManager.spriteType == "normal")
                {
                    animator.runtimeAnimatorController = rightAnim as RuntimeAnimatorController;
                }
                else if (LevelManager.spriteType == "upgraded")
                {
                    animator.runtimeAnimatorController = upRightAnim as RuntimeAnimatorController;
                }
            }
            if (direction == 2)
            {
                if (LevelManager.spriteType == "normal")
                {
                    animator.runtimeAnimatorController = leftAnim as RuntimeAnimatorController;
                }
                else if (LevelManager.spriteType == "upgraded")
                {
                    animator.runtimeAnimatorController = upLeftAnim as RuntimeAnimatorController;
                }
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            //when the mouse left button is clicked then shoot a small stinger/bullet
            SpawnBullet(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        }
        if (Input.anyKey == false)
        {
            //if no keys are pressed then use the still spritesheet 
            if (direction == 1)
            {
                //check the upgrade type and set the sprite accordingly
                if (LevelManager.spriteType == "normal")
                {
                    animator.runtimeAnimatorController = stoppedRight as RuntimeAnimatorController;
                }
                else if (LevelManager.spriteType == "upgraded")
                {
                    animator.runtimeAnimatorController = upStoppedRight as RuntimeAnimatorController;
                }
            }
            if (direction == 2)
            {
                if (LevelManager.spriteType == "normal")
                {
                    animator.runtimeAnimatorController = stoppedLeft as RuntimeAnimatorController;
                }
                else if (LevelManager.spriteType == "upgraded")
                {
                    animator.runtimeAnimatorController = upStoppedLeft as RuntimeAnimatorController;
                }
            }
        }

        //check the player health to see if they are dead
        CheckPlayerHealth();
    }
}
