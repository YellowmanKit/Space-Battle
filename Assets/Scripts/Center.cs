using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Center : MonoBehaviour {

	public Main main;
	public Panel panel;
	public Wave wave;
	public CustomInput input;
	public Recruit recruit;

	public Transform availableCraftsTransform;
	public Dictionary<string, AvailableCraft> availableCrafts = new Dictionary<string, AvailableCraft>();

	public float time;

	void Start(){
		InitAvailableCrafts ();
	}

	void InitAvailableCrafts(){
		for (int i = 0; i < availableCraftsTransform.childCount; i++) {
			var availableCraftTransform = availableCraftsTransform.GetChild (i);
			if (availableCraftTransform.gameObject.activeSelf) {
				var availableCraft = availableCraftTransform.GetComponent<AvailableCraft> ();
				availableCrafts.Add (availableCraft.craftName, availableCraft);
			}
		}
		panel.recruitPanel.InitRecriutChoices ();
	}
	
	void Update () {
		time = Time.timeSinceLevelLoad;
	}
}
