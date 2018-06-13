using System.Collections;
using UnityEngine;

public class Line : Ref {

	public float declayInSeconds;

	LineRenderer lr { get { return GetComponent<LineRenderer>(); } }

	public void SetLine(Vector3 startPosition,Vector3 endPosition){
		lr.SetPosition (0, startPosition);
		lr.SetPosition (1, endPosition);
		SetAlpha (1f);
	}

	void Update(){
		Alpha ();
	}

	float targetAlpha { get { return 0f; } }
	float currentAlpha { get { return lr.startColor.a; } }
	void Alpha(){
		var delta = (targetAlpha - currentAlpha) * deltaTime / Mathf.Clamp(declayInSeconds,0.1f,float.MaxValue);
		SetAlpha (currentAlpha + delta);
	}

	void SetAlpha(float alpha){
		Color c = lr.startColor;
		c.a = alpha > 0f? alpha: 0f;
		lr.startColor = c;
		lr.endColor = c;
	}

}
