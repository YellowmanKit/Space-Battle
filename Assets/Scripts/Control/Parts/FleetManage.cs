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

}
