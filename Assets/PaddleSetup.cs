using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class PaddleSetup : NetworkBehaviour {

	public Camera camera1;
	public Camera camera2;

	public override void OnStartLocalPlayer()
	{
		PaddleController controller = GetComponent<PaddleController> ();
		PaddleBehaviour behaviour = GetComponent<PaddleBehaviour> ();
		controller.enabled = true;


		camera1 = GameObject.Find("Camera 1").GetComponent<Camera>();
		camera2 = GameObject.Find("Camera 2").GetComponent<Camera>();

		Vector3 tra = transform.position;

		if (transform.position.y > 0) {
			camera1.enabled = false;
			camera2.enabled = true;
			controller.inverse = true;
			behaviour.CmdSetPlayerId(2);
			GetComponent<SpriteRenderer>().color = Color.blue;
		} else {
			camera1.enabled = true;
			camera2.enabled = false;
			controller.inverse = false;
			behaviour.CmdSetPlayerId(1);
			GetComponent<SpriteRenderer>().color = Color.red;
		}
	}

}
