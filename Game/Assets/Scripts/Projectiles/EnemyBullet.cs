using UnityEngine;
using System.Collections;

public class EnemyBullet : Bullet {

    public int dmg;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Player>())
        {
            other.gameObject.GetComponent<Player>().Damage(dmg);

            Destroy(gameObject);
        }
    }

}
