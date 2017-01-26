using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class SpeedItem : MonoBehaviour {

	public float speed = 1f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D coll) {

		// Only do collision when game started
		if (GameManager.gameState != GameManager.GameStates.STARTED) {
			return;
		}

		if (coll.gameObject.tag == "Ball") {
			GameManager gameManager = FindObjectOfType<GameManager> ();
			gameManager.assignItem (this.gameObject);
		} 
	}
}
