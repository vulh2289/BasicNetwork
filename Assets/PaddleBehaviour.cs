using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PaddleBehaviour : NetworkBehaviour {

//	[SyncVar]
	private Text text;
	private GameObject item1;

	public void Start() {
		text =  FindObjectOfType<Text>();
		text.text = "";
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
		paddle.powerSpeed += 1f;
		if (isLocalPlayer) {
		}
	}

	[ClientRpc]
	public void RpcIncreaseSpeedPowerBy (float moreSpeed)
	{
		Paddle paddle = gameObject.GetComponent<Paddle>();
		paddle.powerSpeed = Mathf.Clamp(paddle.powerSpeed + moreSpeed, 1f, 15f);
	}

	[SyncVar]
	public int playerId = -1;

	public void CmdSetPlayerId(int playerId) {
		this.playerId = playerId;
	}

}
