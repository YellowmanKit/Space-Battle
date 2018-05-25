using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolt : Projectile {

	public float initSpeed,accel;

	public void Start(){
		rb.velocity = transform.up * initSpeed;
	}

	void FixedUpdate(){
		rb.AddRelativeForce (Vector2.up * accel);
	}

}
