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
    private bool returningOnBase;
    private SpriteRenderer sprite;
    private float xBegin;
    private float patrulRadius = 10;
    public bool sleeped;

    public delegate void ActionSleep(bool value, GameObject enemy);
    // Событие засыпания моба после удара
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
    
    void SetSleeped(bool value, GameObject enemy)
    {
        if(enemy == gameObject)
            sleeped = value;
    }

    void Start ()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        xBegin = transform.position.x;
        sleeped = false;
        returningOnBase = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //TODO: могут появиться проблемы если толкнуть моба в спину, но это не точно
        if (!sleeped)
        {
            if ( Mathf.Abs(PlayerStats.instance.transform.position.x - transform.position.x) > agresionRange || returningOnBase
              && Mathf.Abs(PlayerStats.instance.transform.position.y - transform.position.y) > 0)//(PlayerStats.instance.transform.position.x < transform.position.x)
            {
                float wayToStartPosition = transform.position.x - xBegin;

                if (seePlayer && !returningOnBase)
                {

                    if (wayToStartPosition < 0 && direction < 0
                     || wayToStartPosition > 0 && direction > 0)
                    {
                        direction *= -1f;
                    }
                    
                    returningOnBase = true;
                }   
                else if ( Mathf.Abs(wayToStartPosition) >= patrulRadius )
                {
                    if(!returningOnBase)
                        direction *= -1f;
                    if (direction > 0)
                    {
                        sprite.flipX = true;
                    }
                    else if(direction <= 0)
                    {
                        sprite.flipX = false;
                    }
                }
                float trans = transform.position.x - xBegin;
                if ( Mathf.Abs(trans) < 1f )
                {
                    seePlayer = false;
                    returningOnBase = false;
                }
                velocity = rb.velocity;
                velocity.x = speed * direction;
                rb.velocity = velocity;
            }
            else if (Mathf.Abs(PlayerStats.instance.transform.position.x - transform.position.x) <= agresionRange
                  && Mathf.Abs(PlayerStats.instance.transform.position.y - transform.position.y) < 0.1f) //(PlayerStats.instance.transform.position.x > transform.position.x)
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
                    isSleep(true, gameObject);
                }

            }
        }
        
    }

    public float GetDirection()
    {
        return direction;
    }
}
