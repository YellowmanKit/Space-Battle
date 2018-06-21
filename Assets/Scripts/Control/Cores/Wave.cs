using System.Collections;
using UnityEngine;

public class Wave : Control {

	public int waveCount;

	public void NextWave(){
		FleetPrewarm (Side.Player);

		waveCount++;
		//craftPool.Spawn (CraftName.Bat, Side.Enemy);
		for (int i = 0; i < Mathf.Clamp(3 * waveCount, 6, 40); i++) {
			craftPool.Spawn (CraftName.Bat, Side.Enemy);
		}
		for (int i = 0; i <  Mathf.Clamp(Mathf.Floor(waveCount - 1), 1, 30); i++) {
			craftPool.Spawn (CraftName.Beatle, Side.Enemy);
			craftPool.Spawn (CraftName.Pigeon, Side.Enemy);
			craftPool.Spawn (CraftName.Eagle, Side.Enemy);
		}
		for (int i = 0; i < Mathf.Clamp((int)Mathf.Floor(((waveCount - 3) / 2)),0,20); i++) {
			craftPool.Spawn (CraftName.Bee, Side.Enemy);
			craftPool.Spawn (CraftName.Eel, Side.Enemy);
		}
		for (int i = 0; i < Mathf.Clamp((int)Mathf.Floor(((waveCount - 5) / 3)),0,15); i++) {
			craftPool.Spawn (CraftName.Dolphin, Side.Enemy);
		}
		/*for (int i = 0; i < Mathf.Clamp((int)Mathf.Floor((waveCount / 15)),0,3); i++) {
			craftPool.Spawn (CraftName.HumpbackWhale, Side.Enemy);
		}*/
		SpawnBattleship ();

		search.SortFleetListByXPosition (Side.Enemy);
		FleetPrewarm (Side.Enemy);
	}

	int battleshipWave = 10;
	float count = 1;
	void SpawnBattleship(){
		if (waveCount == battleshipWave) {
			battleshipWave += 5;
			for (int i = 0; i < Mathf.Floor(count); i++) {
				craftPool.Spawn (CraftName.HumpbackWhale, Side.Enemy);
			}
			count += 0.5f;
		}
	}

	void FleetPrewarm(Side side){
		fleetManage.PrewarmFleet (craftPool.pools [SideToPoolType(side)]);
	}

	public void WaveCleared(){
		recruit.credits += 250 * waveCount;
		fleetManage.RechargeFleet (craftPool.pools [PoolType.Player]);
		fleetManage.SetFormation (Side.Player);
		main.SetPhase (Phase.Recruit);
	}

}