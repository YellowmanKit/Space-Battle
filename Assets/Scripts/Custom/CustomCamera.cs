using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCamera : Ref {

	Camera cam { get { return GetComponent<Camera>(); } }

	void Start(){
		UpdateCamera ();
	}

	public float camMin,camMax;
	public float targetCamSize;
	public void UpdateCamera(){
		targetCamSize = camMin + Mathf.Clamp ((camMax - camMin) * center.scale, 0f, camMax - camMin);
	}

	public float scaleSpeed;
	float actualSize { get { return cam.orthographicSize; } }
	void Update(){
		var diff = targetCamSize - actualSize;
		cam.orthographicSize = actualSize + diff * deltaTime * scaleSpeed;
	}

}
