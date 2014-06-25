using UnityEngine;
using System.Collections;

public class FloorTileSpawner : MonoBehaviour {

	public GameObject[] obj;

	// Use this for initialization
	void Start () {
		SpawnFirstTile();
		SpawnSecondTile();
	}

	void SpawnFirstTile()
	{
		Instantiate(obj[0], transform.position, Quaternion.identity);
	}

	void SpawnSecondTile()
	{
		GameObject secondTile = (GameObject)Instantiate(obj[1], transform.position, Quaternion.identity);
		secondTile.transform.position = new Vector3(199,-1f,-15.1f);
	}	
}
