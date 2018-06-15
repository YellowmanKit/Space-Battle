using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PoolType {
	All,
	Player,
	Enemy
}

public enum CraftName {
	Wasp,
	Bee,
	Beatle,
	Bat,
	Pigeon,
	Eagle,
	Eel,
	Dolphin,
	Shark,
	HumpbackWhale
}

public enum ProjectileName {
	Bolt,
	Bullet,
	Rocket
}

public enum ParticleName {
	BoltOnHit,
	BulletOnHit,
	RocketOnHit,
	BeamOnHit
}

public abstract class Pool : Control {
	
	public int initAmount;
	public Transform inactive,players,enemies;
	public Dictionary<PoolType,List<GameObject>> pools = new Dictionary<PoolType,List<GameObject>>();

	protected void InitPool(){
		var playerPool = new List<GameObject> ();
		pools.Add(PoolType.Player,playerPool);

		var enemyPool = new List<GameObject> ();
		pools.Add(PoolType.Enemy,enemyPool);
	}

	public GameObject[] prefabs;
	public void InitByPrefabs(){
		var allPrefabs = new List<GameObject> ();
		foreach (GameObject prefab in prefabs) {
			allPrefabs.AddRange (CreatePool (prefab.name, prefab, initAmount));
		}
		pools.Add(PoolType.All,allPrefabs);
	}

	protected List<GameObject> CreatePool(string name,GameObject prefab,int amount){
		var prefabs = new List<GameObject> ();
		var container = new GameObject ().transform;
		container.SetParent (inactive);
		container.name = name + "s";
		for (int i = 0; i < amount; i++) {
			var newCraft = InstantiateNewPrefab (name, prefab, container);
			prefabs.Add (newCraft);
		}
		return prefabs;
	}

	protected GameObject InstantiateNewPrefab(string name,GameObject prefab,Transform container){
		var newPrefab = Instantiate (prefab, container);
		newPrefab.name = name;
		newPrefab.SetActive (false);
		return newPrefab;
	}

	protected GameObject SpawnFromPool(string prefabName,GameObject prefab,Side side){
		var container = inactive.Find (prefabName + "s");
		for (int i = 0; i <= container.childCount; i++) {
			if (i == container.childCount) {
				return Wake (InstantiateNewPrefab(prefabName,prefab,container), side);
			}

			var child = container.GetChild (i).gameObject;
			if (!child.activeSelf) {
				return Wake (child, side);
			}
		}
		return null;
	}

	Dictionary<string,int> particleCount = new Dictionary<string,int>();
	protected GameObject SpawnParticleFromPool(string prefabName,GameObject prefab,Side side){
		if (!particleCount.ContainsKey (prefabName)) {
			particleCount.Add (prefabName, 0);
		}
		var count = particleCount [prefabName];

		var container = inactive.Find (prefabName + "s");
		particleCount [prefabName] = (count + 1) % container.childCount;
		return Wake (container.GetChild (count).gameObject,side);
	}

	protected abstract GameObject Wake(GameObject prefab,Side side);
}
