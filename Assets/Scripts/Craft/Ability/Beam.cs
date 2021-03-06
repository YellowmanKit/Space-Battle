﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : Ability {

	public ParticleName particleName;
	public int damage;
	public float beamWidth,duration,delay;

	void OnEnable(){
		shootTime = float.MinValue;
		gameObject.layer = LayerMask.NameToLayer ((isPlayer? "Player":"Enemy") + "Beam");
		beamCollider.size = new Vector2 (0f, beamCollider.size.y);
	}

	float parentRotationZ { get { return Mathf.Deg2Rad * transform.parent.localRotation.eulerAngles.z; } }
	void InitBeamParticle(){
		var psmain = GetComponent<ParticleSystem> ().main;
		psmain.startRotation = parentRotationZ;
		psmain.startSizeY = 5f + 25f * center.scale;
	}

	protected override bool shallUse(){
		return true;
	}

	protected override void UseAbility(){
		InitBeamParticle ();
		ShootBeam ();
	}

	void Update(){
		AbilityUpdate ();
		BeamCollider ();
		BeamParticle ();
		DeadLock ();
	}

	float shootTime;
	void ShootBeam(){
		ps.Play ();
		shootTime = time;
		shallInitCollider = true;
	}

	bool shallInitCollider;
	bool beamIsShooting { get { return time > shootTime + delay && time < shootTime + delay + duration; } }
	BoxCollider2D beamCollider { get { return GetComponent<BoxCollider2D> (); } }
	void BeamCollider(){
		//beamCollider.enabled = beamIsShooting;
		if (beamIsShooting) {
			if (shallInitCollider) {
				beamCollider.size = new Vector2 (beamWidth, beamCollider.size.y);
				shallInitCollider = false;
			} else {
				beamCollider.size = new Vector2 (Mathf.Clamp (beamCollider.size.x - beamWidth * deltaTime / duration, 0.1f, float.MaxValue), beamCollider.size.y);
			}
		} else {
			beamCollider.size = new Vector2 (0f , beamCollider.size.y);
		}
	}

	float remainDuration { get { return shootTime + delay + duration - time; } }
	void OnTriggerEnter2D(Collider2D other){
		other.GetComponentInParent<Hitpoint> ().TakeDamageOverTime (damage * remainDuration / duration, remainDuration);
	}

	void OnTriggerExit2D(Collider2D other){
		other.GetComponentInParent<Hitpoint> ().RemoveDamageOverTime (damage * remainDuration / duration, remainDuration);
	}

	float randomize { get { return Random.Range (-0.05f, 0.05f); } }
	float nextParticle;
	void BeamParticle(){
		if (time > shootTime + delay && time < shootTime + delay + duration - 0.25f && time > nextParticle) {
			nextParticle = time + 0.1f + randomize;
			RaycastHit2D hit = Physics2D.Raycast (transform.position, transform.up, 10f, isPlayer ? playerRaycastLayer : enemyRaycastLayer);
			if (hit.collider != null) {
				var particle = particlePool.Spawn (particleName);
				if (particle != null) {
					particle.GetComponent<Particle> ().InitBeam (hit.point, isPlayer);
				}
			}
		}
	}

	bool deadlockIsSet;
	void DeadLock(){
		if (time < shootTime + delay + duration && !deadlockIsSet) {
			state.deadLock = true;
			deadlockIsSet = true;
		} else if(time > shootTime + delay + duration && deadlockIsSet) {
			state.deadLock = false;
			deadlockIsSet = false;
		}
	}

}