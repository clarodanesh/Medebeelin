using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    //public variables set in the inspector
    public ParticleSystem nectarHitParticles;
    private int direction;
    public static int health;
    public static int speed;
    public bool isGrounded;
    public Rigidbody2D playerRigidBody;
    public Animator animator;
    //animator controllers used for the animation states
    public RuntimeAnimatorController rightAnim;
    public RuntimeAnimatorController leftAnim;
    public RuntimeAnimatorController stoppedRight;
    public RuntimeAnimatorController stoppedLeft;
    public RuntimeAnimatorController upRightAnim;
    public RuntimeAnimatorController upLeftAnim;
    public RuntimeAnimatorController upStoppedRight;
    public RuntimeAnimatorController upStoppedLeft;

    //if the player hits the enemy then the enemy needs to go the opposite way
    //fixes bug where enemy keeps hitting player and takes too much health away
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            if (EnemyScript.pos == "left")
            {
                EnemyScript.pos = "right";
            }
            else if(EnemyScript.pos == "right")
            {
                EnemyScript.pos = "left";
            }
        }
        if (collision.collider.tag == "EnemyBody")
        {
            nectarHitParticles.Play();
            if (EnemyScript.pos == "left")
            {
                EnemyScript.pos = "right";
            }
            else if (EnemyScript.pos == "right")
            {
                EnemyScript.pos = "left";
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        direction = 1;
    }

    //remove all of the player health
    void RemovePlayerHealth()
    {
        health = 0;
        LevelManager.score = 0;
    }

    //need to keep checking if player is alive or dead
    void CheckPlayerHealth()
    {
        if(health <= 0)
        {
            RemovePlayerHealth();

            Destroy(gameObject);
            SceneManager.LoadScene("GameOver");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Using animation controller to change animation during runtime
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            //set the direction to 1 means going right
            direction = 1;
            //move the player
            playerRigidBody.velocity = gameObject.transform.right * speed;
            //sets the skin for the player
            if (LevelManager.spriteType == "normal")
            {
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
            //check if the player is grounded before allowing to jump again
            //players only allowed one jump stops them from floating away
            if (isGrounded)
            {
                if (LevelManager.level == "Level1")
                {
                    playerRigidBody.velocity = gameObject.transform.up * 40;
                }
                else if(LevelManager.level == "Level2")
                {
                    playerRigidBody.velocity = gameObject.transform.up * 50;
                }
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
                //plays the jump sound
                PlayerAudioManagerScript.playerAudioSource.Play();
            }
        }
        if(Input.anyKey == false)
        {
            //if no keys down just set the animation to still
            Debug.Log("NO KEYS DOWN");
            if(direction == 1)
            {
                if (LevelManager.spriteType == "normal")
                {
                    animator.runtimeAnimatorController = stoppedRight as RuntimeAnimatorController;
                }
                else if (LevelManager.spriteType == "upgraded")
                {
                    animator.runtimeAnimatorController = upStoppedRight as RuntimeAnimatorController;
                }
            }
            if(direction == 2)
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

        //check the player health to see if they are dead yet
        CheckPlayerHealth();
    }
}
