using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : Alpha {

	public Class craftClass;
	public CraftName craftName;
	public bool destroyed;

	void OnEnable(){
		InitState ();
		InitEngine ();
		InitLayer ();
	}

	void InitState(){
		SetAlpha (1f);
		destroyed = false;
		engine.gameObject.SetActive (true);
	}

	void InitLayer(){
		var layer = LayerMask.NameToLayer (pilot.side.ToString() + state.craftClass.ToString ());
		if (layer != -1) {
			gameObject.layer = layer;
		}
	}

	public void CraftDestroyed(){
		destroyed = true;
		engine.gameObject.SetActive (false);
	}

	public void OnHit(){
		if (state.destroyed) {
			return;
		}
		SetAlpha (0.5f);
	}

	void Update(){
		AlphaUpdate ();
	}

	protected override float targetAlpha (){
		return destroyed ? 0f : main.gamePhase == Phase.Recruit? 0.5f: 1f;
	}

	protected override void OnAlphaZero(){
		gameObject.SetActive (false);
		craftPool.Destroyed (gameObject);
	}

	Transform engine { get { return transform.Find ("Engine"); } }
	ParticleSystem[] engineParticles { get { return engine.GetComponentsInChildren<ParticleSystem> (); } }
	void InitEngine(){
		SetParticleMain(engineParticles);
	}

	void SetParticleMain(ParticleSystem[] psArray){
		foreach(ParticleSystem ps in psArray){
			var psMain = ps.main;
			psMain.startRotation = (isPlayer? 0f:180f) * Mathf.Deg2Rad;
		}
	}

}
