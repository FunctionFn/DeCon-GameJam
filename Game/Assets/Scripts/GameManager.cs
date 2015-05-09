using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Player.Inst.health < 0)
        {
            Application.LoadLevel(Application.loadedLevel);
        }
	}
}
