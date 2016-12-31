using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class Ball : NetworkBehaviour {

	private float speed;
	public GameManager gameManager;

	[SyncVar]
	private int lastTouchPlayerId = -1;
	

	// Use this for initialization
	void Start () {
		gameManager = FindObjectOfType<GameManager> ();
		CmdChangeSpeed (5f);
		GetComponent<Rigidbody2D> ();
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

	[Command]
	public void CmdSetPosition (Vector2 position)
	{
		GetComponent<Rigidbody2D> ().position = position;
	}
		
	public void CmdSetWinner(int playerIndex) {
		
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
		
		// Only do collision when game started
		if (GameManager.gameState != GameManager.GameStates.STARTED) {
			return;
		}

		if (coll.gameObject.tag == "Player1Goal") {
			gameManager.setWinner (1);
		} else if (coll.gameObject.tag == "Player2Goal") {
			gameManager.setWinner (0);
		} else {
			addRandomDirection ();
		}
	}
}
