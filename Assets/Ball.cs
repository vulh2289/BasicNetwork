using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	private int lastTouchPlayerId = -1;
	

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player1Goal") {
			print ("hit player 1 goal");
		} else if (coll.gameObject.tag == "Player2Goal") {
			print ("hit player 2 goal");
		}
	}
}
