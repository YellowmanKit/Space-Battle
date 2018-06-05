using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pilot : Behav {

	public Side side;
	public Position position;
	public float moveCd,variation;
	float randomize { get { return Random.Range (-variation, variation); } }

	void OnEnable () {
		Init ();
	}

	void Init(){
		gameObject.tag = side.ToString ();
		transform.rotation = Quaternion.Euler (new Vector3 (0f, 0f, side == Side.Enemy? 180f:0f));
	}
	
	Transform target;
	void Update(){
		SearchForTarget ();
		Destination ();
	}

	void SearchForTarget(){
		if (target == null || !target.gameObject.activeSelf) {
			target = search.FindClosestTarget (transform, isPlayer? Side.Enemy : Side.Player);
		}
	}

	float nextMove;
	void Destination(){
		if (time < nextMove) {
			return;
		}
		nextMove = time + moveCd;

		if (target != null && target.gameObject.activeSelf) {
			move.destination = new Vector2 (target.position.x + randomize, Random.Range(yMin,yMax));
		} else {
			SetRandomDestination ();
		}
	}

	float yMax { get { return 
			position == Position.Front ? (side == Side.Player ? -0.5f : 2f) : 
			position == Position.Middle ? (side == Side.Player ? -2f : 3.5f) : 
			position == Position.Back ? (side == Side.Player ? -3.5f : 5f) : 
			position == Position.HalfFree ? (side == Side.Player ? 0f : Center.yMax) : 
			position == Position.Free ? Center.yMax : 0f; } }
	
	float yMin { get { return 
			position == Position.Front? (side == Side.Player? -2f: 0.5f) : 
			position == Position.Middle? (side == Side.Player? -3.5f: 2f): 
			position == Position.Back? (side == Side.Player? -5f: 3.5f): 
			position == Position.HalfFree ? (side == Side.Player ? Center.yMin : 0f) : 
			position == Position.Free ? Center.yMin : 0f; } }

	void SetRandomDestination(){
		move.destination = new Vector2 (Random.Range(Center.xMin,Center.xMax), Random.Range(yMin,yMax));
	}

	public void PrewarmAbility(){
		for (int i = 0; i < transform.childCount; i++) {
			gameObject.BroadcastMessage ("Prewarm");
		}
	}

}
