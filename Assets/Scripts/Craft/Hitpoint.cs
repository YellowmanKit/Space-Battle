using System.Collections;
using UnityEngine;

public class Hitpoint : Craft {

	public int hp,hpMax;
	public Bar hpBar;
	public float barDistance;

	public void Start(){
		hp = hpMax;
		InitHpBar ();
	}

	void InitHpBar(){
		if (hpBar == null) {
			hpBar = center.battleUI.CreateHpBar ();
			hpBar.InitBar (this);
		}
	}

	public void TakeDamage(int damage){
		hp -= damage;
		hpBar.nextHide = time + 3f;
		state.OnHit ();
		if (hp <= 0) {
			state.CraftDestroyed ();
		}
	}



}
