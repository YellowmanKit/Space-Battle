using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchProjectile : Ability {

	public ProjectileName projectileName;

	protected override bool shallShoot(){
		return true;
	}

	protected override void ShootAbility(){
		var proj = projectilePool.Spawn(projectileName, isPlayer? Side.Player: Side.Enemy);
		proj.transform.position = transform.position;
		proj.transform.rotation = transform.rotation;
		proj.BroadcastMessage ("Init");
	}

}