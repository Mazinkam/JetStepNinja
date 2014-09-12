using UnityEngine;
using System.Collections;

public class Floor_3 : MonoBehaviour {
	
	public GameObject[] obj;
	public float spawnMin = 1f;
	public float spawnMax = 2f;
	
	// Use this for initialization
	void Start () {
		Spawn();
	}
	
	void Update(){

	}
	
	void Spawn()
	{
		Instantiate(obj[Random.Range(0, obj.Length)], transform.position, Quaternion.identity);
		//Debug.Log("countFloor_3: " + countFloor_3);
		Invoke("Spawn", Random.Range(spawnMin, spawnMax));

	}
}
