using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : NetworkBehaviour
{
	public GameObject ballPrefab;
	public GameObject[] itemPrefabs;

	public enum GameStates {
		WAITING_FOR_PLAYERS, READY, STARTED, PLAYER1_WIN, PLAYER2_WIN
	}

	public static GameStates gameState;
	public static Paddle[] paddles = {null, null};
	public static Ball ball;

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

//	public void setWinner (int playerId)
//	{
//		foreach(Paddle item in paddles) 
//		{
//			if (item.paddleClient.playerId == playerId) {
//				item.paddleClient.RpcWin ();
//			} else {
//				item.paddleClient.RpcLose ();
//			}
//		}
//	}

	public void assignItem(GameObject item) {
		AbstractItem sI = item.GetComponent<AbstractItem> ();
		sI.onRetrieve (getLastTouchedPlayer (), getOpponentPlayer (), ball);
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
			if (tempPaddles [0].paddleClient.playerId == 1) {
				paddles [0] = tempPaddles [0];
				paddles [1] = tempPaddles [1];
			} else {
				paddles [0] = tempPaddles [1];
				paddles [1] = tempPaddles [0];
			}
				
//			paddles [0].paddleClient.CmdSetPlayerId (1);
//			paddles [1].paddleClient.CmdSetPlayerId (2);

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
		var ballObj = Instantiate (ballPrefab, new Vector3 (0, 0, 0), 
			          Quaternion.identity) as GameObject;
		NetworkServer.Spawn (ballObj);

		ball = FindObjectOfType<Ball> ();

	}

	void CreateItems() {
		if (gameState == GameStates.STARTED) {

			if (itemPrefabs.Length == 0) {
				return;
			}

			int randomItem = Random.Range (0, itemPrefabs.Length - 1);

			if (randomItem != -1) {
				var itemObj = Instantiate (itemPrefabs[randomItem], new Vector3 (0, 0, 0), 
					Quaternion.identity) as GameObject;
				NetworkServer.Spawn (itemObj);
			}
		}
	}

	Paddle getLastTouchedPlayer () {
		int playerId = ball.lastTouchPlayerId;
		if (paddles [0].paddleClient.playerId == playerId)
			return paddles [0];
		else
			return paddles [1];
	}

	Paddle getOpponentPlayer () {
		int playerId = ball.lastTouchPlayerId;
		if (paddles [0].paddleClient.playerId == playerId)
			return paddles [1];
		else
			return paddles [0];
	}
}


