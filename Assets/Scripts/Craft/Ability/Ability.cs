using System.Collections;
using UnityEngine;

public abstract class Ability : Craft {

	public float coolDown,prewarm;
	protected float nextShot;

	protected void initAbility(){
		nextShot = center.time + prewarm;
	}

	protected abstract void ShootAbility();

	protected void AttemptToShootAbility(){
		if (center.time > nextShot) {
			nextShot = center.time + coolDown;
			ShootAbility ();
		}
	}

}