using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBar : MonoBehaviour {

    private SpriteRenderer []hpCircles = new SpriteRenderer[3];

    public delegate void ActionDamage(uint damage);
    // Событие, вызываемое во время получения игроком урона
    public static event ActionDamage takeDamage;

    private uint countOfEnebledBars;

    /// <summary>
    /// Вызывается когда объект появляется на сцене
    /// </summary>
    private void OnEnable()
    {
        // Привязываем обработчик события убийства
        PlayerStats.takeDamage += HideCircle;//Enemy.isDie
    }

    /// <summary>
    /// Вызывается когда объект уходит со сцены
    /// </summary>
    private void OnDisable()
    {
        // Отвязываем обработчик события убийства
        PlayerStats.takeDamage -= HideCircle;
    }

    private void Start()
    {
        countOfEnebledBars = 3;
        hpCircles = GetComponentsInChildren<SpriteRenderer>();
    }
    public void HideCircle(uint damage)//получает кривую цифру из расчетов хп и фантазии больной головы тупого прогера
    {
        if (hpCircles.Length == 1) { return; }
        for (int i = 0; i < damage; ++i)
        {
            hpCircles[countOfEnebledBars - 1].gameObject.SetActive(false);
            --countOfEnebledBars;

            //if (countOfEnebledBars == 0)
            //{
            //}
        }
    }
}
