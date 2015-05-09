using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
    public int dmg;
    public Transform target;

    public float speed;
    public int maxHealth;
    public int health;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public virtual void OnActivate()
    {

    }

    public virtual void OnShot(int dmg)
    {
        health -= dmg;
    }
}
