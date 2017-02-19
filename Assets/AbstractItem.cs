using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public abstract class AbstractItem : NetworkBehaviour {

	void OnTriggerEnter2D(Collider2D coll) {

		// Only do collision when game started
		if (GameManager.gameState != GameManager.GameStates.STARTED) {
			return;
		}

		if (coll.gameObject.tag == "Ball") {
			GameManager gameManager = FindObjectOfType<GameManager> ();
			gameManager.assignItem (this.gameObject);
		} 
		Destroy(gameObject);
	}

	public abstract void onAction (Paddle lastTouchedPlayer, Paddle opponent, Ball ball);


}
