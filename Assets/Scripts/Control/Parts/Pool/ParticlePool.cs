﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePool : Pool {

	void Start(){
		InitByPrefabs ();
	}

	public Particle Spawn(ParticleName particleName){
		return SpawnFromPool (particleName.ToString (), prefabs [(int)particleName], Side.Neutral).GetComponent<Particle>();
	}

	protected override GameObject Wake(GameObject particle,Side side){
		particle.SetActive (true);
		return particle;
	}

}