using System.Collections;
using UnityEngine;

public class Limit : Craft {

	public float xMin,xMax,yMin,yMax;
	float clampForce { get { return rb.mass * 2; } }

	void Update () {
		ClampPosition ();
	}

	void ClampPosition(){
		float xDir = 0;
		if (transform.position.x < xMin) {
			xDir = 1f;
		}
		if (transform.position.x > xMax) {
			xDir = -1f;
		}
		float yDir = 0;
		if (transform.position.y < yMin) {
			yDir = 1f;
		}
		if (transform.position.y > yMax) {
			yDir = -1f;
		}
		rb.AddForce (new Vector2 (clampForce * xDir, clampForce * yDir));
	}
}