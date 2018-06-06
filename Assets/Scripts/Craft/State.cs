using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : Alpha {

	public Class craftClass;
	public bool destroyed;

	void OnEnable(){
		InitState ();
	}

	void InitState(){
		SetAlpha (1f);
		destroyed = false;
	}

	public void CraftDestroyed(){
		destroyed = true;
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
		return main.gamePhase == Phase.Recruit? 0.5f: state.destroyed ? 0f : 1f;
	}

	protected override void OnAlphaZero(){
		gameObject.SetActive (false);
		craftPool.Destroyed (gameObject);
	}
}
