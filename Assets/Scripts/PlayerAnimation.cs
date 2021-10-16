using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    private PlayerController isAttack;

    private void Awake()
    {
        isAttack = GetComponent<PlayerController>();
    }

    
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetButton("Horizontal") && !isAttack.isAttack)
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isAttack.isAttack)
        {
            anim.SetTrigger("jump");
        }

        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("attack1");
        }

        if (Input.GetMouseButtonDown(1))
        {
            anim.SetTrigger("attack2");
        }
    }
}