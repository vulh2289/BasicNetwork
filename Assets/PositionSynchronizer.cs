using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using UnityEngine.UI;

public class PositionSynchronizer : NetworkBehaviour
{

	[SyncVar]
	private Vector3 syncPos;

	[SerializeField]
	Transform myTransform;
	private float lerpRate = 15;


	private Vector3 lastPos;
	private float threshold = 0f;

	public Text latencyText;
	public NetworkClient client;

	void Start()
	{
		latencyText = GameObject.Find("LatencyText").GetComponent<Text>();
		client = GameObject.Find("NetworkManager").GetComponent<CustomNetworkManager>().client;
	}

	void Update()
	{
		UpdateBase();
		LerpPosition();
	}

	void FixedUpdate()
	{
		TransmitPosition();
	}

	protected void UpdateBase()
	{

		if (isLocalPlayer)
		{
			var latency = client.GetRTT();
			latencyText.text = string.Format("latency is {0}", latency);
		}
	}

	void LerpPosition()
	{
		if (!isLocalPlayer)
		{
			Lerp();
		}
	}

	void Lerp()
	{
		myTransform.position = Vector3.Lerp(myTransform.position, syncPos, Time.deltaTime * lerpRate);

	}

	[Command]
	void CmdProvidePositionToServer(Vector3 pos)
	{
		syncPos = pos;
	}

	[ClientCallback]
	void TransmitPosition()
	{
		if (isLocalPlayer && Vector3.Distance(myTransform.position, lastPos) > threshold)
		{
			CmdProvidePositionToServer(myTransform.position);
			lastPos = myTransform.position;
		}
	}
}