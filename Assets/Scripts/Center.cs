using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Center : MonoBehaviour {

	public static Center instance;
	public Main main;
	public Wave wave;
	public Recruit recruit;

	public Crafts craftPool;
	public Projectiles projectilePool;
	public Particles particlePool;

	public Search search;
	public FleetManage fleetManage;

	public Panel panel;
	public BattleUI battleUI;
	public CustomInput input;
	public CustomCamera cusCam;

	public Transform availableCraftsTransform;
	public Dictionary<CraftName, AvailableCraft> availableCrafts = new Dictionary<CraftName, AvailableCraft>();

	public static float xMin,xMax,yMin,yMax;

	void Awake(){
		Center.instance = this;
	}

	void Start(){
		InitAvailableCrafts ();
		SetArea ();
	}

	//public float scale { get { return Mathf.Clamp( (craftPool.pools [PoolType.Player].Count / 100f) + fleetManage.amountOfHughCrafts * 0.25f, 0f, 1f); } }
	public float scale;
	public float[] scaleValueForEachClass;
	public void UpdateScale(){
		scale = 0;
		foreach (KeyValuePair<CraftName,List<GameObject>> pair in craftPool.craftsList[Side.Player]) {
			scale += pair.Value.Count * scaleValueForEachClass[(int)Craft.CraftNameToClass(pair.Key)];
		}
		scale = Mathf.Clamp (scale, 0f, 1f);
		SetArea ();
	}

	public float minWidth,maxWidth,minHeight,maxHeight;
	public float width,height;
	public void SetArea(){
		width = minWidth + Mathf.Clamp ((maxWidth - minWidth) * scale, 0f, maxWidth - minWidth);
		height = minHeight + Mathf.Clamp ((maxHeight - minHeight) * scale, 0f, maxHeight - minHeight);
		//Debug.Log (width + " " + height);

		Center.xMin = -width / 2f;
		Center.xMax = width / 2f;
		Center.yMin = -height / 2f;
		Center.yMax = height / 2f;

		cusCam.UpdateCamera ();
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

	public AvailableCraft availCraftByName(CraftName craftName){
		foreach (KeyValuePair<CraftName, AvailableCraft> pair in availableCrafts) {
			if (pair.Value.craftName == craftName) {
				return pair.Value;
			}
		}
		return null;
	}

	public static bool OutOfArea(Transform transform){
		return (
		transform.position.x > Center.xMax
			|| transform.position.x < Center.xMin
			|| transform.position.y > Center.yMax
			|| transform.position.y < Center.yMin);
	}
}
