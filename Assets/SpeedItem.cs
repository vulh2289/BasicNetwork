using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SpeedItem : AbstractItem {

	public float speed = 1f;
		
	public override void onRetrieve (Paddle lastTouchedPlayer, Paddle opponent, Ball ball){
		lastTouchedPlayer.paddleClient.RpcIncreaseSpeedPowerBy (speed);
	}
}
