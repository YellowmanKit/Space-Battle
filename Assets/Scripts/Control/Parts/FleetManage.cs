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
			var list = pair.Value;
			if (list.Count == 0) {
				continue;
			}

			list.Sort((a,b)=>( a.transform.position.x.CompareTo(b.transform.position.x) ));

			Class craftClass = list [0].GetComponent<State> ().craftClass;
			float space = craftClass == Class.Drone ? 0.35f : craftClass == Class.Fighter ? 0.5f : craftClass == Class.Cruiser ? 0.75f : craftClass == Class.Battleship ? 1.75f : 1f;
			float width = Center.xMax - Center.xMin;
			for (int i = 0; i < 10; i++) {
				if (list.Count * space > width) {
					space *= 0.66f;
				}
			}

			float occupied = 0f;
			float multiplier = 0f;

			var index = Mathf.FloorToInt (list.Count / 2f);
			var value = -1;
			for (int i = 0; i < list.Count * 2; i++) {
				if (index < 0 || index > list.Count - 1) {
					index += value;
					value *= -1;
					value += value > 0? 1 : -1;
					continue;
				}
				float xPosi = Mathf.Ceil(occupied / 2f) * multiplier * space;
				if (xPosi > Center.xMax || xPosi < Center.xMin) {
					xPosi = 0f;
					occupied = 0f;
				}
				var pilot = list [index].GetComponent<Pilot> ();
				pilot.formationX = xPosi;
				//Debug.Log (index + " " + xPosi);

				occupied++;
				multiplier = occupied % 2f == 0f ? 1f : -1f;

				index += value;
				value *= -1;
				value += value > 0? 1 : -1;
			}
				
		}
	}

}