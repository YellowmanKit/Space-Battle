using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intercept : Laser {

	void OnEnable(){
		if (projectilePool.pools.Count == 3) {
			targetList = projectilePool.pools [isPlayer ? PoolType.Enemy : PoolType.Player];
		}
	}
		
	protected override bool checkCondition(){
		foreach (GameObject projectile in targetList) {
			if (Vector3.Distance (transform.position, projectile.transform.position) <= range) {
				target = projectile;
				return true;
			}
		}
		return false;
	}
		
	protected override void ShootLaser(){
		laserLine.SetLine (transform.position, target.transform.position);
		target.BroadcastMessage ("Intercepted");
	}

}