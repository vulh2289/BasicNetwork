using System;
using UnityEngine.Networking;
using UnityEngine;
using System.Collections.Generic;

public class CustomNetworkManager : NetworkManager
{
	public static int playerCount;

	public Transform pos1;
	public Transform pos2;

	void Start() {
	}

	public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId)
	{
		Transform transform;
		playerCount++;

		if (playerCount == 1) {
			transform = pos1;
		} else {
			transform = pos2;
		}

		GameObject player = (GameObject)Instantiate(playerPrefab, transform.position, transform.rotation);

		NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
	}

	public override void OnStopClient() {
		playerCount--;
	}

}

