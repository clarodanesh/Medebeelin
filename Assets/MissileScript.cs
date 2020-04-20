using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileScript : MonoBehaviour
{
    public GameObject player;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Contains("Player"))
        {
            BossLevelPlayerScript.health -= 13;
            DeathAudioManager.deathAudioSource.Play();
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("pablo-spritesheet_0");
        FacePlayer(player);
        StartCoroutine(DelaySelfDestruct(4f));
    }

    void FacePlayer(GameObject otf)
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
        FacePlayer(player);
        transform.position += transform.right * 8 * Time.deltaTime;
    }
}
