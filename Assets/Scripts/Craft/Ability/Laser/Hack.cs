using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hack : Laser {

	CraftName[] targetCrafts = new CraftName[2]{ CraftName.Bee, CraftName.Beatle };
	Side targetSide { get { return isPlayer ? Side.Enemy : Side.Player; } }
	void OnEnable(){
		targetList = new List<GameObject> ();
	}

	void FetchTargetList(){
		foreach (CraftName craftName in targetCrafts) {
			if (craftPool.craftsList [targetSide].ContainsKey (craftName)) {
				targetList.AddRange(craftPool.craftsList [targetSide][craftName]);
			}
		}
	}

	protected override bool checkCondition(){
		if (target != null) {
			return true;
		}
		FetchTargetList ();
		if (targetList.Count == 0) {
			target = null;
			pilot.target = null;
			pilot.yFree = false;
			return false;
		}
		target = targetList [Random.Range (0, targetList.Count)];
		//var targetClass = target.GetComponent<State> ().craftClass;
		//laserVariation = targetClass == Class.Battleship ? 0.15f : targetClass == Class.Cruiser ? 0.03f : 0.01f;
		//bonusRange = targetClass == Class.Battleship ? 0.35f : targetClass == Class.Cruiser ? 0.1f : 0f;

		pilot.target = target.transform;
		pilot.yFree = true;

		return true;
	}

	public int hackValue;
	//float laserVariation,bonusRange;
	//float randomize { get { return Random.Range (-laserVariation, laserVariation); } } 
	protected override void ShootLaser(){
		if (Vector3.Distance (transform.position, target.transform.position) <= (range)) {
			laserLine.SetLine (transform.position, new Vector3(target.transform.position.x, target.transform.position.y, 0f));
			//target.SendMessage ("Hacked", hackValue);
		}
	}

}
