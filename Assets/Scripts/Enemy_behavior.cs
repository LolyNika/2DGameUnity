using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_behavior : MonoBehaviour
{
    #region Public Variables
    public float attackDisnace; // Minimum distance for attack
    public float moveSpeed;
    public float timer; //Timer for cooldown between attacks
    public Transform target;
    public Transform leftLimit;
    [HideInInspector] public Transform rightlimit;
    [HideInInspector] public bool inRange; //Check if player is in range
    public GameObject hotZone;
    public GameObject triggerArea;
    private Animator anim;
    #endregion

    #region Private Variables
    private float distance; //Store for distanc b/w enemy and player
    
    private bool attackMode;
    private bool cooling; //Checl if enemy is cooling after attack
    
    private float intTimer;
    #endregion

    private void Awake()
    {
        SelectTarget();
        intTimer = timer; //Store the initial value of timer
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!attackMode)
        {
            Move();
        }

        if (!InsideofLimits() && !inRange && !anim.GetCurrentAnimatorStateInfo(0).IsName("Enemy_attack"))
        {
            SelectTarget();
        }
        
        if (inRange)
        {
            EnemyLogic();
        }
    }

    void EnemyLogic()
    {
        distance = Vector2.Distance(transform.position, target.position);

        if (distance > attackDisnace) 
        {
            Move();
            StopAttack();
        } else if (attackDisnace >= distance && cooling == false)
        {
            Attack();
        }

        if (cooling )
        {
            Cooldown();
            anim.SetBool("Attack", false);
        }
    }

    void Move()
    {
        anim.SetBool("canWalk", true);
        
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Enemy_attack"))
        {
            Vector2 targetPosition = new Vector2(target.position.x, transform.position.y);

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        }
    }

    void Attack()
    {
        timer = intTimer; // Reset timer when player enter attack renge
        attackMode = true; // To check if enemy is can still attack or not 

        anim.SetBool("canWalk", false);
        anim.SetBool("Attack", true);
    }

    void Cooldown()
    {
        timer -= Time.deltaTime;

        if (timer <= 0 && cooling && attackMode)
        {
            cooling = false;
            timer = intTimer;
        }
    }
    
    void StopAttack()
    {
        cooling = false;
        attackMode = false;
        anim.SetBool("Attack", false);
        
    }

    public void TriggerCooling()
    {
        cooling = true;
        
    }

    private bool InsideofLimits()
    {
        return transform.position.x > leftLimit.position.x && transform.position.x < rightlimit.position.x;
    }

    public void SelectTarget()
    {
        float distanceToLeft = Vector2.Distance(transform.position, leftLimit.position);
        float distanceToRight = Vector2.Distance(transform.position, rightlimit.position);

        if (distanceToLeft > distanceToRight)
        {
            target = leftLimit;
            
        }
        else
        {
            target = rightlimit;
        }

        Flip();
    }

    public void Flip()
    {
        Vector3 rotation = transform.eulerAngles;
        if (transform.position.x > target.position.x)
        {
            rotation.y = 180f;
        }
        else
        {
            rotation.y = 0f; 
        }

        transform.eulerAngles = rotation;
    }
}
