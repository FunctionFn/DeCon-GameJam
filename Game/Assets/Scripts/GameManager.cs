using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

    // UI Management
    public Text healthText;
    public Text ammoText;
    

	// Use this for initialization
	void Start () {
        healthText.GetComponent<Text>();
        ammoText.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Player.Inst.health < 0)
        {
            Application.LoadLevel(Application.loadedLevel);
        }

        healthText.text = Player.Inst.health.ToString();

        if (Player.Inst.canShoot)
        {
            ammoText.text = Player.Inst.ammo.ToString();
        }
        else
        {
            ammoText.text = (Mathf.Round(Player.Inst.reloadTimer * 100)/10).ToString();
        }

	}
}
