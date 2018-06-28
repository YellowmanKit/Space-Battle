using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crafts : Pool {

	public Dictionary<Side, Dictionary<CraftName,List<GameObject>>> craftsList = new Dictionary<Side, Dictionary<CraftName,List<GameObject>>>();

	public void Init(){
		InitPool ();
		InitCraftsList ();
		var availableCrafts = center.availableCrafts;
		var allFleet = new List<GameObject> ();
		foreach(KeyValuePair<CraftName , AvailableCraft> entry in availableCrafts){
			var craft = entry.Value;
			allFleet.AddRange(CreatePool (craft.craftName.ToString(),craft.prefab,5));
		}
		pools.Add(PoolType.All,allFleet);
	}

	void InitCraftsList(){
		var playerCraftsList = new Dictionary<CraftName,List<GameObject>> ();
		var enemyCraftsList = new Dictionary<CraftName,List<GameObject>> ();
		craftsList.Add (Side.Player, playerCraftsList);
		craftsList.Add (Side.Enemy, enemyCraftsList);
	}

	public GameObject Spawn(CraftName craftName,Side side){
		return SpawnFromPool (craftName.ToString (), center.availableCrafts [craftName].prefab, side);
	}

	protected override GameObject Wake(GameObject craft,Side side){
		pools [SideToPoolType(side)].Add (craft);

		craft.GetComponent<Pilot> ().side = side;

		craft.transform.SetParent (side == Side.Enemy ? enemies : players);

		var craftName = StringToCraftName (craft.name);
		if(!craftsList [side].ContainsKey(craftName)){
			var list = new List<GameObject> ();
			craftsList [side].Add (craftName,list);
		}
		craftsList [side] [craftName].Add (craft);

		if (side == Side.Player) {
			center.UpdateScale ();
		}

		craft.SetActive (true);
		return craft;
	}

	public void Destroyed(GameObject craft){
		var side = craft.GetComponent<Pilot> ().side;
		pools [SideToPoolType (side)].Remove (craft);

		var craftName = StringToCraftName (craft.name);
		craftsList [side] [craftName].Remove (craft);

		craft.transform.SetParent (inactive.Find(craft.name + "s"));

		if (main.gamePhase == Phase.Recruit) {
			panel.UpdatePanel ();
		}
		if (main.gamePhase == Phase.Battle && !fleetManage.enemyExist) {
			wave.WaveCleared ();
		}
		if (main.gamePhase == Phase.Battle && !fleetManage.playerExist) {
			fleetManage.SetFormation (Side.Enemy);
			main.GameOver ();
		}

		if (side == Side.Player) {
			center.UpdateScale ();
		}

	}

}