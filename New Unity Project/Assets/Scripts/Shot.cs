using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour {

    public Transform weaponPrefab;
    public float shootingRate = 0.5f;
    private float shootCooldown;
    void Start () {
        shootCooldown = 0f;

    }
	
	// Update is called once per frame
	void Update () {
        if (shootCooldown < 0)
            shootCooldown -= Time.deltaTime;
	}
    public bool CanAttack {
        get { return shootCooldown <= 0; }
    }

    public void Attack(bool isEnemy)
    {
       
            shootCooldown = shootingRate;

            // Создайте новый выстрел
            var shotTransform = Instantiate(weaponPrefab) as Transform;

            // Определите положение
            shotTransform.position = transform.position;

            // Свойство врага
          /*  TrowWeapon shot = shotTransform.gameObject.GetComponent<TrowWeapon>();
            if (shot != null)
            {
                shot.isEnemyShot = isEnemy;
            }*/

            // Сделайте так, чтобы выстрел всегда был направлен на него
           /* MoveScript move = shotTransform.gameObject.GetComponent<MoveScript>();
            if (move != null)
            {
                move.direction = this.transform.right; // в двухмерном пространстве это будет справа от спрайта
            }*/
        
    }
}
