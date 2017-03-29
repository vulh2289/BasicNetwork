using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class Ball : NetworkBehaviour {

	private float speed;
	public GameManager gameManager;

	[SyncVar]
	public int lastTouchPlayerId = -1;

	// Use this for initialization
	void Start () {
		gameManager = FindObjectOfType<GameManager> ();
		CmdChangeSpeed (5f);
		GetComponent<Rigidbody2D> ();

		foreach (Paddle paddle in GameManager.paddles) {
			if (paddle.isBallAssigned) {
				lastTouchPlayerId = paddle.paddleClient.playerId;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	[Command]
	public void CmdChangeSpeed(float newSpeed) {
		this.speed = newSpeed;
		GetComponent<Rigidbody2D> ().velocity = GetComponent<Rigidbody2D> ().velocity.normalized * speed;
	}

//	[Command]
//	public void CmdChangeLastTouch(int playerId) {
//		lastTouchPlayerId = playerId;
//	}

	[Command]
	public void CmdFire (Vector2 touchPoint)
	{
		GetComponent<Rigidbody2D> ().velocity = touchPoint;
//		GetComponent<Rigidbody2D> ().AddTorque(1f, ForceMode2D.Force);
		CmdChangeSpeed (speed);
	}

	[Command]
	public void CmdSetPosition (Vector2 position)
	{
		GetComponent<Rigidbody2D> ().position = position;
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
			GameManager.paddles[0].paddleClient.RpcOnCollision(CollisionType.CONCEDE);
			GameManager.paddles[1].paddleClient.RpcOnCollision(CollisionType.GOAL);
		} else if (coll.gameObject.tag == "Player2Goal") {
			GameManager.paddles[0].paddleClient.RpcOnCollision(CollisionType.GOAL);
			GameManager.paddles[1].paddleClient.RpcOnCollision(CollisionType.CONCEDE);
		} 
		// Send out CollisionType

		else if (coll.gameObject.tag == "Player") {
			if (GameManager.paddles [0].paddleClient.playerId == lastTouchPlayerId) {
				GameManager.paddles [0].paddleClient.RpcOnCollision (CollisionType.BALL_TO_PADDLE);
				GameManager.paddles [1].paddleClient.RpcOnCollision (CollisionType.BALL_TO_OPP_PADDLE);

				lastTouchPlayerId = GameManager.paddles [0].paddleClient.playerId;
			} else {
				GameManager.paddles[0].paddleClient.RpcOnCollision(CollisionType.BALL_TO_OPP_PADDLE);
				GameManager.paddles[1].paddleClient.RpcOnCollision(CollisionType.BALL_TO_PADDLE);

				lastTouchPlayerId = GameManager.paddles [1].paddleClient.playerId;
			}
		} 

		else {
//			addRandomDirection ();
//			GetComponent<Rigidbody2D> ().AddTorque( Random.Range (0, 10), ForceMode2D.Force);
		}
	}

}
