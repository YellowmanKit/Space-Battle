using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : Alpha {

	public float shieldHp,shieldHpMax;
	CapsuleCollider2D shieldCollider { get { return GetComponent<CapsuleCollider2D> (); } }

	void OnEnable(){
		Recharge ();
	}

	void Prewarm(){
	}

	void Recharge(){
		gameObject.layer = LayerMask.NameToLayer ((isPlayer ? "Player" : "Enemy") + "Shield");
		shieldHp = shieldHpMax;
	}

	Bar hpBar { get { return GetComponentInParent<Hitpoint> ().hpBar; } }
	public void TakeDamage(float damage){
		//Debug.Log ("Shield TakeDamage: " + damage);
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
		//return !isDown? 0.05f: 0f;
		return 0f;
	}

	protected override void OnAlphaZero (){
		return;
	}

	public void OnHit(){
		SetAlpha (0.3f + 0.3f * shieldHp / shieldHpMax);
	}

	public bool isDown { get { return shieldHp <= 0; } }
}