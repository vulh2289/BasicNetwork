using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;
using UnityEngine.UI;

public class PaddleClient : NetworkBehaviour
{
	public int playerId = -1;

	private Text text;
	public Camera camera1;
	public Camera camera2;

	Paddle paddle;

	// Attributes

	[SyncVar]
	public int health = 3;

	[SyncVar]
	public float powerSpeed = 5f;

	public float tmpPowerSpeed = 0f;

	public void Start() {
		text =  FindObjectOfType<Text>();
		text.text = "";
	}

	public override void OnStartLocalPlayer()
	{
		paddle = GetComponent<Paddle> ();

		PaddleController controller = GetComponent<PaddleController> ();
		controller.enabled = true;

		camera1 = GameObject.Find("Camera 1").GetComponent<Camera>();
		camera2 = GameObject.Find("Camera 2").GetComponent<Camera>();

		Vector3 tra = transform.position;

		if (transform.position.y > 0) {
			camera1.enabled = false;
			camera2.enabled = true;
			controller.inverse = true;
			CmdSetPlayerId(2);
			//			GetComponent<SpriteRenderer>().color = Color.blue;
		} else {
			camera1.enabled = true;
			camera2.enabled = false;
			controller.inverse = false;
			CmdSetPlayerId(1);
			//			GetComponent<SpriteRenderer>().color = Color.red;
		}
	}

	[ClientRpc]
	public void RpcOnCollision (CollisionType collisionType)
	{
		if (!isLocalPlayer) 
		{
			return;
		}

		foreach (ItemEffect ie in paddle.effects) {
			ie.OnCollision (collisionType);
		}

		text.text = "Loser!";

		switch (collisionType) 
		{
		case CollisionType.BALL_TO_PADDLE:
			float speed = tmpPowerSpeed > powerSpeed ? tmpPowerSpeed : powerSpeed;
			paddle.ball.CmdChangeSpeed (speed);
			break;
		case CollisionType.BALL_TO_OPP_PADDLE:
			break;
		case CollisionType.CONCEDE:
			text.text = "Loser!";
			break;
		case CollisionType.GOAL:
			text.text = "Winner!";
			break;
		}
	}

	[ClientRpc]
	public void RpcAssignItem (ActivateItem item)
	{
		Paddle paddle = gameObject.GetComponent<Paddle>();
		if (isLocalPlayer) {
			if (paddle.item1 == ActivateItem.NONE) {
				paddle.item1 = item;
			} else if (paddle.item2 == ActivateItem.NONE) {
				paddle.item2 = item;
			}
		}
	}

	[ClientRpc]
	public void RpcIncreaseSpeedPowerBy (float moreSpeed)
	{
		powerSpeed = Mathf.Clamp(powerSpeed + moreSpeed, 1f, 15f);
	}

	[Command]
	public void CmdSetPlayerId(int playerId) {
		this.playerId = playerId;
	}
}


