using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class PaddleSetup : NetworkBehaviour {

	void Start () {

		if (isLocalPlayer) {
			Paddle paddle = GetComponent<Paddle> ();
			paddle.playerId = CustomNetworkManager.playerCount;

			PaddleController controller = GetComponent<PaddleController> ();
			controller.enabled = true;


			if (paddle.playerId == 2) {
				controller.inverse = true;
				Camera.main.transform.Rotate (new Vector3 (0, 0, 180));
			} 
		}
	}

	public override void OnStartLocalPlayer()
	{
		GetComponent<SpriteRenderer>().color = Color.blue;
	}

}
