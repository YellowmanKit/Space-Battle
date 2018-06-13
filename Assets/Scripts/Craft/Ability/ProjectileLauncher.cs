using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : Ability {

	public ProjectileName projectileName;

	protected override bool shallUse(){
		return true;
	}

	public int amountPreUse;
	float nextShoot;
	protected override void UseAbility(){
		amountToShoot += amountPreUse;
		nextShoot = time;
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

	public Vector2[] shotSpawns;
	int count;
	void ShootProjectile(){
		var proj = projectilePool.Spawn(projectileName, isPlayer? Side.Player: Side.Enemy);
		var localScale = transform.parent.localScale;
		proj.transform.position = transform.position;
		proj.transform.Translate (new Vector2(shotSpawns [count].x * localScale.x, shotSpawns [count].y * localScale.y));

		proj.transform.rotation = transform.rotation;
		proj.BroadcastMessage ("Init",isPlayer);
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