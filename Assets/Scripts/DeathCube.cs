using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathCube : MonoBehaviour
{
    private Rigidbody2D rb;
    private Vector2 pos;
    private float speed;
    public float t = 0;
    public float amp = 0.25f;
    public float freq = 2;
    public float offset;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pos = transform.position;
    }

    private void Update()
    {
        t += Time.deltaTime;
        offset = amp * Mathf.Cos(t * freq);

        transform.position = pos + new Vector2(0, offset); 
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            rb.isKinematic = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        rb.isKinematic = true;
        // transform.position = pos;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            Debug.Log("Game Over");
        }
    }
}
