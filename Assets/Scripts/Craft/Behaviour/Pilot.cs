using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pilot : Behav {

	public Side side;
	public float moveCd,variation;
	public bool backWhenShieldDown;

	float randomize { get { return Random.Range (-variation, variation); } }

	void OnEnable () {
		Init ();
	}

	void Init(){
		gameObject.tag = side.ToString ();
		transform.rotation = Quaternion.Euler (new Vector3 (0f, 0f, side == Side.Enemy? 180f:0f));
	}
	
	public Transform target;
	void Update(){
		SearchForTarget ();
		Destination ();
	}

	void SearchForTarget(){
		if (target == null || !target.gameObject.activeSelf) {
			target = search.FindClosestTarget (transform, isPlayer? Side.Enemy : Side.Player);
			yFree = false;
		}
	}

	public float backDistance;
	public bool yFree;
	float distanceFromFrontline { get { return side == Side.Enemy ? backDistance : -backDistance; } }
	float yPosition { get { return (backWhenShieldDown && shield != null && shield.isDown)? isPlayer? Center.yMin: Center.yMax : Random.Range (distanceFromFrontline - 0.5f, distanceFromFrontline + 0.5f); } }
	float nextMove;
	void Destination(){
		if (time < nextMove) {
			return;
		}

		if (target != null && target.gameObject.activeSelf) {
			move.destination = new Vector2 (target.position.x + randomize, yFree? target.position.y + randomize: yPosition);
			nextMove = time + moveCd;
		} else {
			SetRandomDestination ();
			nextMove = time + 0.05f;
		}
	}

	void SetRandomDestination(){
		move.destination = new Vector2 (Random.Range(Center.xMin,Center.xMax), yPosition);
	}

	public void PrewarmAbility(){
		for (int i = 0; i < transform.childCount; i++) {
			gameObject.BroadcastMessage ("Prewarm");
		}
	}

}
