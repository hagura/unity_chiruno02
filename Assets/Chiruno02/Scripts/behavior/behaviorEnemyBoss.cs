using UnityEngine;
using System.Collections;

public class behaviorEnemyBoss : behaviorEmeny {

	// Update is called once per frame
	protected override void Update () {

		base.Update();

		if (m_waitDestroy <= 0) {

			scene.GetComponent<CGame>().ChangeMode(CGame.GAMEMODE.MODE_CLEAR);
		}
	}
}
