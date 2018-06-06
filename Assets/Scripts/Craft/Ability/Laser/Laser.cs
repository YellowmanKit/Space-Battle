using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Laser : Ability {

	public float range;
	protected GameObject target;
	protected List<GameObject> targetList;

	float nextCheck;
	protected abstract bool checkCondition();
	protected override bool shallShoot(){
		if (time < nextCheck) {
			return false;
		}
		nextCheck = time + 0.1f;
		return checkCondition();
	}

	protected abstract void ShootLaser();
	protected override void ShootAbility(){
		ShootLaser ();
	}

	protected Line laserLine { get { return GetComponentInChildren<Line> (); } }
	protected Pilot pilot { get { return GetComponentInParent<Pilot> (); } }

}
