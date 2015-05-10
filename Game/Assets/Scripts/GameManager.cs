using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

    // Death

    public float deathDuration;
    public float deathTimer;

    public AudioSource src;
    // UI Management
    public Text healthText;
    public Text ammoText;
    public Text scoreText;
    public Text waveText;

    public GameObject sceneCamera;

    public float punchX;
    public float punchY;
    public float punchZ;
    public float punchTime;


    public int score;

    // Pickups
    public float timerPickup;
    public float spawnPickupInterval;
    public bool bPickupSpawned;

    public GameObject mGunPickupPrefab;
    public GameObject sGunPickupPrefab;

    //Instantiation
    private static GameManager _inst;
    public static GameManager Inst { get { return _inst; } }

    void Awake()
    {
        _inst = this;
    }
	// Use this for initialization
	void Start () {
        healthText.GetComponent<Text>();
        ammoText.GetComponent<Text>();
        scoreText.GetComponent<Text>();

        bPickupSpawned = false;

        src.time = 60;
	}
	
	// Update is called once per frame
	void Update () {
        if (Player.Inst.health <= 0)
        {
            Player.Inst.bIsDead = true;
        }

        healthText.text = Player.Inst.health.ToString();
        scoreText.text = score.ToString();
        waveText.text = EnemyManager.Inst.wave.ToString();

        if (Player.Inst.canShoot)
        {
            ammoText.text = Player.Inst.ammo.ToString();
        }
        else
        {
            ammoText.text = (Mathf.Round(Player.Inst.reloadTimer * 100)/10).ToString();
        }

        if (bPickupSpawned == false)
            timerPickup += Time.deltaTime;

        if (timerPickup >= spawnPickupInterval)
        {
            timerPickup = 0;
            SpawnPickup();
            bPickupSpawned = true;
        }

        if (Player.Inst.bIsDead)
        {
            deathTimer += Time.deltaTime;
        }

        if (deathTimer >= deathDuration)
        {
          
            Application.LoadLevel("Level1");
        }

	}

    public void SpawnPickup()
    {
        Vector3 spawnPoint = new Vector3(
            Random.Range(-Player.Inst.clampX, Player.Inst.clampX),
            Random.Range(-Player.Inst.clampY, Player.Inst.clampY),
            0f);
        GameObject fgo;
        switch (Random.Range(1,3))
        {
            case 1:
                fgo = (GameObject)Instantiate(mGunPickupPrefab, spawnPoint, Quaternion.identity);
                break;
            case 2:
                fgo = (GameObject)Instantiate(sGunPickupPrefab, spawnPoint, Quaternion.identity);
                break;
        }
    }

    public void PunchCamera(float mult)
    {
        iTween.PunchPosition(sceneCamera, new Vector3 (punchX * mult, punchY * mult, punchZ * mult), punchTime * mult);
    }

    public void cameraColor(Color color)
    {
        sceneCamera.GetComponent<Camera>().backgroundColor = color;
    }
}
