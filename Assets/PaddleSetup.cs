using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class PaddleSetup : NetworkBehaviour {

	public Camera camera1;
	public Camera camera2;

	public override void OnStartLocalPlayer()
	{
		GetComponent<SpriteRenderer>().color = Color.blue;
		PaddleController controller = GetComponent<PaddleController> ();
		controller.enabled = true;


		camera1 = GameObject.Find("Camera 1").GetComponent<Camera>();
		camera2 = GameObject.Find("Camera 2").GetComponent<Camera>();

		Vector3 tra = transform.position;

		if (transform.position.y > 0) {
			camera1.enabled = false;
			camera2.enabled = true;
			controller.inverse = true;
			controller.playerId = 2;
		} else {
			camera1.enabled = true;
			camera2.enabled = false;
			controller.inverse = false;
			controller.playerId = 1;
		}
	}

}
