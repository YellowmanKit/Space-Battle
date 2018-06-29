using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulse : Alpha {

	void OnEnable(){
		InitLayer ();
		SetPulse (false);
	}

	void InitLayer(){
		var layer = LayerMask.NameToLayer (pilot.side.ToString() + "Trigger");
		if (layer != -1) {
			gameObject.layer = layer;
		}
	}

	public float pulseThickness;
	float worldRadius { get { return circleCollider.radius * currentScale * transform.parent.parent.localScale.x; } }
	void OnTriggerEnter2D(Collider2D other){
		var diff = worldRadius - Vector3.Distance (transform.position, other.transform.position);
		if (diff <= pulseThickness) {
			transform.parent.SendMessage ("OnPulseHit", other);
		}
	}

	CircleCollider2D circleCollider { get { return GetComponent<CircleCollider2D>(); } }
	public void ReleasePulse(){
		SetPulse (true);
		SetAlpha (1f);
		transform.localScale = new Vector3 (0f, 0f, 1f);
	}

	void Update(){
		AlphaUpdate ();
		Scale ();
		PulseEnd ();
	}

	bool pulseActive { get { return time > shootTime && time < endTime; } }
	float currentScale { get { return transform.localScale.x; } }
	public float targetScale,duration;
	void Scale(){
		if (!pulseActive || currentScale >= targetScale) {
			return;
		}

		var delta = targetScale * deltaTime / duration;
		var newValue = currentScale + delta;
		transform.localScale = new Vector3 (newValue, newValue, 1f);
	}

	float shootTime,endTime;
	void PulseEnd(){
		if (time > endTime) {
			endTime = float.MaxValue;
			circleCollider.enabled = false;
			sr.enabled = false;
		}
	}

	void SetPulse(bool enable){
		shootTime = enable ? time : float.MaxValue;
		endTime = enable? time + duration: float.MaxValue;
		circleCollider.enabled = enable;
		sr.enabled = enable;
	}

	protected override float targetAlpha (){
		return 0f;
	}

	protected override void OnAlphaZero(){
	}

}