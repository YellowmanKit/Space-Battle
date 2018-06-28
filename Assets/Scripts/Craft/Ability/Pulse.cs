using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pulse : Ability {

	void OnEnable(){
		InitLayer ();
	}

	void InitLayer(){
		var layer = LayerMask.NameToLayer (pilot.side.ToString() + "Trigger");
		if (layer != -1) {
			gameObject.layer = layer;
		}
	}

	protected override bool shallUse(){
		return true;
	}

	protected override void UseAbility(){
		
	}

}
