using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Alpha : Craft {

	protected abstract float targetAlpha ();
	protected abstract void OnAlphaZero ();
	float currentAlpha { get { return sr.color.a; } }
	float minDelta = 0.01f;

	protected void AlphaUpdate(){
		var delta = (targetAlpha() - currentAlpha) * 0.1f;
		SetAlpha (currentAlpha + (Mathf.Abs(delta) > minDelta? delta: delta > 0f? minDelta: -minDelta));
		if (currentAlpha <= 0f) {
			OnAlphaZero ();
		}
	}

	protected void SetAlpha(float alpha){
		Color c = sr.color;
		c.a = alpha > 0f? alpha: 0f;
		sr.color = c;
	}

}
