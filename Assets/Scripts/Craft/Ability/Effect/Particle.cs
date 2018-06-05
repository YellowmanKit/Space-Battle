using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : Ref {

	ParticleSystem ps { get { return GetComponent<ParticleSystem>(); } }

	public void Init(Vector3 position, Quaternion rotation, float duration){
		transform.position = position;
		transform.rotation = rotation;
		disableTime = time + duration;
		ps.Play ();
	}

	float disableTime;
	void Update(){
		if (time > disableTime) {
			gameObject.SetActive (false);
		}
	}

}
