using System.Collections;
using UnityEngine;

public class Move : Craft {

	public float maxSpeed, accel, breakDistance,breakFactor;
	public Vector2 destination;

	float engineForce { get { return rb.mass * accel; } }

	void FixedUpdate(){
		//destination = center.input.target;
		Movement ();
	}

	public bool mLock;
	void Movement(){
		if (Center.OutOfArea (transform)) {
			return;
		}
		if (mLock) {
			destination = (Vector2)transform.position;
			return;
		}
		Vector2 diff = input.target - (Vector2)transform.position;
		if (isPlayer && input.lastTouch > time - 0.25f && diff.magnitude < 1f) {
			rb.AddForce (Vector3.Normalize(diff) * engineForce * -1f / Mathf.Clamp(diff.magnitude * 2f,0.05f,float.MaxValue));
		} else {
			diff = destination - (Vector2)transform.position;
			rb.AddForce (Vector3.Normalize(diff) * engineForce * Mathf.Clamp(diff.magnitude,1f,3f));
		}

		if(rb.velocity.magnitude > maxSpeed){
			rb.velocity = rb.velocity.normalized * maxSpeed;
		}
		BreakWhenNearDestination ();
	}

	void BreakWhenNearDestination(){
		if (Vector3.Distance (transform.position, destination) < breakDistance) {
			rb.velocity = rb.velocity * breakFactor;
		}
	}

}