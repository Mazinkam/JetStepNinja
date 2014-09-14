using UnityEngine;
using System.Collections;

public class DestroyerScript : MonoBehaviour {

	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "BasicObstacle")
		{
			Debug.Log("BasicObstacle");
			Destroy(other.gameObject);
		}
		if( other.tag == "Boat"){

			Debug.Log("boat");
			other.transform.position = new Vector3(Random.Range(500,3000),other.transform.position.y, other.transform.position .z);
		}
	}
}
