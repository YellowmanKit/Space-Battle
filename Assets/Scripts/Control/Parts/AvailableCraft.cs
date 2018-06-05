using System.Collections;
using UnityEngine;

public class AvailableCraft : Control {

	public Class craftClass;
	public Type craftType;
	public Sprite sprite;
	public CraftName craftName;
	public string description;
	public int cost,unlockCost;
	public bool locked,playerOnly,enemyOnly;
	public GameObject prefab;

}
