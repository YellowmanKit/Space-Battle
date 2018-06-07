using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FleetManage : Control {

	public bool enemyExist { get { return craftPool.pools [PoolType.Enemy].Count > 0; } }
	public bool playerExist { get { return craftPool.pools [PoolType.Player].Count > 0; } }

	public void PrewarmFleet(List<GameObject> fleet){
		foreach (GameObject craft in fleet) {
			craft.BroadcastMessage ("Prewarm");
		}
	}

	public void RechargeFleet(List<GameObject> fleet){
		foreach (GameObject craft in fleet) {
			var shield = craft.transform.Find ("Shield");
			if (shield != null) {
				shield.SendMessage ("Recharge");
			}
		}
	}

	public void SetFormation(Side side){

		foreach (KeyValuePair<CraftName,List<GameObject>> pair in craftPool.craftsList[side]) {
			if (pair.Value.Count == 0) {
				continue;
			}
			Class craftClass = pair.Value [0].GetComponent<State> ().craftClass;
			float space = craftClass == Class.Drone ? 0.25f : craftClass == Class.Fighter ? 0.5f : craftClass == Class.Cruiser ? 0.75f : craftClass == Class.Battleship ? 2f : 1f;
			float occupied = 0f;
			float multiplier = 0f;
			foreach (GameObject craft in pair.Value) {
				float xPosi = Mathf.Ceil(occupied / 2f) * multiplier * space;
				if (xPosi > Center.xMax || xPosi < Center.xMin) {
					xPosi = 0f;
					occupied = 0f;
				}
				craft.GetComponent<Pilot> ().formationX = xPosi;

				occupied++;
				multiplier = occupied % 2f == 0f ? -1f : 1f;
			}
		}
	}

}
