﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Paddle : MonoBehaviour {

	public bool isBallAssigned = false;
	public Ball ball;
	public PaddleClient paddleClient;

	public ActivateItem item1;
	public ActivateItem item2;

	public List<ItemEffect> effects = new List<ItemEffect>(); 

	// 
	private float paddleHeight;
	private float ballHeight;

	// Use this for initialization
	void Start () {

		paddleClient = FindObjectOfType<PaddleClient> ();

		Collider2D collider2D = gameObject.GetComponent<Collider2D>();
		paddleHeight = collider2D.bounds.size.y;

	}
	
	// Update is called once per frame
	void Update () {
		if (isBallAssigned && GameManager.gameState == GameManager.GameStates.READY) {
			if (waitToFire ()) {
				isBallAssigned = false;
				GameManager.gameState = GameManager.GameStates.STARTED;
			}

			foreach (ItemEffect ie in effects) {
				ie.Update ();
			}
		}
	}

	public void assignBall(){
		isBallAssigned = true;
	}

	bool waitToFire ()
	{
		Vector2 touchPoint = GetTouchPoint ();
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

	Ball getBall () {
		if (ball == null) {
			ball = FindObjectOfType<Ball> ();
			Collider2D collider2D = ball.gameObject.GetComponent<Collider2D>();
			ballHeight = collider2D.bounds.size.y;
		}

		return ball;
	}
		
	private Vector2 GetTouchPoint() {
		if (Input.GetMouseButtonDown(0)) {

			Vector3 p = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			return new Vector2 (p.x, p.y);
		}

		return new Vector2(0,0);
	}
}
