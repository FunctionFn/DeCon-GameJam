using UnityEngine;
using System.Collections;

public class FriendlyBullet : Bullet {

    public int dmg;


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Zombie>())
        {
            other.gameObject.GetComponent<Zombie>().OnShot(dmg);

            Destroy(gameObject);
        }
    }
}
