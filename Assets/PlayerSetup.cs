using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Collections.Generic;

public class PlayerSetup : NetworkBehaviour {

	public static int count;

	void Start () {
		count++;

		if (isLocalPlayer) {
			GetComponent<PaddleController> ().enabled = true;
			if (count == 2) {
				Camera.main.transform.Rotate (new Vector3 (0, 0, 180));
			} 
		}
		Debug.Log(count);	

	}

	public override void OnStartLocalPlayer()
	{
		GetComponent<SpriteRenderer>().color = Color.blue;
	}

	[Command]
	public void CmdSetPosition(float x, float y) {
		transform.position = new Vector3 (x, y, 0);	
	}

}
