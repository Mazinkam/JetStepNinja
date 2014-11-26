using UnityEngine;
using System.Collections;

public class Floor_2 : MonoBehaviour {

	public GameObject[] obj;
	public float spawnMin = 1f;
	public float spawnMax = 2f;
	
	public int limitFloor_2;
	public static int countFloor_2 = 0;
	
	
	// Use this for initialization
	void Start () {
		Spawn();
	}
	
	void Update(){
		//	Spawn();
	}
	
	
	void Spawn()
	{


			var newObj = TrashMan.spawn( obj[Random.Range(0, obj.Length)], transform.position, Quaternion.Euler(0,90,0));
			TrashMan.despawnAfterDelay( newObj, 3 );


			Invoke("Spawn", Random.Range(spawnMin, spawnMax));

	}
}
