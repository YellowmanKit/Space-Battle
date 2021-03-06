﻿using System.Collections;
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
		transform.position = new Vector3 (Random.Range (Center.xMin, Center.xMax), isPlayer ? Center.yMin + (distanceFromFrontline + randomize) : Center.yMax + (distanceFromFrontline + randomize), 0f);
		transform.rotation = Quaternion.Euler (new Vector3 (0f, 0f, side == Side.Enemy? 180f:0f));
	}
	
	public Transform target;
	void Update(){
		SearchForTarget ();
		Destination ();
	}

	void SearchForTarget(){
		if (target == null || !target.gameObject.activeSelf) {
			target = search.FindClosestTarget (gameObject, isPlayer? Side.Enemy : Side.Player);
		}
	}

	public float backPercentage;
	public bool yFree;
	float distanceFromFrontline { get { return side == Side.Enemy ? Center.yMax * backPercentage : Center.yMax * backPercentage * -1f; } }
	float yPosition { get { return (backWhenShieldDown && shield != null && shield.isDown)? isPlayer? Center.yMin * 0.85f + randomize: Center.yMax * 0.85f + randomize : distanceFromFrontline + randomize; } }
	float nextMove;
	void Destination(){
		if (time < nextMove) {
			return;
		}
		nextMove = time + moveCd;

		if (target != null && target.gameObject.activeSelf) {
			move.destination = new Vector2 (target.position.x + randomize, yFree? target.position.y + randomize: yPosition);
		} else {
			SetFormationDestination ();
		}
	}

	public float formationX;
	void SetFormationDestination(){
		move.destination = new Vector2 (formationX + randomize, yPosition);
	}

	public void PrewarmAbility(){
		for (int i = 0; i < transform.childCount; i++) {
			gameObject.BroadcastMessage ("Prewarm");
		}
	}

}