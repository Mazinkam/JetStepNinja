using UnityEngine;
using System.Collections;

public class BasicObstacleSpawner : MonoBehaviour {

	public GameObject[] obj;
	public float spawnMin = 1f;
	public float spawnMax = 2f;
	
	// Use this for initialization
	void Start () {
		Spawn();
	}
	
	void Spawn()
	{
		Instantiate(obj[Random.Range(0, obj.Length)], transform.position, Quaternion.identity);
		Invoke("Spawn", Random.Range(spawnMin, spawnMax));
	}
}
