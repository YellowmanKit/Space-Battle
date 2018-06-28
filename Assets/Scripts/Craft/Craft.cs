using System.Collections;
using UnityEngine;

public enum Class {
	Drone,
	Fighter,
	Cruiser,
	Battleship
}

public enum Type {
	Attack,
	Defence,
	Support
}

public abstract class Craft : Ref {

	protected Rigidbody2D rb { get { return GetComponent<Rigidbody2D> (); } }
	protected Move move { get { return GetComponent<Move>(); } }
	protected Hitpoint hitpoint { get { return GetComponentInParent<Hitpoint> (); } }
	protected Shield shield { get { return GetComponentInChildren<Shield> (); } }
	protected State state { get { return GetComponent<State>(); } }
	protected Pilot pilot { get { return GetComponentInParent<Pilot>(); } }
	protected SpriteRenderer sr { get { return GetComponent<SpriteRenderer>(); } }

	public bool isPlayer { get { return pilot.side == Side.Player; } }

	public static Class CraftNameToClass(CraftName craftName){
		switch (craftName) {
		case CraftName.Bee:
			return Class.Drone;
		case CraftName.Beatle:
			return Class.Drone;
		case CraftName.Bat:
			return Class.Fighter;
		case CraftName.Pigeon:
			return Class.Fighter;
		case CraftName.Eagle:
			return Class.Fighter;
		case CraftName.Eel:
			return Class.Cruiser;
		case CraftName.Dolphin:
			return Class.Cruiser;
		case CraftName.HumpbackWhale:
			return Class.Battleship;
		case CraftName.BlueWhale:
			return Class.Battleship;
		}
		return Class.Drone;
	}

}
