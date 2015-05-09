using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
    public int dmg;
    public Transform target;

    public float speed;
    public int maxHealth;
    public int health;


    public bool bSpawnDisarm = true;
    public float timerDisarm = 0f;
    public float spawnDisarmTime;
	// Use this for initialization
	void Start () {
        bSpawnDisarm = true;
        health = maxHealth;
	}
	
	// Update is called once per frame
	public virtual void Update () {
        if (health <= 0)
        {
            Destroy(gameObject);
            EnemyManager.Inst.enemiesRemaining -= 1;
        }

        timerDisarm += Time.deltaTime;

        if (timerDisarm >= spawnDisarmTime)
        {
            bSpawnDisarm = false;
        }
	}

    public virtual void OnActivate()
    {

    }

    public virtual void OnShot(int dmg)
    {
        health -= dmg;
    }
}
