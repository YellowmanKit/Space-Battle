using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : UI {

	Hitpoint hitpoint;

	public void InitBar(Hitpoint _hitpoint){
		hitpoint = _hitpoint;
		rt.sizeDelta = new Vector2 (Mathf.Clamp(10f + hitpoint.hp * 0.25f, 10f, 100f) , 1.75f);
	}

	void Update(){
		Position ();
		Value ();
		Alpha ();
	}

	void Position(){
		transform.position = hitpoint.transform.position;
		transform.Translate (hitpoint.transform.up * -1f * hitpoint.barDistance, Space.Self);
	}

	float targetValue { get { return hitpoint != null ? hitpoint.hp : 0; } }
	float maxValue { get { return hitpoint != null ? hitpoint.hpMax : 0; } }
	float displayValue;

	void Value(){
		displayValue += (targetValue - displayValue) * 0.2f;
		image.fillAmount = displayValue / maxValue;
	}

	public float nextHide;
	void Alpha(){
		if (time < nextHide) {
			image.color = Color.white;
		} else {
			Color c = image.color;
			c.a *= 0.9f;
			image.color = c;
			if (c.a <= 0.01f) {
				gameObject.SetActive (false);
			}
		}
	}

	public void WakeBar(){
		gameObject.SetActive (true);
		nextHide = time + 3f;
	}

}