using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BossLevelPlayerScript : MonoBehaviour
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

    public GameObject playerBullet;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Enemy")
        {
            if (EnemyScript.pos == "left")
            {
                EnemyScript.pos = "right";
            }
            else if (EnemyScript.pos == "right")
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
        if (health <= 0)
        {
            RemovePlayerHealth();
            Destroy(gameObject);
            SceneManager.LoadScene("GameOver");
        }
    }

    void SpawnBullet(float x, float y, float z)
    {
        Debug.Log("SPAWNING A BULLET " + gameObject.transform.position.x);
        GameObject go = Instantiate(playerBullet);
        float randX = Random.Range(-10f, 10f);
        float randY = Random.Range(-10f, 10f);
        //go.transform.position = new Vector3(randX, randY, randX);
        go.transform.position = new Vector3(x, y, z);
        //go.transform.GetChild(0).transform.localPosition = new Vector2(-0.1405144f, 0.5059581f);
    }

    /*void AimAtMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 0;
        float angle = Mathf.Atan2(mousePos.y, mousePos.x) * Mathf.Rad2Deg;
        //angle -= 90;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }*/

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
            //PlayerAudioManagerScript.playerAudioSource.Play();
            //isGrounded = false;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
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
            //PlayerAudioManagerScript.playerAudioSource.Play();
            //isGrounded = false;
        }
        if (Input.GetMouseButtonDown(0))
        {
            SpawnBullet(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z);
        }
        if (Input.anyKey == false)
        {
            Debug.Log("NO KEYS DOWN");
            if (direction == 1)
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

        Debug.Log("SPEED" + speed);
        Debug.Log("TRANSFORM RIGHT" + gameObject.transform.right);
        Debug.Log("VELOCITY" + playerRigidBody.velocity);
        //AimAtMouse();
        CheckPlayerHealth();
    }
}
