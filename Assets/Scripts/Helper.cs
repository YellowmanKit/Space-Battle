using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper : Ref {

	void Update () {
		SpawnEnemyByKey ();
	}

	void SpawnEnemyByKey(){
		if(KeyDown(KeyCode.Alpha1)){
			craftPool.Spawn(CraftName.Bee,Side.Enemy);
		}
		if(KeyDown(KeyCode.Alpha2)){
			craftPool.Spawn(CraftName.Beatle,Side.Enemy);
		}
		if(KeyDown(KeyCode.Alpha3)){
			craftPool.Spawn(CraftName.Bat,Side.Enemy);
		}
		if(KeyDown(KeyCode.Alpha4)){
			craftPool.Spawn(CraftName.Pigeon,Side.Enemy);
		}
		if(KeyDown(KeyCode.Alpha5)){
			craftPool.Spawn(CraftName.Eagle,Side.Enemy);
		}
		if(KeyDown(KeyCode.Alpha6)){
			craftPool.Spawn(CraftName.Eel,Side.Enemy);
		}
		if(KeyDown(KeyCode.Alpha7)){
			craftPool.Spawn(CraftName.Dolphin,Side.Enemy);
		}
		if(KeyDown(KeyCode.Alpha8)){
			craftPool.Spawn(CraftName.HumpbackWhale,Side.Enemy);
		}
		if(KeyDown(KeyCode.Alpha9)){
			craftPool.Spawn(CraftName.BlueWhale,Side.Enemy);
		}
		if(KeyDown(KeyCode.Alpha0)){
			//craftPool.Spawn(CraftName.Bat,Side.Enemy);
		}
			
	}

	bool KeyDown(KeyCode code){
		return Input.GetKeyDown (code);
	}
}
