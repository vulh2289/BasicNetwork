using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class GameManager : NetworkBehaviour
{
	public GameObject ballPrefab;

	public enum GameStates {
		WAITING_FOR_PLAYERS, READY, STARTED, PLAYER1_WIN, PLAYER2_WIN
	}
		
	public static GameStates gameState;
	public static Paddle[] paddles;

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
			case GameStates.PLAYER1_WIN:
				informWinner ();
				break;
			case GameStates.PLAYER2_WIN:
				informWinner ();
				break;
			}
		}
	}
		
	void waitingForPlayer ()
	{
		paddles = FindObjectsOfType<Paddle> ();
		if (paddles.Length == 1) {

			// Change Game State
			gameState = GameStates.READY;

			// Create ball
			CreateBall();

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


