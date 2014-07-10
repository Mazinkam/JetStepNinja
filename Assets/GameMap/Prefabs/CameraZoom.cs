using UnityEngine;
using System.Collections;

public class CameraZoom : MonoBehaviour {

	public int zoom = 20;
	public int  normal = 60;
	public float smooth = 5;
	private bool isZoomed = false;

	void Update () {
		if(Input.GetKeyDown("z")){
			isZoomed = !isZoomed;
		}
		
		if(isZoomed == true){
			camera.fieldOfView = Mathf.Lerp(camera.fieldOfView,zoom,Time.deltaTime*smooth);
		}
		else{
			camera.fieldOfView = Mathf.Lerp(camera.fieldOfView,normal,Time.deltaTime*smooth);
		}
	}
}
