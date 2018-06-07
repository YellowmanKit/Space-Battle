using System.Collections;
using UnityEngine;

public abstract class Ability : Ref {

	public float coolDown,prewarm,variation;
	protected float nextUse;
	protected bool craftIsDestroyed { get { return GetComponentInParent<State> ().destroyed; } }
	protected Rigidbody2D rb { get { return GetComponentInParent<Rigidbody2D> (); } }

	float randomize { get { return Random.Range (-variation, variation); } }

	void OnEnable(){
		Prewarm ();
	}

	void Prewarm(){
		nextUse = time + prewarm + randomize;
	}

	protected void AbilityUpdate(){
		AttemptToShootAbility ();
	}

	public bool isPlayer { get { return transform.parent.tag == "Player"; } }
	bool canUse { get { return !craftIsDestroyed && time > nextUse && ((isPlayer && fleetManage.enemyExist) || (!isPlayer && fleetManage.playerExist)); } }
	protected abstract bool shallUse();

	protected abstract void UseAbility();

	void AttemptToShootAbility(){
		if (canUse && shallUse()) {
			nextUse = time + Mathf.Clamp(coolDown + randomize, 0.02f, float.MaxValue);
			UseAbility ();
		}
	}

}