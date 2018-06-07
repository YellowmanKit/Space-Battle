using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolt : Projectile {

	public Side side;
	public int damage;
	public float initSpeed,accel;

	bool hitted;

	public void Init(){
		rb.velocity = transform.up * initSpeed;
		hitted = false;
	}

	void FixedUpdate(){
		ProjectileUpdate ();
		rb.AddRelativeForce (Vector2.up * accel);
	}

	void OnTriggerEnter2D(Collider2D other){
		if (OnHit (other)) {
			SpawnOnHitEffect ();
			DealDamage (other);
			SelfDestruct ();
		}
	}

	void DealDamage(Collider2D other){
		other.gameObject.SendMessage("TakeDamage",damage);
		hitted = true;
	}

	bool OnHit(Collider2D other){
		repelled = other.tag == "Shield" ? true : false;
		var tag = other.tag == "Shield"? other.transform.parent.tag: other.tag;
		return (
			!hitted && 
			((gameObject.tag == "PlayerProjectile" && tag == "Enemy") ||
				(gameObject.tag == "EnemyProjectile" && tag == "Player"))
		);
	}

	public void Intercepted(){
		repelled = false;
		SpawnOnHitEffect ();
		SelfDestruct ();
	}

}