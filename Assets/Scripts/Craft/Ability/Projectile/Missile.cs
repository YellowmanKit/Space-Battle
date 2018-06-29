using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : Projectile {

	public int damage;
	public float initSpeed,accel,missileRadius,explosionRadius,deadTime,destVariation;
	float destRandomize { get { return Random.Range (-destVariation, destVariation); } }

	public void Init(object[] vars){
		InitProjectile ((bool)vars[0]);
		SetMissile (false);
		reached = false;

		tr.time = deadTime;
		rb.velocity = transform.up * initSpeed;
		var dest = (Vector2)vars [1];
		move.destination = new Vector2(dest.x + destRandomize, dest.y + destRandomize);
		rb.velocity = (Vector2)vars [2];
	}

	void SetMissile(bool explode){
		exploded = explode;

		circleCollider.enabled = true;
		circleCollider.radius = explode? explosionRadius: missileRadius;

		destructTime = explode? time + deadTime: time + 5f;
		move.mLock = explode;

		if (explode) {
			ps.Stop ();
		} else {
			ps.Play ();
		}
	}

	Move move { get { return GetComponent<Move>(); } }
	float destructTime;
	void FixedUpdate(){
		ProjectileUpdate ();
		CheckIfReachedDestinatione ();
		Destruction ();
	}

	bool reached;
	void CheckIfReachedDestinatione(){
		if (Vector2.Distance (transform.position, move.destination) <= 0.1f && !exploded && !reached) {
			SpawnOnHitEffect ();
			SetMissile (true);
			reached = true;
		}
	}

	void Destruction(){
		if (time > destructTime && !reached && !exploded) {
			SpawnOnHitEffect ();
			SetMissile (true);
			destructTime = float.MaxValue;
		}
	}

	bool exploded;
	void OnTriggerEnter2D(Collider2D other){
		CheckHit (other);
	}

	void OnTriggerStay2D(Collider2D other){
		CheckHit (other);
	}

	void CheckHit(Collider2D other){
		if (OnHit (other)) {
			if (!exploded && !reached) {
				SpawnOnHitEffect ();
				SetMissile (true);
			} else {
				//Debug.Log ("Missile deal damage");
				DealDamage (other);
				circleCollider.enabled = false;
			}
		}
	}

	void DealDamage(Collider2D other){
		other.gameObject.BroadcastMessage("TakeDamage",damage);
	}

	bool OnHit(Collider2D other){
		return other.tag != "Trigger"? true: false;
	}

	public void Intercepted(){
		if (exploded) {
			return;
		}
		//Debug.Log ("Intercepted");
		SpawnOnHitEffect ();
		SetMissile (true);
		circleCollider.enabled = false;
	}

}