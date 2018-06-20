using System.Collections;
using UnityEngine;

public class Wave : Control {

	public int waveCount;

	public void NextWave(){
		FleetPrewarm (Side.Player);

		waveCount++;
		//craftPool.Spawn (CraftName.Bat, Side.Enemy);
		for (int i = 0; i < 5 * waveCount; i++) {
			craftPool.Spawn (CraftName.Bat, Side.Enemy);
		}
		for (int i = 0; i < (int)Mathf.Floor((waveCount / 2)); i++) {
			craftPool.Spawn (CraftName.Beatle, Side.Enemy);
			craftPool.Spawn (CraftName.Pigeon, Side.Enemy);
			craftPool.Spawn (CraftName.Eagle, Side.Enemy);
		}
		for (int i = 0; i < (int)Mathf.Floor((waveCount / 3)); i++) {
			craftPool.Spawn (CraftName.Bee, Side.Enemy);
			craftPool.Spawn (CraftName.Eel, Side.Enemy);
		}
		for (int i = 0; i < (int)Mathf.Floor((waveCount / 4)); i++) {
			craftPool.Spawn (CraftName.Dolphin, Side.Enemy);
		}
		for (int i = 0; i < (int)Mathf.Floor((waveCount / 10)); i++) {
			craftPool.Spawn (CraftName.Dolphin, Side.Enemy);
		}

		search.SortFleetListByXPosition (Side.Enemy);
		FleetPrewarm (Side.Enemy);
	}

	void FleetPrewarm(Side side){
		fleetManage.PrewarmFleet (craftPool.pools [SideToPoolType(side)]);
	}

	public void WaveCleared(){
		recruit.credits += 50 * waveCount;
		fleetManage.RechargeFleet (craftPool.pools [PoolType.Player]);
		fleetManage.SetFormation (Side.Player);
		main.SetPhase (Phase.Recruit);
	}

}