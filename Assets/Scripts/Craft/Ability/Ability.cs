using System.Collections;
using UnityEngine;

public abstract class Ability : Ref {

	public float coolDown,prewarm,variation;
	protected float nextShot;
	protected bool craftIsDestroyed { get { return GetComponentInParent<State> ().destroyed; } }

	float randomize { get { return Random.Range (-variation, variation); } }

	void OnEnable(){
		Prewarm ();
	}

	void Prewarm(){
		nextShot = time + prewarm + randomize;
	}

	void Update(){
		AttemptToShootAbility ();
	}

	public bool isPlayer { get { return transform.parent.tag == "Player"; } }
	bool canShoot { get { return !craftIsDestroyed && time > nextShot && ((isPlayer && fleetManage.enemyExist) || (!isPlayer && fleetManage.playerExist)); } }
	protected abstract bool shallShoot();

	protected abstract void ShootAbility();

	void AttemptToShootAbility(){
		if (canShoot && shallShoot()) {
			nextShot = time + coolDown + randomize;
			ShootAbility ();
		}
	}

}