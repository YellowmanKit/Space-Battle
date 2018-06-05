using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Search : Control {

	public Transform FindClosestTarget(Transform caller,Side targetSide){
		var pool = craftPool.pools [SideToPoolType (targetSide)];
		if (pool.Count == 0) {
			return null;
		}

		GameObject target = gameObject;
		float minDistance = float.MaxValue;
		foreach (GameObject craft in pool) {
			float distance = Mathf.Abs (craft.transform.position.x - caller.position.x);
			if (distance < minDistance) {
				target = craft;
				minDistance = distance;
			}
		}
		return target.transform;
	}
}
