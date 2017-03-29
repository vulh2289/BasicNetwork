using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public abstract class ItemEffect : NetworkBehaviour
{
	protected Paddle targetPaddle;

	public ItemEffect(Paddle targetPaddle) {
		this.targetPaddle = targetPaddle;
	}

	// Method to be overriden by concrete classes
	public abstract void Update();

	public abstract void OnCollision (CollisionType colType);
}

