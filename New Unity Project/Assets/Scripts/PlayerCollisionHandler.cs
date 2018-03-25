using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionHandler : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
 
    /// <summary>
    /// Уничтожаем врага при прышке ему на голову(mario style)
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
            foreach (var contact in collision.contacts)
                if (contact.normal.y > 0)
                {
                    collision.gameObject.GetComponent<EnemyBase>().Die();
                    return;
                }  
           // Destroy(collision.gameObject);   
    }
}
