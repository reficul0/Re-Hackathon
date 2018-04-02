using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrowWeapon : MonoBehaviour {

    public int damage;
    public bool isEnemyShot;
    [Range(0,50)]
    public float speed;
    private Rigidbody2D rb;
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(200f * speed, 0));
        isEnemyShot = false;
        damage = 3;
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        Destroy(gameObject, 5);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.gameObject.CompareTag("Enemy"))
            collision.gameObject.GetComponent<EnemyBase>().Die();
        Destroy(gameObject);
    }
}
