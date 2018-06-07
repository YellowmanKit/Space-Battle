using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class BattleUI : UI {

	public GameObject barPrefab;
	public Transform hitpointIndicators;
	public Text fps;

	public Bar CreateHpBar(){
		var bar = Instantiate (barPrefab, hitpointIndicators);
		return bar.GetComponent<Bar> ();
	}

	float deltaTimeCounter;
	void Update(){
		deltaTimeCounter += (Time.unscaledDeltaTime - deltaTimeCounter) * 0.1f;
		fps.text = Mathf.Floor (1.0f / deltaTimeCounter).ToString ("F0");
	}

}