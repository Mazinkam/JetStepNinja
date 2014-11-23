using UnityEngine;
using System.Collections;

public class HomingObstacle : MonoBehaviour {

	public float homingSpeed;
	private Transform target;

	// Use this for initialization
	void Start () {
		target = UmeController.UmeTransform;
	}
	
	// Update is called once per frame
	void Update () {
			
		transform.position = Vector3.MoveTowards(transform.position, target.position, homingSpeed*Time.deltaTime);
	}
}
