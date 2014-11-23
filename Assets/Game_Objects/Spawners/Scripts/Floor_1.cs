using UnityEngine;
using System.Collections;
using Prime31.GoKitLite;

public class Floor_1 : MonoBehaviour {
	
	public float spawnMin = 1f;
	public float spawnMax = 2f;
	
	// Use this for initialization
	void Start () {

	}
	
	void Update(){
			Spawn();
	}
	
	
	void Spawn()
	{

			Invoke("Spawn", Random.Range(spawnMin, spawnMax));

	}
}
