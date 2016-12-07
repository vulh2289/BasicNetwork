using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class GameManager2 : NetworkBehaviour {

	public GameObject ballPrefab;

	public override void OnStartServer()
	{
		var ball = Instantiate(ballPrefab, 
			transform.position + new Vector3(0,0,0), 
			Quaternion.identity) as GameObject;


		ball.GetComponent<Rigidbody2D> ().velocity = new Vector3 (2f, 10f, 0f);

		NetworkServer.Spawn(ball);
	}
}
