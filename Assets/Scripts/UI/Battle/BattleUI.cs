using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUI : UI {

	public GameObject barPrefab;
	public Transform hitpointIndicators;

	public Bar CreateHpBar(){
		var bar = Instantiate (barPrefab, hitpointIndicators);
		return bar.GetComponent<Bar> ();
	}

}