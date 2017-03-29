using UnityEngine;
using System.Collections;

public class ItemButton : MonoBehaviour {

	private PaddleController paddleController;

	// Use this for initialization
	void Start () {
		paddleController = FindObjectOfType<PaddleController> ();
	}

	public void UseItem1() {
		paddleController.useItem1 ();
	}

	public void UseItem2() {
		paddleController.useItem2 ();
	}
}
