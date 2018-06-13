using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : Ability {

	protected override bool shallUse(){
		return true;
	}

	protected override void UseAbility(){
		ShootBeam ();
	}

	void FixedUpdate(){
		AbilityUpdate ();
	}

	void ShootBeam(){

	}



}
