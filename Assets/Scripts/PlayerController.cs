using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject attackHitBox1;
    [SerializeField] private GameObject attackHitBox2;
    private Rigidbody2D rb;
    public float speed;
    public float jumpForce;
    private float moveInput;

    private bool isGrounded = false;
    // private bool facingRight = true;

    // private int extraJumps;

    // private Animator anim;
    // private SpriteRenderer sprite;

    public bool isAttack = false;

    private float HorizontalMove = 0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // sprite = GetComponentInChildren<SpriteRenderer>();
        attackHitBox1.SetActive(false);
        attackHitBox2.SetActive(false);
    }


    private void Update()
    {
        if (Input.GetKey(KeyCode.D) && !isAttack)
        {
            Run();
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (Input.GetKey(KeyCode.A) && !isAttack)
        {
            Run();
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (Input.GetButtonDown("Fire1") && !isAttack)
        {
            isAttack = true;
            StartCoroutine(DoAttack1());
            // Invoke("ResetAttack", .5f); // void ResetAttack() {isAttack = false;} Это "условно" для блокирования ходьбы и ее анимации при атаках и время задержки
        }

        if (isGrounded && Input.GetKeyDown(KeyCode.Space) && !isAttack)
        {
            Jump();
        }

        if (Input.GetButtonDown("Fire2") && !isAttack)
        {
            isAttack = true;
            StartCoroutine(DoAttack2());
            // Invoke("ResetAttack", .8f);
        }

    }

    private void FixedUpdate()
    {
        CheckGround();
        Vector2 targetVelocity = new Vector2(HorizontalMove * 2f, rb.velocity.y);
        rb.velocity = targetVelocity;
    }



    private void Run()
    {
        Vector3 dir = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(transform.position, transform.position + dir, speed * Time.deltaTime);
        // sprite.flipX = dir.x < 0.0f; Нельзя исп тк мы исп атаку в обе стороны
    }

    private void Jump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }

    private void CheckGround()
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(transform.position, 0.3f);
        isGrounded = collider.Length > 1;
    }

    IEnumerator DoAttack1()
    {
        // attackHitBox1.SetActive(true);
        // yield return new WaitForSeconds(1f);
        // attackHitBox1.SetActive(false);
        // isAttack = false;
        
        yield return new WaitForSeconds(0.1f);
        attackHitBox1.SetActive(true);
        attackHitBox1.transform.localScale += new Vector3(0.0001f, 0, 0);
        yield return new WaitForSeconds(0.5f);
        attackHitBox1.SetActive(false);
        attackHitBox1.transform.position += new Vector3(-0.0001f, 0, 0);
        isAttack = false;
    }
    
    
    IEnumerator DoAttack2()
    {
        // attackHitBox2.SetActive(true);
        // yield return new WaitForSeconds(1f);
        // attackHitBox2.SetActive(false);
        // isAttack = false;
        
        yield return new WaitForSeconds(0.3f);
        attackHitBox2.SetActive(true);
        attackHitBox2.transform.localScale += new Vector3(0.0001f, 0, 0);
        yield return new WaitForSeconds(0.9f);
        attackHitBox2.SetActive(false);
        attackHitBox2.transform.position += new Vector3(-0.0001f, 0, 0);
        isAttack = false;
    }
}