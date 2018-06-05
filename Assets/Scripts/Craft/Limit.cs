using System.Collections;
using UnityEngine;

public class Limit : Craft {

	float clampForce { get { return rb.mass * 5; } }

	void Update () {
		ClampPosition ();
	}

	void ClampPosition(){
		float xDir = 0;
		if (transform.position.x < Center.xMin) {
			xDir = 1f;
		}
		if (transform.position.x > Center.xMax) {
			xDir = -1f;
		}
		float yDir = 0;
		if (transform.position.y < Center.yMin) {
			yDir = 1f;
		}
		if (transform.position.y > Center.yMax) {
			yDir = -1f;
		}
		rb.AddForce (new Vector2 (clampForce * xDir, clampForce * yDir));
	}
}