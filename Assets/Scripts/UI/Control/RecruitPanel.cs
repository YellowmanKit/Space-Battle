using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecruitPanel : UI {

	public Transform choices { get { return transform.GetChild (0); } }
	GameObject choicePrefab { get { return choices.GetChild (0).gameObject; } }

	public RecruitChoice selectedChoice;
	public AvailableCraft selectedAvailableCraft { get { return selectedChoice.availableCraft; } }

	public void InitRecriutChoices(Dictionary<CraftName, AvailableCraft> availableCrafts){
		foreach(KeyValuePair<CraftName , AvailableCraft> entry in availableCrafts){
			PushChoice (entry.Value);
		}
		choicePrefab.SetActive (false);
	}

	void PushChoice(AvailableCraft craft){
		if (craft.enemyOnly) {
			return;
		}
		var choice = Instantiate (choicePrefab, choices) as GameObject;
		choice.GetComponent<RecruitChoice> ().Init (craft);
	}

	public void OnChoiceSelected(RecruitChoice choice){
		if (selectedChoice != null) {
			selectedChoice.selected = false;
			selectedChoice.SetChoice ();
		}
		selectedChoice = choice;
		choice.selected = true;
		UpdateInformation ();

		panel.UpdateRecruitButton ();
	}

	public void UpdateInformation(){
		if (selectedChoice == null) {
			panel.messages [(int)Message.RecruitDesc].text = "Recruit your fleet";
			return;
		}
		selectedChoice.SetChoice ();
		panel.messages [(int)Message.RecruitDesc].text = GenerateDescription();
	}

	string GenerateDescription(){
		string description = "";
		description += selectedAvailableCraft.craftClass.ToString() + " - " + selectedAvailableCraft.craftName;
		description += !selectedAvailableCraft.locked ? "\n" : " (locked)\n";
		description += selectedAvailableCraft.description + "\n";
		description += 
			!selectedAvailableCraft.locked? "\nCost: $" + selectedAvailableCraft.cost.ToString ():
			"\nUnlock cost: $" + selectedAvailableCraft.unlockCost.ToString ();
		return description;
	}

}