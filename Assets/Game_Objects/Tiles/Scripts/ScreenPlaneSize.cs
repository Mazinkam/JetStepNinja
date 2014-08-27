using UnityEngine;
using System.Collections;

public class ScreenPlaneSize : MonoBehaviour {

	private float height, width;

	// Use this for initialization
	void Start () {
	
		float height = 2.0f * Mathf.Tan(0.5f * Camera.main.fieldOfView * Mathf.Deg2Rad) * 15 ;
		float width = height * Screen.width / Screen.height;
		
		
		
		gameObject.transform.localScale = new Vector3( width, 1, height);
		
		Debug.Log("width: " + width + " height: " + height );
		Debug.Log("x: " + gameObject.transform.localScale.x + " y: " + gameObject.transform.localScale.y + " z:" + gameObject.transform.localScale.y);

	}
	
	// Update is called once per frame
	void Update () {

	}
}
