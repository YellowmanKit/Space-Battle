using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repair : Laser {

	void OnEnable(){
		if (craftPool.pools.Count == 3) {
			targetList = craftPool.pools [isPlayer ? PoolType.Player : PoolType.Enemy];
		}
	}

	bool targetNeedRepair { get { return target.GetComponent<Hitpoint> ().isDamaged; } }
	bool targetExist { get { return target != null && target.activeSelf; } }
	protected override bool checkCondition(){
		if (targetExist && targetNeedRepair) {
			return true;
		}
		if (targetList.Count <= 1) {
			return false;
		}

		GameObject closestTarget = null;
		var index = targetList.IndexOf (transform.parent.gameObject) - 1;
		var value = 2;
		for (int i = 0; i < targetList.Count * 2; i++) {
			if (index < 0 || index > targetList.Count - 1) {
				index += value;
				value *= -1;
				value += value > 0? 1 : -1;
				continue;
			}
			if (targetList [index].GetComponent<Hitpoint> ().isDamaged) {
				closestTarget = targetList [index];
				break;
			}
			index += value;
			value *= -1;
			value += value > 0? 1 : -1;
		}

		if (closestTarget != null) {
			var targetClass = closestTarget.GetComponent<State> ().craftClass;
			laserVariation = targetClass == Class.Battleship ? 0.15f : targetClass == Class.Cruiser ? 0.03f : 0.01f;
			bonusRange = targetClass == Class.Battleship ? 0.35f : targetClass == Class.Cruiser ? 0.1f : 0f;

			target = closestTarget;
			pilot.target = closestTarget.transform;
			pilot.yFree = true;
			return true;
		} else {
			target = null;
			pilot.target = null;
			pilot.yFree = false;
			return false;
		}
	}

	public int repairValue;
	float laserVariation,bonusRange;
	float randomize { get { return Random.Range (-laserVariation, laserVariation); } } 
	protected override void ShootLaser(){
		if (Vector3.Distance (transform.position, target.transform.position) <= (range + bonusRange)) {
			laserLine.SetLine (transform.position, new Vector3(target.transform.position.x + randomize,target.transform.position.y + randomize, 0f));
			target.SendMessage ("Repaired", repairValue);
		}
	}
}