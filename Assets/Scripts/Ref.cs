using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ref : MonoBehaviour {

	protected Center center { get { return Center.instance; } }
	protected Main main { get { return center.main; } }
	protected Wave wave { get { return center.wave; } }
	protected Recruit recruit { get { return center.recruit; } }

	protected Panel panel { get { return center.panel; } }
	protected CustomInput input { get { return center.input; } }

	protected CraftPool craftPool { get { return center.craftPool; } }
	protected ProjectilePool projectilePool { get { return center.projectilePool; } }
	protected ParticlePool particlePool { get { return center.particlePool; } }

	protected Search search { get { return center.search; } }
	protected FleetManage fleetManage { get { return center.fleetManage; } }

	protected int uiLayer { get { return 1 << 5; } }
	protected float time { get { return Time.timeSinceLevelLoad; } }

}