using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChatSpawner : MonoBehaviour {

    public static int spawnerArrSize = 16;
    public GameObject[] spawners = new GameObject[spawnerArrSize];

    public static int chatWindowArrSize = 11;

    public GameObject[] chatWindows = new GameObject[chatWindowArrSize];

    public float spawnTimer;
    public float spawnInterval;

    public int i = 0;
    public int j = 0;

    public float spawnSpeed;

    void Start()
    {
        spawnTimer = Random.Range(0, spawnInterval);
    }

	// Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            spawnChat(i, j);
            spawnTimer = 0;
        }
       

    }

    public void spawnChat(int i, int j)
    {
        i = Random.Range(0, chatWindowArrSize);
        j = Random.Range(0, spawnerArrSize);

        Debug.Log(i);
        Debug.Log(j);

        GameObject chatPrefab = chatWindows[i];


        GameObject oppositeSpawner = spawners[j];

        GameObject go = (GameObject)Instantiate(chatPrefab, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.identity);
        go.transform.LookAt(oppositeSpawner.transform.position);
        go.rigidbody2D.AddForce(go.transform.forward * spawnSpeed);

        float AngleRad = Mathf.Atan2((transform.position + new Vector3(oppositeSpawner.transform.position.x, oppositeSpawner.transform.position.y, 0)).y - go.transform.position.y, (transform.position + new Vector3(oppositeSpawner.transform.position.x, oppositeSpawner.transform.position.y, 0)).x - go.transform.position.x);
        // Get Angle in Degrees
        float AngleDeg = (180 / Mathf.PI) * AngleRad;
        // Rotate Object
        go.transform.rotation = Quaternion.Euler(0, 0, AngleDeg);

        go.transform.Rotate(0, 0, 90);
    }
}
