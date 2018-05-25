using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecruitChoice : UI {

	public AvailableCraft availableCraft;
	public Image craftImage,lockImage;
	public bool selected;

	public void Init(AvailableCraft craft){
		availableCraft = craft;
		gameObject.SetActive (true);
		gameObject.name = craft.craftName;
		craftImage.sprite = craft.sprite;

		gameObject.GetComponent<Button> ().onClick.AddListener (OnRecruitChoiceClicked);

		SetChoice ();
	}

	void OnRecruitChoiceClicked(){
		//Debug.Log (craft.craftName);
		panel.recruitPanel.OnChoiceSelected(this);
		SetChoice ();
	}

	public void SetChoice(){
		gameObject.GetComponent<CanvasGroup> ().alpha = selected? 1f: 0.5f;
		lockImage.gameObject.SetActive (availableCraft.locked);
	}
}