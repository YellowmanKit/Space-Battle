using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMP : Ability {

	Pulse pulse { get { return GetComponentInChildren<Pulse> (); } }

	protected override bool shallUse(){
		return true;
	}

	protected override void UseAbility(){
		pulse.ReleasePulse ();
	}

	void FixedUpdate(){
		AbilityUpdate ();
	}

	//public float empStrength;
	public void OnPulseHit(Collider2D other){
		other.gameObject.SendMessage ("Intercepted");
		/*var targetState = other.gameObject.GetComponent<State> ();
		if (targetState == null) {
			other.gameObject.SendMessage ("Intercepted");
		} else {
			other.gameObject.BroadcastMessage ("HittedByEMP",empStrength);
		}*/
	}

}