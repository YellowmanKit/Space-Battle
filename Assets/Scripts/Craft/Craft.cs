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

}
