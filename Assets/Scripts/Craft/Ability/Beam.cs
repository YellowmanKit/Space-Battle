using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : Ability {

	public int damage,damageDivide;
	public float beamWidth,duration,delay;

	void OnEnable(){
		gameObject.layer = LayerMask.NameToLayer ((isPlayer? "Player":"Enemy") + "Beam");
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
		BeamDamage ();
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
		if (beamIsShooting) {
			beamCollider.size = new Vector2 (Mathf.Clamp (beamCollider.size.x - beamWidth * deltaTime / duration, 0f, float.MaxValue), beamCollider.size.y);
		}
	}

	List<GameObject> targetList = new List<GameObject>();
	void OnTriggerEnter2D(Collider2D other){
		targetList.Add (other.gameObject);
	}

	void onTriggerExit2D(Collider2D other){
		targetList.Remove (other.gameObject);
	}

	float nextDamage;
	void BeamDamage(){
		if (beamIsShooting && time > nextDamage) {
			for(int i = targetList.Count - 1;i >= 0; i--) {
				if (!targetList[i].activeSelf || targetList[i].GetComponentInParent<State>().destroyed) {
					targetList.RemoveAt (i);
				} else {
					targetList[i].BroadcastMessage ("TakeDamage", damage / (damageDivide > 0 ? damageDivide : 1));
				}
			}
			nextDamage = time + duration / (damageDivide > 0? damageDivide: 1);
		}
	}

}