using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    public ParticleSystem nectarHitParticles;
    private int direction;
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

    void RemovePlayerHealth()
    {
        health = 0;
        LevelManager.score = 0;
    }

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
        if (Input.GetKey(KeyCode.RightArrow))
        {
            direction = 1;
            playerRigidBody.velocity = gameObject.transform.right * speed;
            if (LevelManager.spriteType == "normal")
            {
                animator.runtimeAnimatorController = rightAnim as RuntimeAnimatorController;
            }
            else if (LevelManager.spriteType == "upgraded")
            {
                animator.runtimeAnimatorController = upRightAnim as RuntimeAnimatorController;
            }
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
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
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (isGrounded)
            {
                playerRigidBody.velocity = gameObject.transform.up * 40;
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
                PlayerAudioManagerScript.playerAudioSource.Play();
                //isGrounded = false;
            }
        }
        if(Input.anyKey == false)
        {
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

        CheckPlayerHealth();
    }
}
