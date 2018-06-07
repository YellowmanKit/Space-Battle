using System.Collections;
using UnityEngine;

public class Wave : Control {

	public int waveCount;

	public void NextWave(){
		PlayerFleetPrewarm ();
		waveCount++;
		for (int i = 0; i < 5 * waveCount; i++) {
			craftPool.Spawn (CraftName.Bat, Side.Enemy);
		}
		for (int i = 0; i < (int)Mathf.Floor((waveCount / 2)); i++) {
			craftPool.Spawn (CraftName.Beatle, Side.Enemy);
			craftPool.Spawn (CraftName.Pigeon, Side.Enemy);
		}
		for (int i = 0; i < (int)Mathf.Floor((waveCount / 3)); i++) {
			craftPool.Spawn (CraftName.Bee, Side.Enemy);
			craftPool.Spawn (CraftName.Eagle, Side.Enemy);
		}
	}

	void PlayerFleetPrewarm(){
		fleetManage.PrewarmFleet (craftPool.pools [PoolType.Player]);
	}

	public void WaveCleared(){
		recruit.credits += 50 * waveCount;
		fleetManage.RechargeFleet (craftPool.pools [PoolType.Player]);
		fleetManage.SetFormation (Side.Player);
		main.SetPhase (Phase.Recruit);
	}

}