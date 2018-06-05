using System.Collections;
using UnityEngine;

public class Wave : Control {

	public int waveCount;

	public void NextWave(){
		PlayerFleetPrewarm ();
		waveCount++;
		for (int i = 0; i < 2 * waveCount; i++) {
			center.craftPool.Spawn (CraftName.Wasp, Side.Enemy);
		}
	}

	void PlayerFleetPrewarm(){
		fleetManage.PrewarmFleet (craftPool.pools [PoolType.Player]);
	}

	public void WaveCleared(){
		recruit.credits += 50 * waveCount;
		main.SetPhase (Phase.Recruit);
	}

}