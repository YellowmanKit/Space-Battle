using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : Control {

	public Phase gamePhase;

	public void Start(){
		SetPhase (Phase.Entry);
	}

	void Update(){
		if(Input.GetKey(KeyCode.Escape)){
			SceneManager.LoadScene ("main");
		}
	}

	public void HandleBotButtonPressed(){

		switch (gamePhase) {
		case Phase.Entry:
			SetPhase (Phase.Recruit);
			break;
		case Phase.Recruit:
			if (fleetManage.playerExist) {
				wave.NextWave ();
				SetPhase (Phase.Battle);
			}
			break;
		case Phase.Battle:
			break;
		case Phase.GameOver:
			SceneManager.LoadScene ("main");
			break;
		}
	}

	public void GameOver(){
		SetPhase (Phase.GameOver);
	}

	public void SetPhase(Phase phase){
		gamePhase = phase;
		panel.SetPanel ();
	}

}