using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {

    public float spawnTimer;

    public float spawnInterval;

    public float w1SpawnInterval;
    public float w2SpawnInterval;
    public float w3SpawnInterval;
    public float w4SpawnInterval;

    public float wXSpawnDecrement;

    public int enemiesRemaining;
    public int enemyCount;

    public int w1Enemies;
    public int w2Enemies;
    public int w3Enemies;
    public int w4Enemies;

    public int wXEnemiesIncrement;
    public int wXEnemies;

    public GameObject player;

    public GameObject zombie;
    public GameObject fastzombie;
    public GameObject bigZombie;
    public GameObject shooter;

    public int spawnRandomizer;
    public float hiveSpeedIncreaseCoef;
    public float hiveSpeedIncreaseExp;

    

    public int wave;

    //Instantiation
    private static EnemyManager _inst;
    public static EnemyManager Inst { get { return _inst; } }

    void Awake()
    {
        _inst = this;
    }

    void Start()
    {
        ChangeWave(1);
    }
	
	// Update is called once per frame
	void Update ()
    {
        spawnTimer += Time.deltaTime;

        WaveSpawner();

        if (enemiesRemaining <= 0)
        {
            ChangeWave(wave + 1);
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

    void WaveSpawner()
    {
        if (spawnTimer > spawnInterval)
        {
            switch (wave)
            {
                case 1:
                    SpawnEnemy(zombie);
                    break;
                case 2:
                    spawnRandomizer = Random.Range(1, 4);
                    switch (spawnRandomizer)
                    {
                        case 1:
                        case 2:
                            SpawnEnemy(zombie);
                            break;
                        case 3:
                            SpawnEnemy(fastzombie);
                            break;
                    }
                    break;
                case 3:
                    spawnRandomizer = Random.Range(1, 6);
                    switch (spawnRandomizer)
                    {
                        case 1:
                        case 2:
                        case 3:
                            SpawnEnemy(zombie);
                            break;
                        case 4:
                            SpawnEnemy(fastzombie);
                            break;
                        case 5:
                            SpawnEnemy(bigZombie);
                            break;
                    }
                    break;
                case 4:
                    spawnRandomizer = Random.Range(1, 7);
                    switch (spawnRandomizer)
                    {
                        case 2:
                            SpawnEnemy(zombie);
                            break;
                        case 3:
                        case 4:
                            SpawnEnemy(fastzombie);
                            break;
                        case 1:
                        case 5:
                            SpawnEnemy(bigZombie);
                            break;
                        case 6:
                            SpawnEnemy(shooter);
                            break;
                    }
                    break;
                default:
                    spawnRandomizer = Random.Range(1, 9);
                    switch (spawnRandomizer)
                    {
                        case 1:
                            SpawnEnemy(zombie);
                            break;
                        case 2:
                        case 7:
                            SpawnEnemy(fastzombie);
                            break;
                        case 3:
                        case 4:
                            SpawnEnemy(bigZombie);
                            break;
                        case 5:
                        case 6:
                        case 8:
                            SpawnEnemy(shooter);
                            break;
                    }
                    break;
            }
            enemiesRemaining -= 1;
            enemyCount += 1;
            spawnTimer = 0;
        }
    }

    void ChangeWave(int waveNum)
    {
        wave = waveNum;

        switch (wave)
        {
            case 1:
                spawnInterval = w1SpawnInterval;
                enemiesRemaining = w1Enemies;
                break;
            case 2:
                spawnInterval = w2SpawnInterval;
                enemiesRemaining = w2Enemies;
                break;
            case 3:
                spawnInterval = w3SpawnInterval;
                enemiesRemaining = w3Enemies;
                break;
            case 4:
                spawnInterval = w4SpawnInterval;
                enemiesRemaining = w4Enemies;
                break;
            default:
                spawnInterval -= wXSpawnDecrement;
                enemiesRemaining = wXEnemies;
                wXEnemies += wXEnemiesIncrement;
                break;
        }


    }
}
