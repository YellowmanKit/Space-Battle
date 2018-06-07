﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Alpha {

	public int shieldHp,shieldHpMax;
	CircleCollider2D shieldCollider { get { return GetComponent<CircleCollider2D> (); } }

	void OnEnable(){
		Recharge ();
	}

	void Prewarm(){
	}

	void Recharge(){
		shieldHp = shieldHpMax;
	}

	Bar hpBar { get { return GetComponentInParent<Hitpoint> ().hpBar; } }
	public void TakeDamage(int damage){
		if (isDown) {
			return;
		}
		shieldHp -= damage;
		if (isDown) {
			shieldCollider.enabled = false;
		}
		OnHit ();
	}

	void Update(){
		AlphaUpdate ();
		ShieldCollider ();
	}

	void ShieldCollider(){
		if (!isDown && !shieldCollider.enabled) {
			shieldCollider.enabled = true;
		}
	}

	protected override float targetAlpha (){
		return !isDown? 0.05f: 0f;
	}

	protected override void OnAlphaZero (){
		return;
	}

	public void OnHit(){
		SetAlpha (0.5f);
	}

	public bool isDown { get { return shieldHp <= 0; } }
}