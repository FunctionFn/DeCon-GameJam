using UnityEngine;
using System.Collections;

public class FriendlyBullet : Bullet {

    public int dmg;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Zombie>())
        {
            other.gameObject.GetComponent<Zombie>().OnShot(dmg);

            Destroy(gameObject);
        }
    }
}
