using UnityEngine;
using System.Collections;

public class TopFloorTileSpawner : MonoBehaviour {

	public GameObject[] obj;

	// Use this for initialization
	void Awake () {
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
		float temp  = ((MeshRenderer)secondTile.GetComponents<MeshRenderer>()[0]).bounds.size.x;

		secondTile.transform.position = new Vector3(temp,32f,-15f);

	}	
}
