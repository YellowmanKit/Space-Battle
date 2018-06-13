using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : Ref {

	public ParticleName particleName;
	public bool isPlayerProjectile;
	protected Rigidbody2D rb { get { return GetComponent<Rigidbody2D> (); } }

	protected void InitProjectile(bool isPlayer){
		gameObject.layer = LayerMask.NameToLayer ((isPlayer? "Player":"Enemy") + "Projectile");
		isPlayerProjectile = isPlayer;
	}

	protected void ProjectileUpdate(){
		if (Center.OutOfArea(transform)) {
			SelfDestruct ();
		}
	}

	protected void SelfDestruct(){
		gameObject.SetActive (false);
		projectilePool.Destroyed (gameObject,isPlayerProjectile);
	}

	protected bool repelled;
	float multiplier { get { return repelled ? -1f : 1f; } }
	protected void SpawnOnHitEffect(){
		var onhit = particlePool.Spawn(particleName);
		onhit.Init (transform.position, Quaternion.Euler (new Vector3 (isPlayerProjectile ? -90f * multiplier : 90f * multiplier, 0f, 0f)));
	}

}