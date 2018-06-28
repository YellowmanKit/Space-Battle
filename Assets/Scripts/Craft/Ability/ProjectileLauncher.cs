using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : Ability {

	public ProjectileName projectileName;

	protected override bool shallUse(){
		return true;
	}

	public int amountPreUse;
	public float delay;
	public ParticleSystem shootEffect;
	public bool shootEffectBeforeDelay;
	float nextShoot;
	protected override void UseAbility(){
		amountToShoot += amountPreUse;
		nextShoot = time + delay;

		if (shootEffect != null && shootEffectBeforeDelay) {
			shootEffect.Play ();
		}
	}

	void FixedUpdate(){
		AbilityUpdate ();
		Shooting ();
	}

	public float shootSeperation;
	int amountToShoot;
	void Shooting(){
		if (amountToShoot > 0 && time > nextShoot) {
			ShootProjectile ();
			nextShoot = time + shootSeperation;
			amountToShoot--;
		}
	}

	public bool isMissile;
	public Vector2[] shotSpawns,initSpeed;
	public float speedVariation;
	float speedRandomize { get { return Random.Range (-speedVariation, speedVariation); } }
	float multiplier { get { return isPlayer ? 1f : -1f; } }
	int count;
	void ShootProjectile(){
		var proj = projectilePool.Spawn(projectileName, isPlayer? Side.Player: Side.Enemy);
		var localScale = transform.parent.localScale;
		proj.transform.position = transform.position;
		proj.transform.Translate (new Vector2(shotSpawns [count].x * localScale.x * multiplier, shotSpawns [count].y * localScale.y * multiplier));

		if (shootEffect != null  && !shootEffectBeforeDelay) {
			shootEffect.transform.position = proj.transform.position;
			shootEffect.Play ();
		}

		proj.transform.rotation = transform.rotation;

		if (!isMissile) {
			proj.BroadcastMessage ("Init", isPlayer);
		}else{
			object[] vars = new object[3];
			vars [0] = isPlayer;
			vars[1] = pilot.target != null ? (Vector2)pilot.target.position : new Vector2 (transform.position.x, isPlayer ? Center.yMax : Center.yMin);
			vars [2] = initSpeed.Length > count? new Vector2( initSpeed [count].x + speedRandomize,initSpeed [count].y + speedRandomize) * multiplier: Vector2.zero;

			proj.BroadcastMessage ("Init",vars);
		}
		count = (count + 1) % shotSpawns.Length;

		ForceOnShoot ();
	}

	public Vector2 shootAccel;
	float xForce { get { return rb.mass * shootAccel.x; } }
	float yForce { get { return rb.mass * shootAccel.y; } }
	void ForceOnShoot(){
		rb.AddRelativeForce (new Vector2 (xForce, yForce));
	}

}