using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class PaddleController : NetworkBehaviour {

	private float speed = 10.0f;

	void Start () {
		Input.multiTouchEnabled = true; //enabled Multitouch
	}

	// Update is called once per frame
	void Update () {
		if (!isLocalPlayer) {
			return;
		} 

		if (Input.GetKey (KeyCode.A) ) {
			CmdMoveLeft ();
		}


		if (Input.GetKey (KeyCode.D)) {
			CmdMoveRight ();
		}

		foreach (Touch touch in Input.touches) {
			if (touch.position.x < Screen.width/2) {
				CmdMoveLeft();
			}
			else if (touch.position.x > Screen.width/2) {
				CmdMoveRight();
			} 
		}
	}		

	[Command]
	public void CmdMoveLeft() {
		transform.position += Vector3.left * speed * Time.deltaTime;
	}

	[Command]
	public void CmdMoveRight() {
		transform.position += Vector3.right * speed * Time.deltaTime;
	}
}
