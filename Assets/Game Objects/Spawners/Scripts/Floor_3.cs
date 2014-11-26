using UnityEngine;
using System.Collections;

public class Floor_3 : MonoBehaviour {
	
	public GameObject[] obj;
	public float spawnMin;
	public float spawnMax;
	
	// Use this for initialization
	void Start () {
		Spawn();
	}
	
	void Update(){

	}
	
	void Spawn()
	{
		//Instantiate(obj[Random.Range(0, obj.Length)], transform.position, Quaternion.identity);
		//Debug.Log("countFloor_3: " + countFloor_3);

		var newObj = TrashMan.spawn( obj[Random.Range(0, obj.Length)], transform.position, Quaternion.identity );
		TrashMan.despawnAfterDelay( newObj, 3 );


		Invoke("Spawn", Random.Range(spawnMin, spawnMax));

	}
}
