using UnityEngine;
using System.Collections;

public class DestroyerScript : MonoBehaviour {

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "BasicObstacle")
		{
			Debug.Log("BasicObstacle");
			Destroy(other.gameObject);
			BasicObstacleSpawner.obstacleCount--;
		}
	}
}
