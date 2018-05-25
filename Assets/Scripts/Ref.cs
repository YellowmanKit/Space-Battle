using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ref : MonoBehaviour {

	protected Center center { get { return GameObject.FindWithTag ("Center").GetComponent<Center> (); } }
	protected Main main { get { return center.main; } }
	protected Panel panel { get { return center.panel; } }
	protected Wave wave { get { return center.wave; } }
	protected CustomInput input { get { return center.input; } }
	protected Recruit recruit { get { return center.recruit; } }
	protected Dictionary<string, AvailableCraft> availableCrafts { get { return center.availableCrafts; } }

	protected int uiLayer { get { return 1 << 5; } }

}