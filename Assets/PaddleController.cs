using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class PaddleController : NetworkBehaviour {

	private float speed = 10.0f;
	public bool inverse;

	void Start () {
		Input.multiTouchEnabled = true; //enabled Multitouch
	}

	// Update is called once per frame
	void Update () {
		if (!isLocalPlayer) {
			return;
		} 

		if (Input.GetKey (KeyCode.A) ) {
			MoveLeft ();
		}

		if (Input.GetKey (KeyCode.D)) {
			MoveRight ();
		}

		foreach (Touch touch in Input.touches) {
			if (touch.position.x < Screen.width/2) {
				MoveLeft();
			}
			else if (touch.position.x > Screen.width/2) {
				MoveRight();
			} 
		}
	}

	private void MoveLeft() {
		if (inverse) {
			CmdMoveRight ();
		} else {
			CmdMoveLeft ();
		}
	}
	private void MoveRight() {
		if (inverse) {
			CmdMoveLeft ();
		} else {
			CmdMoveRight ();
		}
	}

	[Command]
	public void CmdMoveLeft() {

		Vector3 direction = !inverse ? Vector3.left : Vector3.right;

		transform.position += direction * speed * Time.deltaTime;
	}

	[Command]
	public void CmdMoveRight() {
		Vector3 direction = !inverse ? Vector3.right : Vector3.left;

		transform.position += direction * speed * Time.deltaTime;
	}
}
