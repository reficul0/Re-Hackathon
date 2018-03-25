using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerStats : MonoBehaviour
{

    private uint health;
    private uint money;

    public static PlayerStats instance; 
   // private HpBar hpBar;

    public delegate void Action();
    // Событие, вызываемое во время убийства персонажей
    public static event Action isDie;

    public delegate void ActionDamage(uint damage);
    // Событие, вызываемое во время получения игроком урона
    public static event ActionDamage takeDamage;

    /// <summary>
    /// Вызывается когда объект появляется на сцене
    /// </summary>
    private void OnEnable()
    {
        // Привязываем обработчик события убийства
        EnemyBase.isDie += IncreacseMoney;//Enemy.isDie
    }

    /// <summary>
    /// Вызывается когда объект уходит со сцены
    /// </summary>
    private void OnDisable()
    {
        // Отвязываем обработчик события убийства
        EnemyBase.isDie -= IncreacseMoney;
    }

    void Start()
    {
        // Установим значения по умолчанию
        health = 3;
        money = 0;
        instance = this;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Умираем если здоровье упало до 0
        if (health == 0)
            Die();
    }

    private void Die()
    {
        if (health == 0)
        {
            //TODO: Воспроизводим анимацию смерти
            //TODO: text "u die ballsucker"
            //TODO: reload scene or load scene "valhalla
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void TakeDamage(uint damage) {
        health -= damage;
        takeDamage(damage);

       // Debug.Log(health);
    }
    /// <summary>
    /// Вызывается по сигналу смерти моба
    /// и дает денежную награду за него герою
    /// </summary>
    /// <param name="reward">Количество денег</param>
    public void IncreacseMoney(uint reward)
    {
        money += reward;
        Debug.Log(money);
    }
}

  

   

