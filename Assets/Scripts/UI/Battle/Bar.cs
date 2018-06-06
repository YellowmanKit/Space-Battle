using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : UI {

	Hitpoint hitpoint;

	public void InitBar(Hitpoint _hitpoint){
		hitpoint = _hitpoint;
		rt.sizeDelta = new Vector2 (15f + hitpoint.hp * 0.5f , 2.5f);
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

	int targetValue { get { return hitpoint != null ? hitpoint.hp : 0; } }
	int maxValue { get { return hitpoint != null ? hitpoint.hpMax : 0; } }
	float displayValue;

	void Value(){
		displayValue += ((float)targetValue - displayValue) * 0.2f;
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
		}
	}

}