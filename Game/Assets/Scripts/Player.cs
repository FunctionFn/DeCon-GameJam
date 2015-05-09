using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public float speed;
    public bool canShoot;

    public int maxHealth;
    public int health;

    public GameObject bulletPrefab;
    public float bulletSpeed;

    public string weapon;
    public int ammo;
    public bool bIsReloading;

    public float clampX;
    public float clampY;
    public float timer;
    public float shootInterval;


    // Weapon Variables

    public int pistolAmmo;
    public float reloadTimer;
    public float pistolReloadTime;










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
	}
	
	// Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        float moveH = Input.GetAxis("Horizontal");
        float moveV = Input.GetAxis("Vertical");

        transform.Translate(moveH * speed * Time.deltaTime, moveV * speed * Time.deltaTime, 0);
        CheckBounds();

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

    void FixedUpdate()
    {
        if ((Input.GetAxis("HorizontalFire") != 0.0f || Input.GetAxis("VerticalFire") != 0.0f) && canShoot)
        {
            Vector2 shootDirection = new Vector3(Input.GetAxis("HorizontalFire"), Input.GetAxis("VerticalFire")).normalized;

            
            if (timer >= shootInterval)
            {
                Shoot(shootDirection);
                timer = 0;
            }

        }
    }


    void Shoot(Vector2 shootDirection)
    {
        switch (weapon)
        {
            case "pistol":
                if (ammo > 0)
                {
                    GameObject go = (GameObject)Instantiate(bulletPrefab, new Vector3(transform.position.x, transform.position.y, 0), Quaternion.identity);
                    go.rigidbody2D.AddForce(shootDirection * bulletSpeed);
                    ammo -= 1;
                }
                else
                {
                    Reload("pistol");
                    
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
            Damage(other.gameObject.GetComponent<Enemy>().dmg);
        }
    }

    public void Damage(int dmg)
    {
        health -= dmg;
    }

    public void Reload(string gun)
    {
        canShoot = false;
        switch (gun)
        {
            case "pistol":
                reloadTimer += pistolReloadTime;
                break;
        }
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

