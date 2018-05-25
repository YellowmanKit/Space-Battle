using System.Collections;
using UnityEngine;

public class Move : Craft {

	public float maxSpeed, accel, breakDistance,breakFactor;
	public Vector2 destination;

	float engineForce { get { return rb.mass * accel; } }

	void FixedUpdate(){
		destination = center.input.target;
		Movement ();
	}

	public bool mLock;
	void Movement(){
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
		if (Mathf.Abs(transform.position.x - destination.x) < breakDistance) {
			rb.velocity = new Vector3 (rb.velocity.x * breakFactor, rb.velocity.y, 0f);
		}
		if (Mathf.Abs(transform.position.y - destination.y) < breakDistance) {
			rb.velocity = new Vector3 (rb.velocity.x, rb.velocity.y * breakFactor, 0f);
		}
	}

}