using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : Ability {

	public ParticleName particleName;
	public int damage;
	public float beamWidth,duration,delay;

	void OnEnable(){
		shootTime = float.MinValue;
		gameObject.layer = LayerMask.NameToLayer ((isPlayer? "Player":"Enemy") + "Beam");
		InitBeamParticle ();
	}

	float parentRotationZ { get { return Mathf.Deg2Rad * transform.parent.localRotation.eulerAngles.z; } }
	void InitBeamParticle(){
		var psmain = GetComponent<ParticleSystem> ().main;
		psmain.startRotation = parentRotationZ;
	}

	protected override bool shallUse(){
		return true;
	}

	protected override void UseAbility(){
		ShootBeam ();
	}

	void Update(){
		AbilityUpdate ();
		BeamCollider ();
		BeamParticle ();
		DeadLock ();
		//BeamDamage ();
	}

	ParticleSystem ps { get { return GetComponent<ParticleSystem>(); } }
	float shootTime;
	void ShootBeam(){
		ps.Play ();
		beamCollider.size = new Vector2 (beamWidth, beamCollider.size.y);
		shootTime = time;
	}

	bool beamIsShooting { get { return time > shootTime + delay && time < shootTime + delay + duration; } }
	BoxCollider2D beamCollider { get { return GetComponent<BoxCollider2D> (); } }
	void BeamCollider(){
		beamCollider.enabled = beamIsShooting;
		if (beamIsShooting) {
			beamCollider.size = new Vector2 (Mathf.Clamp (beamCollider.size.x - beamWidth * deltaTime / duration, 0.1f, float.MaxValue), beamCollider.size.y);
		}
	}

	float remainDuration { get { return shootTime + delay + duration - time; } }
	//List<GameObject> targetList = new List<GameObject>();
	void OnTriggerEnter2D(Collider2D other){
		other.GetComponentInParent<Hitpoint> ().TakeDamageOverTime (damage * remainDuration / duration , remainDuration);
		//targetList.Add (other.gameObject);
	}

	void OnTriggerExit2D(Collider2D other){
		other.GetComponentInParent<Hitpoint> ().RemoveDamageOverTime (damage * remainDuration / duration , remainDuration);
		//targetList.Remove (other.gameObject);
	}

	float randomize { get { return Random.Range (-0.05f, 0.05f); } }
	float nextParticle;
	void BeamParticle(){
		if (time > shootTime + delay && time < shootTime + delay + duration - 0.25f && time > nextParticle) {
			nextParticle = time + 0.1f + randomize;
			RaycastHit2D hit = Physics2D.Raycast (transform.position, transform.up, 10f, isPlayer ? playerRaycastLayer : enemyRaycastLayer);
			if (hit.collider != null) {
				var particle = particlePool.Spawn (particleName);
				particle.GetComponent<Particle>().InitBeam(hit.point, isPlayer);
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

	/*float nextDamage;
	void BeamDamage(){
		if (beamIsShooting && time > nextDamage) {
			for(int i = targetList.Count - 1;i >= 0; i--) {
				//Debug.Log (targetList [i].name);
				if (!targetList[i].activeSelf || targetList[i].GetComponentInParent<State>().destroyed) {
					targetList.RemoveAt (i);
				} else {
					targetList[i].BroadcastMessage ("TakeDamage", damage / (damageDivide > 0 ? damageDivide : 1));
				}
			}
			nextDamage = time + duration / (damageDivide > 0? damageDivide: 1);
		}
	}*/

}