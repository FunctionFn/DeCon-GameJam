using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public int dmg;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnBecameInvisible()
    {
        Destroy(gameObject);
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
