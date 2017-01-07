using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PaddleBehaviour : NetworkBehaviour {

//	[SyncVar]
	private Text text;

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

	[SyncVar]
	public int playerId = -1;

	public void CmdSetPlayerId(int playerId) {
		this.playerId = playerId;
	}

}
