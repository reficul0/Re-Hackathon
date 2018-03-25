using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    
    private ushort enemy_health;
    private uint damage;
    // Награда за убийство моба
    // TODO: в зависимости от класса моба должа меняться
    private uint reward;
    
    public delegate void Action(uint reward);
    // Событие смерти моба
    public static event Action isDie;
    [Range(0, 900000)]
    public float forceRepulsion;

    void Start ()
    {
        enemy_health = 3;
        reward = 5;
        damage = 1;
    }

    public void Die()
    {
        enemy_health = 0;
    }

    /// <summary> 
    /// Нанесение урона игроку
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) {
            foreach (var contact in collision.contacts)
                if (contact.normal.y >= 0)
                {
                    collision.gameObject.GetComponent<PlayerStats>().TakeDamage(damage);
                  
                   //доделать херню связанную с отталкиванием героя
                   collision.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(7f * -1f, 0.4f) * forceRepulsion);//переделать как отдельные переменные

                    return;
                }
        }
    }
    void FixedUpdate ()
    {
        if (enemy_health == 0)
        {
            // Эммитим сигнал о смерти
            isDie(reward);
            // Разушаем объект
            Destroy(this.gameObject);
        }
	}
}
