using UnityEngine;
using System.Collections;

public class FireItem : AbstractItem {

	public override void onRetrieve (Paddle lastTouchedPlayer, Paddle opponent, Ball ball){
		lastTouchedPlayer.paddleClient.RpcAssignItem (ActivateItem.FIRE);
	}
}
