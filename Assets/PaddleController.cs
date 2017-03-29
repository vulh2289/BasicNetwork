using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;
using UnityEngine.UI;

public class PaddleController : NetworkBehaviour {

	private float speed = 10.0f;
	public bool inverse;

	private Button btn1;
	private Button btn2;

	private Paddle paddle;

	void Start () {
		paddle = FindObjectOfType<Paddle> ();
		Input.multiTouchEnabled = true; //enabled Multitouch

		btn1 =  GameObject.Find("btn_item1").GetComponent<Button>();
		btn2 =  GameObject.Find("btn_item2").GetComponent<Button>();
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

	public void useItem1() {
		if (!isLocalPlayer) {
			return;
		} 

		if (paddle.item1 != ActivateItem.NONE) {
			CmdActivateItem (paddle.item1);
			paddle.item1 = ActivateItem.NONE;
		}
	}

	public void useItem2() {
		if (!isLocalPlayer) {
			return;
		} 

		if (paddle.item2 != ActivateItem.NONE) {
			CmdActivateItem (paddle.item2);
			paddle.item2 = ActivateItem.NONE;
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

	[Command]
	public void CmdActivateItem(ActivateItem item) {
		Debug.Log (item);
	}
}
