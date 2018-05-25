using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchProjectile : Ability {

	public GameObject projectile;

	void Update(){
		AttemptToShootAbility ();
	}

	protected override void ShootAbility(){
		//Debug.Log ("Ability shoted");
		Instantiate (projectile, transform.position,transform.rotation);
	}

}
