using System.Collections;
using UnityEngine;

public class Move : Craft {

	public float maxSpeed, accel, breakDistance,breakFactor;
	public Vector2 destination;

	float engineForce { get { return rb.mass * accel; } }

	void FixedUpdate(){
		//destination = center.input.target;
		Movement ();
		Break ();
	}

	public bool mLock, isMissile;
	void Movement(){
		/*if (Center.OutOfArea (transform)) {
			return;
		}*/
		if (mLock) {
			rb.velocity = Vector2.zero;
			return;
		}
		Vector2 diff = input.target - (Vector2)transform.position;
		if (!isMissile && isPlayer && input.lastTouch > time - 0.25f && diff.magnitude < 1f) {
			rb.AddForce (Vector3.Normalize(diff) * engineForce * -1f / Mathf.Clamp(diff.magnitude * 2f,0.05f,float.MaxValue));
		} else {
			diff = destination - (Vector2)transform.position;
			rb.AddForce (Vector3.Normalize(diff) * engineForce * Mathf.Clamp(diff.magnitude,1f,3f));
		}
	}

	bool breaking { get { return (breakDistance > 0f && Vector3.Distance (transform.position, destination) < breakDistance) || rb.velocity.magnitude > maxSpeed; } }
	void Break(){
		if (breaking) {
			rb.velocity = rb.velocity * breakFactor;
		}
	}

}