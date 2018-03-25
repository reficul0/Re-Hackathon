using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    [Range(0, 100)] public float speed;

    [Range(0, 900000)] public float jumpPower;

    private Rigidbody2D rb = null;
    //private Animator anim = null;
    private Vector2 velocity;

    private SpriteRenderer sprite;

    private bool isGrounded = false;

    private void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        velocity = rb.velocity;
        velocity.x = speed * Input.GetAxisRaw("Horizontal");
        rb.velocity = velocity;

        if (isGrounded == true && Input.GetKeyDown(KeyCode.Space) == true){
            rb.AddForce(jumpPower * transform.up);
            isGrounded = false;
          //  anim.SetBool("Jump", true);
        }

        if (rb.velocity.x > 0)
            sprite.flipX = false;
        // transform.localScale = new Vector3(1, 1, 1);
        else if (rb.velocity.x < 0)
            sprite.flipX = true; //transform.localScale = new Vector3(-1, 1, 1);

       // anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrounded = true;
       // anim.SetBool("Jump", false);
    }

}
