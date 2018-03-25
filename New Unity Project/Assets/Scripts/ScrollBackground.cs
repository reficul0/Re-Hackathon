using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackground : MonoBehaviour
{

    //public float tileSize;
    //private Transform currentObject;


    public delegate void Action(bool value);
    // Событие смерти моба
    public static event Action ScrollChanged;

    private short reversed;

    private Rigidbody2D rb = null;
    //private Animator anim = null;
    private Vector2 velocity;

    private SpriteRenderer sprite;

    private bool needScroll;
    //private bool isGrounded = false;


    [Range(0, 10)] public float speed;

    //[Range(0, 50000)] public float jumpPower;

    private void OnEnable()
    {
        // Привязываем обработчик события убийства
        ScrollBackground.ScrollChanged += SetScroll;//Enemy.isDie
    }

    /// <summary>
    /// Вызывается когда объект уходит со сцены
    /// </summary>
    private void OnDisable()
    {
        // Отвязываем обработчик события убийства
        ScrollBackground.ScrollChanged -= SetScroll;
    }

    void SetScroll(bool value)
    {
        needScroll = value;
    }

    // Use this for initialization
    void Start()
    {
        //currentObject = GetComponent<Transform>();
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        reversed = -1;
        needScroll = false;
    }

    //// Update is called once per frame
    //void Update()
    //{
    //    //currentObject.position = new Vector3(

    //    //                              , currentObject.position.y
    //    //                              , currentObject.position.z
    //    //                              );
        
    //}

    private void FixedUpdate()
    {
        //if (rb.velocity.x > 0)
        //    reversed = -1;
        ////sprite.flipX = false;
        //// transform.localScale = new Vector3(1, 1, 1);
        //else if (rb.velocity.x < 0)
        //    reversed = -1;
        //Mathf.Repeat(reversed * speed * Input.GetAxisRaw("Horizontal"), tileSize)
        if (!needScroll)
        {
            velocity = rb.velocity;
            velocity.x = reversed * speed * Input.GetAxisRaw("Horizontal");
            rb.velocity = velocity;
            
        }
        else { velocity = rb.velocity = new Vector2(0,0); }
            //endOfWay = false;
        //rb.position.Set(Mathf.Repeat(Time.time * speed, tileSize), rb.position.y);
        //currentObject.position = new Vector3(
        //                              Mathf.Repeat(Time.time * speed, tileSize)
        //                              , currentObject.position.y
        //                              , currentObject.position.z
        //                              );

        //if (isGrounded == true && Input.GetKeyDown(KeyCode.Space) == true)
        //{
        //    rb.AddForce(jumpPower * transform.up);
        //    isGrounded = false;
        //    //  anim.SetBool("Jump", true);
        //}


        //sprite.flipX = true; //transform.localScale = new Vector3(-1, 1, 1);

        // anim.SetFloat("Speed", Mathf.Abs(rb.velocity.x));


    }
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    isGrounded = true;
    //    // anim.SetBool("Jump", false);
    //}
    //private void OnCollisionEnter2D(Collision2D collision)
    //{
    //    //endOfWay = true;
    //    // anim.SetBool("Jump", false);
    //}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ScrollChanged(true);
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        ScrollChanged(false);
    }
}
