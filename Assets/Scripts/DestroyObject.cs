using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

public class DestroyObject : MonoBehaviour
{
    private bool isShaking = false;
    private float shake = .05f;
    private Vector2 pos;

    [SerializeField] private int health = 2;

    // [SerializeField] public object destructable;
    [SerializeField] public UnityEngine.Object desctr_chest;
    [SerializeField] public UnityEngine.Object coins = null;
    
    void Start()
    {
        pos = transform.position;
    }
    
    void Update()
    {
        if (isShaking == true)
        {
            transform.position = pos + UnityEngine.Random.insideUnitCircle * shake;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            isShaking = true;
            health--;

            if (health <= 0)
            {
                ExplodeTheObject();
            }
            Invoke("StopShaking", .5f);
            
        }
        else if (collision.CompareTag("BigWeapon"))
        {
            // isShaking = true;
            health--;
            health--;

            if (health <= 0)
            {
                Invoke("ExplodeTheObject", .5f);
            }
            Invoke("StopShaking", .5f);
        }
    }

    void StopShaking()
    {
        isShaking = false;
        transform.position = pos;
    }

    void ExplodeTheObject()
    {
        GameObject destruct = (GameObject)Instantiate(desctr_chest);
        destruct.transform.position = transform.position;
        
        GameObject coin = (GameObject)Instantiate(coins);
        coin.transform.position = transform.position;
        
        Destroy(gameObject);
    }
}
