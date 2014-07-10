using UnityEngine;
using System.Collections;

public class TopFloorTileSpawner : MonoBehaviour {

	public GameObject[] obj;

	// Use this for initialization
	void Start () {
		SpawnFirstTile();
		SpawnSecondTile();
	}

	void SpawnFirstTile()
	{
		Instantiate(obj[0], transform.position, new Quaternion(0,0,360,0));
	}

	void SpawnSecondTile()
	{
		GameObject secondTile = (GameObject)Instantiate(obj[1], transform.position, new Quaternion(0,0,360,0));
		secondTile.transform.position = new Vector3(199,32f,-15.1f);

	}	
}
