using UnityEngine;
using System.Collections;

public class RotateTrigger : MonoBehaviour {

	Runner player;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("CuteMolly").GetComponent<Runner>();
	}

	void OnTriggerEnter(Collider obj)
	{
		// CuteMolly -> Bip001
		// Tag Player
		if(obj.gameObject.tag == "Player")
		{
			Debug.Log("Rotate Me");
			player.startRotateMe();
		}
	}
}
