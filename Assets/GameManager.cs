using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class GameManager : NetworkBehaviour
{
	public GameObject ballPrefab;

	public override void OnStartServer()
	{
		var ball = Instantiate(ballPrefab, new Vector3(0,0,0), 
			Quaternion.identity) as GameObject;

		NetworkServer.Spawn(ball);
	}
}


