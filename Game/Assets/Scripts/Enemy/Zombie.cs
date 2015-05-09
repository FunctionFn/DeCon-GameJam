using UnityEngine;
using System.Collections;

public class Zombie : Enemy {

    
	// Use this for initialization
	
	// Update is called once per frame
	public override void Update () {
        base.Update();
        transform.position += (target.position - transform.position).normalized * speed * Time.deltaTime;
	}

    public override void OnActivate()
    {
        base.OnActivate();

        Player.Inst.Damage(dmg);

        Destroy(gameObject);
    }

    public override void OnShot(int dmg)
    {
        base.OnShot(dmg);


    }
}
