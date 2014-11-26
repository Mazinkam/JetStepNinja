using UnityEngine;
using System.Collections;

public class BottomFloorTileSpawner : MonoBehaviour {

	public GameObject[] obj;

	// Use this for initialization
	void Awake () {
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
		float temp  = ((MeshRenderer)secondTile.GetComponents<MeshRenderer>()[0]).bounds.size.x;
		secondTile.transform.position = new Vector3(temp,-1f,-15f);

	}	
}
