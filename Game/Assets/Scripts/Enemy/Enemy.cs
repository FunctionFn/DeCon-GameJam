using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
    public int dmg;
    public Transform target;
    public int pointVal;

    public float speed;
    public int maxHealth;
    public int health;

    public float minSpeedVari;
    public float maxSpeedVari;

    public bool bSpawnDisarm = true;
    public float timerDisarm = 0f;
    public float spawnDisarmTime;

    public ParticleSystem deathPC;
	// Use this for initialization
	void Start () {
        bSpawnDisarm = true;
        health = maxHealth;

        speed = speed * Random.Range(minSpeedVari, maxSpeedVari);
	}
	
	// Update is called once per frame
	public virtual void Update () {
        if (health <= 0)
        {
            ParticleSystem pc = (ParticleSystem)Instantiate(deathPC, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            iTween.FadeTo(gameObject, 0f, 10f);
            
            Destroy(pc, pc.duration + .5f);

            
            Destroy(gameObject);
            GameManager.Inst.score += pointVal;
            EnemyManager.Inst.enemyCount -= 1;
            
        }

        timerDisarm += Time.deltaTime;

        if (timerDisarm >= spawnDisarmTime)
        {
            bSpawnDisarm = false;
        }

        speed = speed + EnemyManager.Inst.hiveSpeedIncreaseCoef * Mathf.Pow(EnemyManager.Inst.enemyCount, EnemyManager.Inst.hiveSpeedIncreaseExp);
	}

    public virtual void OnActivate()
    {

    }

    public virtual void OnShot(int dmg)
    {
        health -= dmg;

        iTween.ShakeScale(gameObject, new Vector3(1.3f, 1.3f, 0f), .5f);
        
    }
}
