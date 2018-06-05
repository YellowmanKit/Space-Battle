using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public enum UIObject {
	TopMessage,
	MidMessage,
	BotButton,
	Recruit
}

public enum Message {
	Top,
	Mid,
	BotButton,
	RecruitDesc,
	RecruitButton
}

public abstract class UI : Ref {

	protected Image image { get { return GetComponent<Image> (); } }
	protected RectTransform rt { get { return GetComponent<RectTransform> (); } }

}
