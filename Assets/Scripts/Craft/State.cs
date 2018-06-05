using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State : Craft {

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

	void Update(){
		Alpha ();
	}

	float targetAlpha { get { return state.destroyed ? 0f : 1f; } }
	float currentAlpha { get { return sr.color.a; } }
	float minDelta = 0.05f;
	void Alpha(){
		var delta = (targetAlpha - currentAlpha) * 0.1f;
		SetAlpha (currentAlpha + (Mathf.Abs(delta) > minDelta? delta: delta > 0f? minDelta: -minDelta));
		if (currentAlpha <= 0f) {
			gameObject.SetActive (false);
			craftPool.Destroyed (gameObject);
		}
	}

	public void OnHit(){
		if (state.destroyed) {
			return;
		}
		SetAlpha (0.5f);
	}

	void SetAlpha(float alpha){
		Color c = sr.color;
		c.a = alpha > 0f? alpha: 0f;
		sr.color = c;
	}
}
