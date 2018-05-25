using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : Ref {

	protected Rigidbody2D rb { get { return GetComponent<Rigidbody2D> (); } }

}