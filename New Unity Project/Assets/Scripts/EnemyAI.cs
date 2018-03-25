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
    private float xBegin;
    private float patrulRadius = 10;
    public bool sleeped;

    public delegate void ActionSleep(bool value);
    // Событие смерти моба
    public static event ActionSleep isSleep;

    private void OnEnable()
    {
        // Привязываем обработчик события убийства
        EnemyBase.isSleep += SetSleeped;//Enemy.isDie
    }

    /// <summary>
    /// Вызывается когда объект уходит со сцены
    /// </summary>
    private void OnDisable()
    {
        // Отвязываем обработчик события убийства
        EnemyBase.isSleep -= SetSleeped;
    }
    
    void SetSleeped(bool value)
    {
        sleeped = value;
    }

    void Start () {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        xBegin = transform.position.x;
        sleeped = false;
  
	}

    // Update is called once per frame
    void FixedUpdate() {
            if (!sleeped)
            {
            //TODO: ПРОВЕРКУ МБ НАДО
            /*  

              }*/

            if (Mathf.Abs(PlayerStats.instance.transform.position.x - transform.position.x) > agresionRange)//(PlayerStats.instance.transform.position.x < transform.position.x)
            {
                if (Mathf.Abs(transform.position.x - xBegin) >= patrulRadius && !seePlayer)
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
                    isSleep(true);
                }

            }
            }
        
    }

    public float GetDirection() {
        return direction;
    }

  
}
