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

	protected Crafts craftPool { get { return center.craftPool; } }
	protected Projectiles projectilePool { get { return center.projectilePool; } }
	protected Particles particlePool { get { return center.particlePool; } }

	protected Search search { get { return center.search; } }
	protected FleetManage fleetManage { get { return center.fleetManage; } }

	protected int uiLayer { get { return 1 << 5; } }
	protected float time { get { return Time.timeSinceLevelLoad; } }
	protected float deltaTime { get { return Time.deltaTime; } }

	protected int playerRaycastLayer { get { return(
		    1 << LayerMask.NameToLayer ("EnemyDrone") |
			1 << LayerMask.NameToLayer ("EnemyFighter") |
			1 << LayerMask.NameToLayer ("EnemyCruiser") |
			1 << LayerMask.NameToLayer ("EnemyBattleship") |
			1 << LayerMask.NameToLayer ("EnemyShield")
		);}}

	protected int enemyRaycastLayer { get { return(
		1 << LayerMask.NameToLayer ("PlayerDrone") |
		1 << LayerMask.NameToLayer ("PlayerFighter") | 
		1 << LayerMask.NameToLayer ("PlayerCruiser") |
		1 << LayerMask.NameToLayer ("PlayerBattleship") |
		1 << LayerMask.NameToLayer ("PlayerShield")
	);}}

}