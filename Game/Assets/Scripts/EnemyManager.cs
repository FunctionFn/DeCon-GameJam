using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {

    public float spawnTimer;
    public float spawnInterval;
    public GameObject player;

    public GameObject zombie;
    public GameObject fastzombie;
    public GameObject bigZombie;
    public GameObject shooter;

    public int spawnRandomizer;

    public int wave;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update ()
    {
        spawnTimer += Time.deltaTime;
        if (spawnTimer > spawnInterval)
        {
            switch (wave)
            {
                case 1:
                    SpawnEnemy(zombie);
                    break;
                case 2:
                    spawnRandomizer = Random.Range(1, 3);
                    switch (spawnRandomizer)
                    {
                        case 1:
                            SpawnEnemy(zombie);
                            break;
                        case 2:
                            SpawnEnemy(zombie);
                            break;
                        case 3:
                            SpawnEnemy(fastzombie);
                            break;
                    }
                    break;
                default:

                    break;
            }
            spawnTimer = 0;
        }
    
	}

    void SpawnEnemy(GameObject enemy)
    {
        Vector3 spawnPoint = new Vector3(
            Random.Range(-Player.Inst.clampX, Player.Inst.clampX),
            Random.Range(-Player.Inst.clampY, Player.Inst.clampY),
            0f);

        Debug.Log(spawnPoint.ToString());
        
        GameObject ego = (GameObject)Instantiate(enemy, spawnPoint, Quaternion.identity);
        ego.GetComponent<Enemy>().target = player.transform;
        
        


    }
}
