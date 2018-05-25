﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Panel : UI {

	public GameObject[] UIObjects;
	public Text[] messages;
	public RecruitPanel recruitPanel;

	public void ButtonPressed(string panelButton){
		//Debug.Log (panelButton);
		switch (panelButton) {
		case "Recruit":
			recruit.HandleRecruitButtonPressed ();
			break;
		case "Bot":
			main.HandleBotButtonPressed ();
			break;
		}
	}

	public void SetPanel(){
		var phase = main.gamePhase;

		UIObjects[(int)UIObject.TopMessage].SetActive(phase == Phase.Recruit);
		UIObjects[(int)UIObject.MidMessage].SetActive(phase == Phase.Entry || phase == Phase.Battle);
		UIObjects[(int)UIObject.BotButton].SetActive(phase == Phase.Entry || phase == Phase.Recruit);
		UIObjects[(int)UIObject.Recruit].SetActive(phase == Phase.Recruit);

		messages [(int)Message.Mid].text =
			phase == Phase.Entry ? "Space battle v1.0" :
			"";

		messages [(int)Message.RecruitDesc].text =
			phase == Phase.Recruit ? "Recruit your fleet" :
			"";

		messages [(int)Message.BotButton].text =
			phase == Phase.Entry ? "Start" :
			"Next wave (" + (center.wave.waveCount + 1) + ")";

		switch (phase) {
		case Phase.Entry:
			
			break;
		case Phase.Recruit:
			
			break;
		case Phase.Battle:
			StartCoroutine (BattleMessageRoutine ());
			break;
		}
		UpdateRecruitButton ();
	}

	public void UpdatePanel(){
		UpdateTopMessage ();	
		UpdateRecruitButton ();
		recruitPanel.UpdateInformation ();
	}

	void UpdateTopMessage(){
		messages [(int)Message.Top].text =
			main.gamePhase == Phase.Recruit ? "$ " + recruit.credits :
			"";
	}

	public void UpdateRecruitButton(){
		panel.messages [(int)Message.RecruitButton].text = 
			(recruitPanel.selectedChoice == null || !recruitPanel.selectedAvailableCraft.locked)? 
			"Recruit (" + recruit.craftCount + "/" + recruit.craftMax + ")":
			"Unlock";
	}

	IEnumerator BattleMessageRoutine(){
		messages [(int)Message.Mid].text = "Wave " + wave.waveCount;
		yield return new WaitForSeconds (3);
		messages [(int)Message.Mid].text = "";

		main.SetPhase (Phase.Recruit);
	}

}