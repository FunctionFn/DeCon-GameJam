using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public float speed;
    public bool canShoot;

    public bool bIsDead;

    public int maxHealth;
    public int health;

    public AudioClip clip;

    public GameObject bulletPrefab;
    public GameObject mGunBulletPrefab;
    public GameObject sGunBulletPrefab;
    public float bulletSpeed;

    public string weapon;
    public int ammo;
    public bool bIsReloading;

    public float clampX;
    public float clampY;


    // Weapon Variables

    public int pistolAmmo;
    public float reloadTimer;
    public float pistolReloadTime;
    public float pistolShootTimer;
    public float pistolShootInterval;

    public float machineGunShootInterval;
    public float machineGunShootTimer;
    public int machineGunAmmo;

    public float shotGunShootInterval;
    public float shotGunShootTimer;
    public float shotGunOffset;
    public int shotGunAmmo;

    // Particle Systems
    public ParticleSystem pickupPC;

    //Instantiation
    private static Player _inst;
    public static Player Inst { get { return _inst; } }

    void Awake()
    {
        _inst = this;
    }

	// Use this for initialization
	void Start ()
    {
        canShoot = true;
        health = maxHealth;
        bIsDead = false;
	}
	
	// Update is called once per frame
    void Update()
    {
        pistolShootTimer += Time.deltaTime;
        machineGunShootTimer += Time.deltaTime;
        shotGunShootTimer += Time.deltaTime;

        if (health < 0)
        {
            health = 0;
        }
    }

    void FixedUpdate()
    {
        if ((Input.GetAxis("HorizontalFire") != 0.0f || Input.GetAxis("VerticalFire") != 0.0f) && canShoot && !bIsDead)
        {
            Vector2 shootDirection = new Vector3(Input.GetAxis("HorizontalFire"), Input.GetAxis("VerticalFire")).normalized;
            Shoot(shootDirection);
        }

        float moveH = Input.GetAxis("Horizontal");
        float moveV = Input.GetAxis("Vertical");
        if (!bIsDead)
        {
            transform.Translate(moveH * speed * Time.deltaTime, moveV * speed * Time.deltaTime, 0);
        }
        CheckBounds();

        if (bIsDead)
        {
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
        }

        if (!canShoot)
        {
            reloadTimer -= Time.deltaTime;
        }

        if (reloadTimer <= 0 && !canShoot)
        {
            canShoot = true;
            Refill(weapon);
        }
    }


    void Shoot(Vector2 shootDirection)
    {
        switch (weapon)
        {
            case "pistol":
                if (ammo > 0 && pistolShootTimer >= pistolShootInterval)
                {
                    
                    GameObject go = (GameObject)Instantiate(bulletPrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                    go.transform.LookAt(transform.position + new Vector3(shootDirection.x,shootDirection.y,0));
                    go.rigidbody2D.AddForce(go.transform.forward * bulletSpeed);

                    float AngleRad = Mathf.Atan2((transform.position + new Vector3(shootDirection.x, shootDirection.y, 0)).y - go.transform.position.y, (transform.position + new Vector3(shootDirection.x, shootDirection.y, 0)).x - go.transform.position.x);
                    // Get Angle in Degrees
                    float AngleDeg = (180 / Mathf.PI) * AngleRad;
                    // Rotate Object
                    go.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);

                    go.transform.Rotate(0, 0, 90);

                    ammo -= 1;
                    pistolShootTimer = 0;
                }
                if (ammo <= 0)
                {
                    Reload();
                    
                }
                break;
            case "machinegun":
                if (ammo > 0 && machineGunShootTimer > machineGunShootInterval)
                {
                    GameObject go = (GameObject)Instantiate(mGunBulletPrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                    go.rigidbody2D.AddForce(shootDirection * bulletSpeed);
                    ammo -= 1;
                    machineGunShootTimer = 0;
                }
                if (ammo <= 0)
                {
                    Reload();
                }

                break;
            case "shotgun":
                if (ammo > 0 && shotGunShootTimer > shotGunShootInterval)
                {
                    GameObject go = (GameObject)Instantiate(sGunBulletPrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                    go.transform.LookAt(transform.position + new Vector3(shootDirection.x, shootDirection.y, 0));
                    go.rigidbody2D.AddForce(go.transform.forward * bulletSpeed);

                    float AngleRad = Mathf.Atan2((transform.position + new Vector3(shootDirection.x, shootDirection.y, 0)).y - go.transform.position.y, (transform.position + new Vector3(shootDirection.x, shootDirection.y, 0)).x - go.transform.position.x);
                    // Get Angle in Degrees
                    float AngleDeg = (180 / Mathf.PI) * AngleRad;
                    // Rotate Object
                    go.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);

                    go.transform.Rotate(0, 0, 90);

                    GameObject goL = (GameObject)Instantiate(sGunBulletPrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                    goL.transform.LookAt(transform.position + new Vector3(shootDirection.x, shootDirection.y, 0));
                    goL.rigidbody2D.AddForce(goL.transform.forward * bulletSpeed);
                    goL.rigidbody2D.AddForce(-goL.transform.up * shotGunOffset);

                    float AngleRadL = Mathf.Atan2((transform.position + new Vector3(shootDirection.x, shootDirection.y, 0)).y - goL.transform.position.y, (transform.position + new Vector3(shootDirection.x, shootDirection.y, 0)).x - goL.transform.position.x);
                    // Get Angle in Degrees
                    float AngleDegL = (180 / Mathf.PI) * AngleRad;
                    // Rotate Object
                    goL.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);

                    goL.transform.Rotate(0, 0, 90);



                    GameObject goR = (GameObject)Instantiate(sGunBulletPrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                    goR.transform.LookAt(transform.position + new Vector3(shootDirection.x, shootDirection.y, 0));
                    goR.rigidbody2D.AddForce(goR.transform.forward * bulletSpeed);
                    goR.rigidbody2D.AddForce(goR.transform.up * shotGunOffset);
                    float AngleRadR = Mathf.Atan2((transform.position + new Vector3(shootDirection.x, shootDirection.y, 0)).y - goR.transform.position.y, (transform.position + new Vector3(shootDirection.x, shootDirection.y, 0)).x - goR.transform.position.x);
                    // Get Angle in Degrees
                    float AngleDegR = (180 / Mathf.PI) * AngleRad;
                    // Rotate Object
                    goR.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);

                    goR.transform.Rotate(0, 0, 90);
                    
                    
                    ammo -= 1;
                    shotGunShootTimer = 0;
                }
                if (ammo <= 0)
                {
                    Reload();
                }
                break;
        }
            
        
    }
    void CheckBounds()
    {
		transform.position = new Vector3(Mathf.Clamp(transform.position.x, -clampX, clampX),
		                                 Mathf.Clamp(transform.position.y, -clampY, clampY), transform.position.z);
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Enemy>() && !other.gameObject.GetComponent<Enemy>().bSpawnDisarm)
        {
            Debug.Log("ouch");
            GameManager.Inst.PunchCamera(1);
            Damage(other.gameObject.GetComponent<Enemy>().dmg);
            Destroy(other.gameObject);
        }
        else if (other.gameObject.GetComponent<Pickup>())
        {
            weapon = other.gameObject.GetComponent<Pickup>().weapon;
            ammo = other.gameObject.GetComponent<Pickup>().ammo;

            GameManager.Inst.bPickupSpawned = false;

            Destroy(other.gameObject);

            ParticleSystem pc = (ParticleSystem)Instantiate(pickupPC, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
            Destroy(pc, pc.duration + .5f);
        }
    }

    public void Damage(int dmg)
    {
        iTween.ShakeScale(gameObject, new Vector3(1.3f, 1.3f, 0f), .5f);
        AudioSource.PlayClipAtPoint(clip, transform.position);
        health -= dmg;
    }

    public void Reload()
    {
        canShoot = false;
        reloadTimer += pistolReloadTime;
        weapon = "pistol";
        

    }

    public void Refill(string gun)
    {
        switch (gun)
        {
            case "pistol":
                ammo = pistolAmmo;
                break;
        }
    }
    
    
}

