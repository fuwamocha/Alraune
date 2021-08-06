using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStop : MonoBehaviour
{
    Rigidbody2D rb;

    Vector3 cal;

    private string portalTag = "Portal";

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == portalTag)
        {
            GetComponent<PlayerManager>().enabled = false;
            GetComponent<Animator>().enabled = false;
            gameObject.transform.localScale = (-cal);
            rb.velocity = Vector2.zero;
        }
    }
}
