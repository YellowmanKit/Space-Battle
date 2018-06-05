﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Recruit : Control {

	public int credits,craftMax;
	public int craftCount { get { return craftPool.pools [PoolType.Player].Count; } }

	public void HandleRecruitButtonPressed(){
		if (main.gamePhase != Phase.Recruit || panel.recruitPanel.selectedChoice == null) {
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
		credits -= craft.cost;

		craftPool.Spawn (craft.craftName, Side.Player);
		panel.UpdatePanel ();
	}

}