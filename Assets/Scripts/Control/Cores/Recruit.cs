using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Recruit : Control {

	public int credits,craftMax,craftCount;

	public void HandleRecruitButtonPressed(){
		if (main.gamePhase != Phase.Recruit) {
			return;
		}
		var selectedCraft = panel.recruitPanel.selectedAvailableCraft;
		if (selectedCraft.locked) {
			if (credits < selectedCraft.unlockCost) {
				return;
			} else {
				OnCraftUnlocked (selectedCraft);
			}
		} else if (credits < selectedCraft.cost || craftCount == craftMax) {
			return;
		} else {
			OnCraftRecruit (selectedCraft);
		}
	}

	void OnCraftUnlocked(AvailableCraft craft){
		credits -= craft.unlockCost;
		craft.locked = false;

		panel.UpdatePanel ();
	}

	void OnCraftRecruit(AvailableCraft craft){
		craftCount++;
		credits -= craft.cost;

		panel.UpdatePanel ();
	}

}