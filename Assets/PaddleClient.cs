using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;
using UnityEngine.UI;

public class PaddleClient : NetworkBehaviour
{
	[SyncVar]
	public int playerId = -1;

	private Text text;
	private GameObject item1;

	public Camera camera1;
	public Camera camera2;

	// Attributes

	[SyncVar]
	public int health = 3;

	[SyncVar]
	public float powerSpeed = 5f;

	public void Start() {
		text =  FindObjectOfType<Text>();
		text.text = "";
	}

	public override void OnStartLocalPlayer()
	{
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
	public void RpcLose ()
	{
		if (isLocalPlayer)
			text.text = "Loser!";
	}

	[ClientRpc]
	public void RpcWin ()
	{
		if (isLocalPlayer)
			text.text = "Winner!";
	}

	[ClientRpc]
	public void RpcAssign (GameObject item)
	{
		Paddle paddle = gameObject.GetComponent<Paddle>();
		//		SpeedItem speedItem = item.GetComponent<SpeedItem> ();
		paddle.paddleClient.powerSpeed += 1f;
		if (isLocalPlayer) {
		}
	}

	[ClientRpc]
	public void RpcIncreaseSpeedPowerBy (float moreSpeed)
	{
		Paddle paddle = gameObject.GetComponent<Paddle>();
		paddle.paddleClient.powerSpeed = Mathf.Clamp(paddle.paddleClient.powerSpeed + moreSpeed, 1f, 15f);
	}

	public void CmdSetPlayerId(int playerId) {
		this.playerId = playerId;
	}
		
}


