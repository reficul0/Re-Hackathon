using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {

    public float speed = 11f; //будет больше скорости персонажа примерно на 2
    private float direction = -1;
    public float agresionRange = 15;
    private Rigidbody2D rb = null;
    private Vector2 velocity;
    private bool seePlayer = false;
    private SpriteRenderer sprite;
    private int count_flip = 0;//костыль

    void Start () {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {



        if (Mathf.Abs(PlayerStats.instance.transform.position.x - transform.position.x) > agresionRange)//(PlayerStats.instance.transform.position.x < transform.position.x)
        {
            seePlayer = false;
            velocity = rb.velocity;
            velocity.x = speed * direction;
            rb.velocity = velocity;
        }
        else if (Mathf.Abs(PlayerStats.instance.transform.position.x - transform.position.x) <= agresionRange) //(PlayerStats.instance.transform.position.x > transform.position.x)
        {
            seePlayer = true;
            if (PlayerStats.instance.transform.position.x < transform.position.x)
            {
                direction = -1f;
                velocity.x = speed * direction;
                rb.velocity = velocity;
                sprite.flipX = false;
            }
            else if (PlayerStats.instance.transform.position.x > transform.position.x)
            {
                direction = 1f;
                velocity.x = speed * direction;
                rb.velocity = velocity;
                sprite.flipX = true;
            }
            else if (PlayerStats.instance.transform.position.x == transform.position.x)
            {

            }

        }
    }

    public float GetDirection() {
        return direction;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Patrul") && !seePlayer)
        {
            direction *= -1f;
            if (count_flip == 0)
            {
                sprite.flipX = true;
                count_flip++;
            }
            else
            {
                sprite.flipX = false;
                count_flip--;
            }
                
        }
    }
}
