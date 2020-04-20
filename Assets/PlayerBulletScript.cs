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
                Boss.bossHealth -= 1;
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

    void FaceBoss(GameObject otf)
    {
        Vector3 targ = otf.transform.position;
        targ.z = 0f;

        Vector3 objectPos = transform.position;
        targ.x = targ.x - objectPos.x;
        targ.y = targ.y - objectPos.y;

        float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

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
