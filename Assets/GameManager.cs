using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : NetworkBehaviour
{
	public GameObject ballPrefab;

	public enum GameStates {
		WAITING_FOR_PLAYERS, READY, STARTED, PLAYER1_WIN, PLAYER2_WIN
	}

	public static GameStates gameState;
	public static Paddle[] paddles = {null, null};

	public override void OnStartServer()
	{
		gameState = GameStates.WAITING_FOR_PLAYERS;
		InvokeRepeating("CreateItems", 2.0f, 10.0f);
	}

	public void Update() {
		if (isServer) {
			switch (gameState) {
			case GameStates.WAITING_FOR_PLAYERS:
				waitingForPlayer ();
				break;
			case GameStates.READY:
				waitToPlay ();
				break;
			case GameStates.STARTED:
				updateGame ();
				break;
			}
		}
	}

	public void setWinner (int i)
	{
		paddles [i].win();
		int loseIndex = i == 0 ? 1 : 0;
		paddles [loseIndex].lose();
	}
		
	void waitingForPlayer ()
	{
		Paddle[] tempPaddles = FindObjectsOfType<Paddle> ();
		if (tempPaddles.Length == 2) {

			// Change Game State
			gameState = GameStates.READY;

			// Create ball
			CreateBall();

			// Make sure the array correctly sorted by id for easy accessing
			if (tempPaddles [0].paddleController.playerId == 0) {
				paddles [0] = tempPaddles [0];
				paddles [1] = tempPaddles [1];
			} else {
				paddles [0] = tempPaddles [1];
				paddles [1] = tempPaddles [0];
			}

			// Give ball to host
			paddles[0].assignBall();
		}
	}

	void waitToPlay ()
	{
	}

	void updateGame ()
	{

	}

	void informWinner ()
	{

	}

	// Private

	void CreateBall() {
		var ball = Instantiate (ballPrefab, new Vector3 (0, 0, 0), 
			          Quaternion.identity) as GameObject;
		NetworkServer.Spawn (ball);
	}

	void CreateItems() {
		if (gameState == GameStates.STARTED) {
//			var ball = Instantiate (ballPrefab, new Vector3 (0, 0, 0), 
//				Quaternion.identity) as GameObject;
//			NetworkServer.Spawn (ball);
		}
	}
}


