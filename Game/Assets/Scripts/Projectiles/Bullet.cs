using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public ParticleSystem hitPC;

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

    void OnDisable()
    {
        ParticleSystem pc = (ParticleSystem)Instantiate(hitPC, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
        Destroy(pc, pc.duration + .5f);
    }
}
