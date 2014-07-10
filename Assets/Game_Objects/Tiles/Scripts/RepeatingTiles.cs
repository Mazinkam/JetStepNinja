using UnityEngine;
using System.Collections;

public class RepeatingTiles : MonoBehaviour {

	public float movementSpeed;
	private MeshRenderer meshRenderer;
	// Use this for initialization
	void Start () {
		 meshRenderer = GetComponent<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 localPos = transform.localPosition;

		if( localPos.x <= -meshRenderer.bounds.size.x)
		{
			localPos.x += meshRenderer.bounds.size.x *2;
			localPos.x -= movementSpeed * Time.deltaTime;
		}
		else{
			localPos.x -= movementSpeed * Time.deltaTime;
		}
		transform.localPosition = localPos;
	}
}
