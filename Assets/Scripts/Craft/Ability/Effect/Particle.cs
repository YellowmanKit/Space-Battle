using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Particle : Ref {

	ParticleSystem ps { get { return GetComponent<ParticleSystem>(); } }

	public void Init(Vector3 position, Quaternion rotation){
		if (played) {
			return;
		}
		played = true;

		transform.position = position;
		transform.rotation = rotation;

		ps.Play ();
	}

	public void InitBeam(Vector3 position, bool isPlayer){
		if (played) {
			return;
		}
		played = true;

		transform.position = position;

		var rotate = isPlayer? 0f: 180f;
		var psmain = ps.main;
		psmain.startRotation = Random.Range(Mathf.Deg2Rad * (rotate - 45f),Mathf.Deg2Rad * (rotate + 45f));
		ps.Play ();
	}

	bool played;
	float nextPlay;
	void Update(){
		if (time > nextPlay) {
			played = false;
			nextPlay = time + 0.5f;
		}
	}

}