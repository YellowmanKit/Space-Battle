using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Center : MonoBehaviour {

	public static Center instance;
	public Main main;
	public Wave wave;
	public Recruit recruit;

	public CraftPool craftPool;
	public ProjectilePool projectilePool;
	public ParticlePool particlePool;

	public Search search;
	public FleetManage fleetManage;

	public Panel panel;
	public BattleUI battleUI;
	public CustomInput input;


	public Transform availableCraftsTransform;
	public Dictionary<CraftName, AvailableCraft> availableCrafts = new Dictionary<CraftName, AvailableCraft>();

	public static float xMin,xMax,yMin,yMax;

	void Awake(){
		Center.instance = this;
	}

	void Start(){
		InitArea ();
		InitAvailableCrafts ();
	}

	void InitArea(){
		Center.xMin = -2.75f;
		Center.xMax = 2.75f;
		Center.yMin = -5f;
		Center.yMax = 5f;
	}

	void InitAvailableCrafts(){
		for (int i = 0; i < availableCraftsTransform.childCount; i++) {
			var availableCraftTransform = availableCraftsTransform.GetChild (i);
			if (availableCraftTransform.gameObject.activeSelf) {
				var availableCraft = availableCraftTransform.GetComponent<AvailableCraft> ();
				availableCrafts.Add (availableCraft.craftName, availableCraft);
			}
		}
		panel.recruitPanel.InitRecriutChoices (availableCrafts);
		craftPool.Init ();
	}

	public static bool OutOfArea(Transform transform){
		return (
		transform.position.x > Center.xMax
			|| transform.position.x < Center.xMin
			|| transform.position.y > Center.yMax
			|| transform.position.y < Center.yMin);
	}
}
