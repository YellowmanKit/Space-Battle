using System.Collections;
using UnityEngine;

public class Hitpoint : Craft {

	public float hp,hpMax;
	public Bar hpBar;
	public float barDistance,hideBarOnScale;
	public bool isDamaged { get { return hp < hpMax; } }

	public void OnEnable(){
		hp = hpMax;
		InitHpBar ();
	}

	bool noBar { get { return hideBarOnScale != 0 && hideBarOnScale < center.scale; } }
	void InitHpBar(){
		if (hpBar == null) {
			hpBar = center.battleUI.CreateHpBar ();
			hpBar.InitBar (this);
		} else {
			hpBar.Reset ();
		}
	}

	public void TakeDamage(float damage){
		//Debug.Log ("HP TakeDamage: " + damage);
		if (shield != null && !shield.isDown) {
			return;
		}
		hp -= damage;
		if (!noBar) {
			hpBar.WakeBar ();
		}
		state.OnHit ();
		if (hp <= 0) {
			damageToTakePerSecond = 0f;
			totalDamageToTake = 0f;
			state.CraftDestroyed ();
		}
	}

	public void Repaired(float repairValue){
		if (state.destroyed) {
			return;
		}
		hp += repairValue;
		if (!noBar) {
			hpBar.WakeBar ();
		}
		state.OnHit ();
	}

	float totalDamageToTake,damageToTakePerSecond;
	public void TakeDamageOverTime(float damage,float duration){
		//Debug.Log ("TakeDamageOverTime: " + damage + " duration: " + duration);
		damageToTakePerSecond += damage / duration;
		totalDamageToTake += damage;
	}

	public void RemoveDamageOverTime(float damage,float duration){
		//Debug.Log ("RemoveDamageOverTime: " + damage + " duration: " + duration);
		damageToTakePerSecond -= damage / duration;
		totalDamageToTake -= damage;
	}

	void Update(){
		DamageOvertime ();
	}

	float nextDamageOverTime;
	void DamageOvertime(){
		if (time > nextDamageOverTime && damageToTakePerSecond > 1f) {
			nextDamageOverTime = time + 0.2f;

			var damageToTake = damageToTakePerSecond * 0.2f;
			if (shield != null && !shield.isDown) {
				shield.TakeDamage (damageToTake * 0.5f);
			} else {
				TakeDamage (damageToTake);
			}

			totalDamageToTake -= damageToTake;

			if (totalDamageToTake <= 0f) {
				damageToTakePerSecond = 0f;
			}
		}
	}

}