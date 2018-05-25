using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : Control {

	public Phase gamePhase;

	public void Start(){
		SetPhase (Phase.Entry);
	}

	public void HandleBotButtonPressed(){

		switch (gamePhase) {
		case Phase.Entry:
			SetPhase (Phase.Recruit);
			break;
		case Phase.Recruit:
			wave.NextWave ();
			SetPhase (Phase.Battle);
			break;
		case Phase.Battle:
			break;
		}

	}

	public void SetPhase(Phase phase){
		gamePhase = phase;
		panel.SetPanel ();
	}

}
