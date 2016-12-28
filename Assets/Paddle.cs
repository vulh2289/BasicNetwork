using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class Paddle : NetworkBehaviour {

	void Start () {

		if (isLocalPlayer) {
			PaddleController controller = GetComponent<PaddleController> ();
			controller.enabled = true;
			controller.playerId = CustomNetworkManager.playerCount;

			if (controller.playerId == 2) {
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
