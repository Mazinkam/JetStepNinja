using UnityEngine;
using System.Collections;

public class ParalaxMovement : MonoBehaviour {

	public Material[] materials = new Material[0];	//the list of materials to pan
	public float[] speeds = new float[0];	//how fast each material will pan
	
	//initialize the tracking of the camera's position
	public void Start() {
	}
	
	//at the end of the frame
	public void FixedUpdate() {

		//loop through the list of materials
		for(int i=0;i<materials.Length;i++) {
			//find the materials current offset
			Vector2 offset = materials[i].mainTextureOffset;
			//adjust the offset based on the speed
			offset.x += 1 * speeds[i];
			//apply the new offset
			materials[i].mainTextureOffset = offset;
		}

	}
}
