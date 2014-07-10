using UnityEngine;
using System.Collections;

public class PlayButtonScript : MonoBehaviour {

    public Texture texture;

	void OnMouseDown()
	{
		Application.LoadLevel ("water_run_beta");
	}

	// Use this for initialization
	void Start () {

        //renderer.material.mainTexture = texture;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
