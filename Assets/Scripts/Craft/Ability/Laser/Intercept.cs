using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Intercept : Laser {

	CircleCollider2D trigger { get { return GetComponent<CircleCollider2D> (); } }

	void OnEnable(){
		targetList = new List<GameObject> ();
		InitCollider ();
		InitLayer ();
	}

	void InitCollider(){
		trigger.radius = range / transform.parent.localScale.x;
	}

	void InitLayer(){
		var layer = LayerMask.NameToLayer (pilot.side.ToString() + "Trigger");
		if (layer != -1) {
			gameObject.layer = layer;
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		targetList.Add (other.gameObject);
	}

	void OnTriggerExit2D(Collider2D other){
		targetList.Remove (other.gameObject);
	}

	protected override bool checkCondition(){
		for (int i = targetList.Count - 1; i >= 0; i--) {
			if (!targetList [i].activeSelf || Vector2.Distance(targetList[i].transform.position,transform.position) > range) {
				targetList.RemoveAt (i);
			} else {
				target = targetList [i];
				return true;
			}
		}
		return false;
	}

	protected override void ShootLaser(){
		laserLine.SetLine (transform.position, target.transform.position);
		target.BroadcastMessage ("Intercepted");
		targetList.Remove (target);
	}

}