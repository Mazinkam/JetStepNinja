using UnityEngine;
using System.Collections;

public class Floor_1 : MonoBehaviour {

	public GameObject[] obj;
	public float spawnMin = 1f;
	public float spawnMax = 2f;
	
	public int limitFloor_1;
	public static int countFloor_1 = 0;
	
	
	// Use this for initialization
	void Start () {
		Spawn();
	}
	
	void Update(){
		//	Spawn();
	}
	
	
	void Spawn()
	{
		if(countFloor_1 <= limitFloor_1)
		{
			countFloor_1++;
			Instantiate(obj[Random.Range(0, obj.Length)], transform.position, Quaternion.identity);
			//Debug.Log("countFloor_1: " + countFloor_1);
			Invoke("Spawn", Random.Range(spawnMin, spawnMax));
		}
	}
}
