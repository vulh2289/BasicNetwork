using UnityEngine;
using System.Collections;

public class Paddle : MonoBehaviour {

	public float powerSpeed = 5f;
	public int playerId = -1;
	public bool isBallAssigned = false;
	public Ball ball;
	public GameManager gameManager;
	public PaddleController paddleController;

	// Use this for initialization
	void Start () {
		gameManager = FindObjectOfType<GameManager> ();
		paddleController = FindObjectOfType<PaddleController> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (isBallAssigned && gameManager.gameState == GameManager.GameStates.READY) {
			if (waitToFire ()) {
				isBallAssigned = false;
				gameManager.gameState = GameManager.GameStates.STARTED;
			}
		}
	}

	public void assignBall(){
		isBallAssigned = true;
	}

	bool waitToFire ()
	{
		Vector2 touchPoint = paddleController.GetTouchPoint ();
		if (touchPoint.x != 0  && touchPoint.y != 0) {
			ball = getBall ();
			ball = FindObjectOfType<Ball> ();
			ball.CmdFire (touchPoint);
			return true;
		}

		return false;
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Ball") {
			ball = getBall ();
			ball.CmdChangeSpeed (powerSpeed);
			ball.CmdChangeLastTouch (playerId);
		} 
	}

	Ball getBall () {
		if (ball == null) {
			ball = FindObjectOfType<Ball> ();
		}

		return ball;
	}
}
