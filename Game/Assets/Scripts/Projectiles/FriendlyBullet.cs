using UnityEngine;
using System.Collections;

public class FriendlyBullet : Bullet {

    public int dmg;

    void awake()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Zombie>())
        {
            ParticleSystem pc = (ParticleSystem)Instantiate(hitPC, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            Destroy(pc, pc.duration + .5f);

            Debug.Log("pewpewpew");
            GameManager.Inst.PunchCamera(.25f);

            other.gameObject.GetComponent<Zombie>().OnShot(dmg);

            gameObject.renderer.enabled = false;
            gameObject.collider2D.enabled = false;
            gameObject.rigidbody2D.isKinematic = true;
            Destroy(gameObject, 2);

            
        }
    }

}
