using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Search : Control {

	public Transform FindClosestTarget(GameObject caller,Side targetSide){
		var targetPool = craftPool.pools [SideToPoolType (targetSide)];
		var allyPool = craftPool.pools [SideToPoolType (targetSide == Side.Player ? Side.Enemy : Side.Player)];

		if (targetPool.Count == 0 || allyPool.Count == 0) {
			return null;
		}

		return targetPool [Mathf.FloorToInt(allyPool.IndexOf (caller) * targetPool.Count / allyPool.Count) ].transform;
	}

	float nextUpdate;
	void Update(){
		if (time < nextUpdate) {
			return;
		}
		SortFleetListByXPosition (Side.Player);
		SortFleetListByXPosition (Side.Enemy);

		nextUpdate = time + 1f;
	}

	public void SortFleetListByXPosition(Side side){
		var list = craftPool.pools [SideToPoolType (side)];
		list.Sort((a,b)=>( a.transform.position.x.CompareTo(b.transform.position.x) ));
	}


}
