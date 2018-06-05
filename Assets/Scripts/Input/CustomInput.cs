﻿using System.Collections;
using UnityEngine;

public class CustomInput : Ref {

	public Vector2 target;
	public ParticleSystem onClick;

	float nextTouch;

	void Update(){
		GetTargetByTouch ();
		GetTargetByClick ();
	}

	void GetTargetByTouch(){
		if (Input.touchCount  == 1 && time > nextTouch) {
			
			Ray ray = Camera.main.ScreenPointToRay (Input.touches [0].position);
			RaycastHit2D hit = Physics2D.Raycast (ray.origin, ray.direction, 100f, uiLayer);

			if (hit != null && hit.collider.gameObject.name == "Area") {
				target = new Vector2 (hit.point.x, hit.point.y);
				EmitOnclick ();
			}

			nextTouch = time + 0.1f;
		}
	}

	void GetTargetByClick(){
		if (Input.GetKey(KeyCode.Mouse0) == true && time > nextTouch) {
			
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit2D hit = Physics2D.Raycast (ray.origin, ray.direction, 100f, uiLayer);

			if (hit != null && hit.collider.gameObject.name == "Area") {
				target = new Vector2 (hit.point.x, hit.point.y);
				EmitOnclick ();
			}

			nextTouch = time + 0.1f;
		}
	}

	void EmitOnclick (){
		onClick.transform.position = new Vector3 (target.x, target.y, 0f);
		onClick.Play ();
	}
}