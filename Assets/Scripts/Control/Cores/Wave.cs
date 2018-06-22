using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : Control {

	public int waveCount,enemyStartCredits;

	void Start(){
		InitAvailEnemies ();
		enemyCreditsForEachWave = enemyStartCredits;
	}

	public void NextWave(){
		fleetManage.PrewarmFleet (craftPool.pools [SideToPoolType(Side.Player)]);

		waveCount++;
		SpawnEnemyWave ();

		search.SortFleetListByXPosition (Side.Enemy);
		fleetManage.PrewarmFleet (craftPool.pools [SideToPoolType(Side.Enemy)]);
	}

	public void WaveCleared(){
		recruit.credits += 250 * waveCount;
		enemyCreditsForEachWave += 100 * waveCount;

		fleetManage.RechargeFleet (craftPool.pools [PoolType.Player]);
		fleetManage.SetFormation (Side.Player);
		main.SetPhase (Phase.Recruit);
	}

	List<AvailableCraft> availEnemies = new List<AvailableCraft>();
	void InitAvailEnemies(){
		foreach (KeyValuePair<CraftName, AvailableCraft> pair in center.availableCrafts) {
			if (!pair.Value.playerOnly) {
				availEnemies.Add (pair.Value);
			}
		}
	}

	public int enemyCreditsForEachWave;
	int enemyCredits;
	void SpawnEnemyWave(){
		enemyCredits = enemyCreditsForEachWave;
		for (int i = 0; i < 1000; i++) {
			if (enemyCredits < 5) {
				break;
			}
			if (enemyCredits > 10000) {
				SpawnEnemy (center.availCraftByName(CraftName.HumpbackWhale));
				continue;
			}
			var craftToSpawn = availEnemies [Random.Range (0, availEnemies.Count - 1)];
			if (craftToSpawn.cost < enemyCredits) {
				SpawnEnemy (craftToSpawn);
			}
		}
	}

	void SpawnEnemy(AvailableCraft craftToSpawn){
		craftPool.Spawn (craftToSpawn.craftName, Side.Enemy);
		enemyCredits -= craftToSpawn.cost;
	}

}