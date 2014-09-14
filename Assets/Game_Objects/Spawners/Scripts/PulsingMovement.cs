using UnityEngine;
using System.Collections;


/*
 * Needs a version with different pulse movements to make it seem less robotic
 * 
 */
public class PulsingMovement : MonoBehaviour {

	public float rate;
	public float maxMove;
	public float minMove;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	//the number of pulses per second
	
	float movement = (Mathf.Sin(Time.time * (rate * 2 * Mathf.PI)) + 1f)/2f;
	
	movement = Mathf.Lerp (minMove, maxMove, movement);
	
	transform.localPosition = new Vector3(transform.localPosition.x,movement);
	
	}
}
