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
		foreach (GameObject craft in targetList) {
			if (craft.GetComponent<Hitpoint>().isDamaged) {
				target = craft;
				pilot.target = craft.transform;
				pilot.yFree = true;
				return true;
			}
		}
		target = null;
		pilot.target = null;
		pilot.yFree = false;
		return false;
	}

	public int repairValue;
	public float laserVariation;
	float randomize { get { return Random.Range (-laserVariation, laserVariation); } } 
	protected override void ShootLaser(){
		if (Vector3.Distance (transform.position, target.transform.position) <= range) {
			laserLine.SetLine (transform.position, new Vector3(target.transform.position.x + randomize,target.transform.position.y + randomize, 0f));
			target.BroadcastMessage ("Repaired", repairValue);
		}
	}
}