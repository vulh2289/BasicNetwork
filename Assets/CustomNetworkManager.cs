using System;
using UnityEngine.Networking;
using UnityEngine;

public class CustomNetworkManager : NetworkManager
{
	public GameManager gameManager;

	void Start() {

		gameManager = GetComponent<GameManager> ();
		
	}

	public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
	{
		base.OnServerAddPlayer (conn, playerControllerId);
	}

}

