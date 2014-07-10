using UnityEngine;
using System.Collections;

public class PulsingMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	//the number of pulses per second
	float rate = .5f;
	float maxMove = 8f;
	float minMove = 2f;
	
	float movement = (Mathf.Sin(Time.time * (rate * 2 * Mathf.PI)) + 1f)/2f;
	
	movement = Mathf.Lerp (minMove, maxMove, movement);
	
		transform.localPosition = new Vector3(transform.localPosition.x,movement,transform.localPosition.z);
	
	}
}
