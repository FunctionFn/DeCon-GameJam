using UnityEngine;
using System.Collections;

public class ChatWindow : MonoBehaviour {

    //public Transform target;
    //public float speed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
	}

    void OnBecomeInvisible()
    {
        Destroy(gameObject);
    }
}
