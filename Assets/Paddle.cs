using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Paddle : MonoBehaviour {

	public float powerSpeed = 5f;
	public bool isBallAssigned = false;
	public Ball ball;
//	public GameManager gameManager;
	public PaddleController paddleController;

	private float paddleHeight;
	private float ballHeight;

	private Text text;
	// Use this for initialization
	void Start () {
//		gameManager = FindObjectOfType<GameManager> ();
		paddleController = FindObjectOfType<PaddleController> ();

		Collider2D collider2D = gameObject.GetComponent<Collider2D>();
		paddleHeight = collider2D.bounds.size.y;

		text =  FindObjectOfType<Text>();
		text.text = "";
	}
	
	// Update is called once per frame
	void Update () {
		if (isBallAssigned && GameManager.gameState == GameManager.GameStates.READY) {
			if (waitToFire ()) {
				isBallAssigned = false;
				GameManager.gameState = GameManager.GameStates.STARTED;
			}
		}
	}

	public void assignBall(){
		isBallAssigned = true;
	}

	bool waitToFire ()
	{
		Vector2 touchPoint = paddleController.GetTouchPoint ();
		ball = getBall ();

		if (touchPoint.x != 0 && touchPoint.y != 0) {
			ball.CmdFire (touchPoint);
			return true;
		} else {
			Vector2 paddlePos = this.GetComponent<Rigidbody2D> ().position;

			ball.CmdSetPosition (new Vector2(paddlePos.x, paddlePos.y + (paddleHeight/2 + ballHeight/2)));
			return false;
		}
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Ball") {
			ball = getBall ();
			ball.CmdChangeSpeed (powerSpeed);
			ball.CmdChangeLastTouch (paddleController.playerId);
		} 
	}

	Ball getBall () {
		if (ball == null) {
			ball = FindObjectOfType<Ball> ();
			Collider2D collider2D = ball.gameObject.GetComponent<Collider2D>();
			ballHeight = collider2D.bounds.size.y;
		}

		return ball;
	}

	public void win ()
	{
		text.text = "Winner!";
	}

	public void lose ()
	{
		text.text = "Loser!";
	}


}
