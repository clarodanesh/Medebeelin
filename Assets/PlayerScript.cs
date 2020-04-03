using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    public ParticleSystem nectarHitParticles;
    private int direction;
    public static int health;
    public bool isGrounded;
    public Rigidbody2D playerRigidBody;
    public Animator animator;
    public RuntimeAnimatorController rightAnim;
    public RuntimeAnimatorController leftAnim;
    public RuntimeAnimatorController stoppedRight;
    public RuntimeAnimatorController stoppedLeft;
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

    void CheckPlayerHealth()
    {
        if(health <= 0)
        {
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
            playerRigidBody.velocity = gameObject.transform.right * 7;
            animator.runtimeAnimatorController = rightAnim as RuntimeAnimatorController;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            direction = 2;
            playerRigidBody.velocity = -gameObject.transform.right * 7;
            animator.runtimeAnimatorController = leftAnim as RuntimeAnimatorController;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            if (isGrounded)
            {
                playerRigidBody.velocity = gameObject.transform.up * 40;
                if (direction == 1)
                {
                    animator.runtimeAnimatorController = rightAnim as RuntimeAnimatorController;
                }
                if (direction == 2)
                {
                    animator.runtimeAnimatorController = leftAnim as RuntimeAnimatorController;
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
                animator.runtimeAnimatorController = stoppedRight as RuntimeAnimatorController;
            }
            if(direction == 2)
            {
                animator.runtimeAnimatorController = stoppedLeft as RuntimeAnimatorController;
            }
        }

        CheckPlayerHealth();
    }
}
