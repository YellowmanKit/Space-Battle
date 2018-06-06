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
		rb.AddForce((destination - (Vector2)transform.position) * engineForce);

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