using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterceptLaser : Ability {

	public float range;
	GameObject targetProjectile;

	List<GameObject> projectiles;
	void OnEnable(){
		projectiles = projectilePool.pools [isPlayer ? PoolType.Enemy : PoolType.Player];
	}

	protected override bool shallShoot(){
		return projectileInRange ();
	}

	float nextCheck;
	bool projectileInRange(){
		if (time < nextCheck) {
			return false;
		}
		nextCheck = time + 0.1f;
		foreach (GameObject projectile in projectiles) {
			if (Vector3.Distance (transform.position, projectile.transform.position) <= range) {
				targetProjectile = projectile;
				return true;
			}
		}
		return false;
	}

	Line laserLine { get { return GetComponentInChildren<Line> (); } }
	protected override void ShootAbility(){
		laserLine.SetLine (transform.position, targetProjectile.transform.position);
		targetProjectile.BroadcastMessage ("Intercepted");
	}

}