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
		if (shield != null && !shield.isDown) {
			return;
		}
		hp -= damage;
		hpBar.nextHide = time + 3f;
		state.OnHit ();
		if (hp <= 0) {
			state.CraftDestroyed ();
		}
	}

	public void Repaired(int repairValue){
		if (state.destroyed) {
			return;
		}
		hp += repairValue;
		hpBar.nextHide = time + 3f;
		state.OnHit ();
	}

	public bool isDamaged { get { return hp < hpMax; } }

}
