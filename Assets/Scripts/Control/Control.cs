﻿using System.Collections;
using UnityEngine;

public enum Phase {
	Entry,
	Recruit,
	Battle,
	GameOver
}
	
public abstract class Control : Ref {

	protected PoolType SideToPoolType(Side side){
		return side == Side.Enemy ? PoolType.Enemy : PoolType.Player;
	}

	protected CraftName StringToCraftName(string name){
		return (CraftName)System.Enum.Parse (typeof(CraftName), name);
	}

}
