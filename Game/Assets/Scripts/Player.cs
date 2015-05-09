using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public float speed;
    public bool canShoot;

    public GameObject bulletPrefab;
    public float bulletSpeed;

    public float clampX;
    public float clampY;

	// Use this for initialization
	void Start () {
        canShoot = true;
	}
	
	// Update is called once per frame
    void Update()
    {

        float moveH = Input.GetAxis("Horizontal");
        float moveV = Input.GetAxis("Vertical");

        transform.Translate(moveH * speed, moveV * speed, 0);
        CheckBounds();
        

    }

    void FixedUpdate()
    {
        if ((Input.GetAxis("HorizontalFire") != 0.0f || Input.GetAxis("VerticalFire") != 0.0f) && canShoot)
        {
            Debug.Log("pew");
            Vector2 shootDirection = new Vector3(Input.GetAxis("HorizontalFire"), Input.GetAxis("VerticalFire")).normalized;

            Shoot(shootDirection);
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
}


