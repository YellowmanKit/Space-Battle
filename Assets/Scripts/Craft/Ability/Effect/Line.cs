using System.Collections;
using UnityEngine;

public class Line : Ref {

	public float declay;

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
	float minDelta = 0.05f;
	void Alpha(){
		var delta = (targetAlpha - currentAlpha) * declay;
		SetAlpha (currentAlpha + (Mathf.Abs(delta) > minDelta? delta: delta > 0f? minDelta: -minDelta));
	}

	void SetAlpha(float alpha){
		Color c = lr.startColor;
		c.a = alpha > 0f? alpha: 0f;
		lr.startColor = c;
		lr.endColor = c;
	}

}
