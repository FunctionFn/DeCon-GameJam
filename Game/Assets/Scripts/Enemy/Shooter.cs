using UnityEngine;
using System.Collections;

public class Shooter : Zombie
{

    

    public GameObject bulletPrefab;
    public float bulletSpeed;

    public float timer;
    public float shootInterval;

	// Update is called once per frame
	public override void Update () 
    {
        base.Update();
        timer += Time.deltaTime;

        
        if (timer >= shootInterval)
        {
            Shoot();
            timer = 0;
        }
    }
    void Shoot()
    {
        
        GameObject go = (GameObject)Instantiate(bulletPrefab, new Vector3(transform.position.x,transform.position.y,0), Quaternion.identity);
        go.transform.LookAt(target);
        go.rigidbody2D.AddForce(go.transform.forward * bulletSpeed);

        float AngleRad = Mathf.Atan2(target.transform.position.y - go.transform.position.y, target.transform.position.x - go.transform.position.x);
        // Get Angle in Degrees
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        // Rotate Object
        go.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);

        go.transform.Rotate(0, 0, 90);

        Debug.Log("pew");
        
    }
	
	
}
