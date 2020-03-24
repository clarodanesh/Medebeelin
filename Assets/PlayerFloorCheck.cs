using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFloorCheck : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        transform.parent.GetComponent<PlayerScript>().isGrounded = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
