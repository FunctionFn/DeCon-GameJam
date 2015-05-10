using UnityEngine;
using System.Collections;

public class FriendlyBullet : Bullet {

    public int dmg;


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Zombie>())
        {
            Debug.Log("pewpewpew");
            GameManager.Inst.PunchCamera(.25f);

            other.gameObject.GetComponent<Zombie>().OnShot(dmg);

            Destroy(gameObject);

            
        }
    }

}
