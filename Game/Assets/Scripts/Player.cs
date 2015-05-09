using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public float speed;
    public bool canShoot;

    public int maxHealth;
    public int health;

    public GameObject bulletPrefab;
    public float bulletSpeed;

    public float clampX;
    public float clampY;
    public float timer;
    public float shootInterval;

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
        
        GameObject go = (GameObject)Instantiate(bulletPrefab, new Vector3(transform.position.x,transform.position.y,0), Quaternion.identity);
        go.rigidbody2D.AddForce(shootDirection * bulletSpeed);
        
    }
    void CheckBounds()
    {
		transform.position = new Vector3(Mathf.Clamp(transform.position.x, -clampX, clampX),
		                                 Mathf.Clamp(transform.position.y, -clampY, clampY), transform.position.z);
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Enemy>())
        {
            Debug.Log("ouch");
            Damage(other.gameObject.GetComponent<Enemy>().dmg);
        }
    }

    public void Damage(int dmg)
    {
        health -= dmg;
    }
    
}

