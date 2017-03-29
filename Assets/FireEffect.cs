using System;
using UnityEngine;

public class FireEffect : ItemEffect
{
	private enum State 
	{
		PADDLE, BALL, DISABLE
	}

	public FireEffect(Paddle target) : base(target){
	}

	public Sprite paddleSprite;
	public Sprite ballSprite;
	private GameObject go;
	private State state;

	void Start()
	{
		go = new GameObject("FireEffect");
		SpriteRenderer renderer = go.AddComponent<SpriteRenderer>();
		renderer.sprite = paddleSprite;
		state = State.PADDLE;
	}

	public override void Update() 
	{
		switch (state) {
		case State.PADDLE:
			go.transform.position =	base.targetPaddle.transform.position;
			base.targetPaddle.paddleClient.tmpPowerSpeed = 30f;
			break;
		case State.BALL:
			base.targetPaddle.paddleClient.tmpPowerSpeed = 0f;
			break;
		case State.DISABLE:
			Destroy (go);
			break;
		}
	}

	public override void OnCollision (CollisionType colType) {
		switch (colType) {
		case CollisionType.BALL_TO_PADDLE:
			state = State.BALL;
			break;
		default:
			state = State.DISABLE;
			break;
		}
	}
}

