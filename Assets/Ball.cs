using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class Ball : NetworkBehaviour {

	private float speed;

	[SyncVar]
	private int lastTouchPlayerId = -1;
	

	// Use this for initialization
	void Start () {
		CmdChangeSpeed (5f);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	[Command]
	public void CmdChangeSpeed(float newSpeed) {
		this.speed = newSpeed;
		GetComponent<Rigidbody2D> ().velocity = GetComponent<Rigidbody2D> ().velocity.normalized * speed;
	}

	[Command]
	public void CmdChangeLastTouch(int playerId) {
		lastTouchPlayerId = playerId;
	}

	[Command]
	public void CmdFire (Vector2 touchPoint)
	{
		GetComponent<Rigidbody2D> ().velocity = touchPoint;
		CmdChangeSpeed (speed);
	}

	void addRandomDirection ()
	{
		Vector2 currentVelocity = GetComponent<Rigidbody2D> ().velocity;

		float newY = currentVelocity.y;

		if (currentVelocity.y >= 0) {
			newY += Random.Range (0, 10);
		} else {
			newY -= Random.Range (0, 10);
		}

		float newX = currentVelocity.x;

		if (currentVelocity.x >= 0) {
			newX += Random.Range (0, 10);
		} else {
			newX -= Random.Range (0, 10);
		}

		GetComponent<Rigidbody2D> ().velocity = new Vector2 (newX, newY);
		CmdChangeSpeed (speed);
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player1Goal") {
			print ("hit player 1 goal");
		} else if (coll.gameObject.tag == "Player2Goal") {
			print ("hit player 2 goal");
		} else {
			addRandomDirection ();
		}
	}
}
