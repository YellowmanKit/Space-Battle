using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePool : Pool {

	void Start(){
		Init ();
	}

	public void Init(){
		InitPool ();
		InitByPrefabs ();
	}

	public GameObject Spawn(ProjectileName projectileName,Side side){
		return SpawnFromPool (projectileName.ToString (), prefabs [(int)projectileName], side);
	}

	protected override GameObject Wake(GameObject projectile,Side side){
		pools [SideToPoolType(side)].Add (projectile);
		projectile.transform.SetParent (side == Side.Enemy ? enemies : players);
		projectile.tag = side.ToString() + "Projectile";
		projectile.SetActive (true);

		return projectile;
	}

	public void Destroyed(GameObject projectile){
		var side = projectile.tag == "PlayerProjectile"? Side.Player:Side.Enemy;
		pools [SideToPoolType (side)].Remove (projectile);
		projectile.transform.SetParent (inactive.Find(projectile.name + "s"));
	}

}