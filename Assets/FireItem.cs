using UnityEngine;
using System.Collections;

public class FireItem : AbstractItem {

	public float speed = 30f;

	public override void onAction (Paddle lastTouchedPlayer, Paddle opponent, Ball ball) {
		lastTouchedPlayer.paddleClient.RpcIncreaseSpeedPowerBy (speed);
	}
}
