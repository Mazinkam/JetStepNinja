using UnityEngine;
using System.Collections;

public class WorldMapMove : MonoBehaviour {
	
	private float distance;
	private Vector3 lastRayPoint;
	private Vector3 planeBotLeft;
	private Vector3 planeTopRight;

	void Start()
	{
		planeBotLeft = transform.position;
		planeTopRight = transform.position;

		Vector3 posBotLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
		Vector3 posTopRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
		
		Vector3 botLeftChild = transform.FindChild("BotLeft").transform.position;
		Vector3 topRightChild = transform.FindChild("TopRight").transform.position;

		planeBotLeft.x += posBotLeft.x - botLeftChild.x ;
		planeBotLeft.z += posBotLeft.z - botLeftChild.z ;

		planeTopRight.x += posTopRight.x - topRightChild.x ;
		planeTopRight.z += posTopRight.z - topRightChild.z ;
	}
	
	void OnMouseDown()
	{
		distance = Vector3.Distance(transform.position, Camera.main.transform.position);
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		lastRayPoint = ray.GetPoint(distance);
	}
	
	void OnMouseDrag()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		// Current object position
		Vector3 currentPosition = transform.position;
		// Get current mouse position
		Vector3 rayPoint = ray.GetPoint(distance);
		// Calc delta
		Vector3 delta = rayPoint - lastRayPoint;
		// calc new position


		Vector3 posBotLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
		Vector3 posTopRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
		
		Vector3 botLeftChild = transform.FindChild("BotLeft").transform.position;
		Vector3 topRightChild = transform.FindChild("TopRight").transform.position;

		lastRayPoint = rayPoint;

		if(posBotLeft.x > botLeftChild.x)
		{
			//currentPosition.x = planeBotLeft.x;
			currentPosition.x += delta.x;

		}

		if(posBotLeft.z > botLeftChild.z)
		{
		//	currentPosition.z = planeBotLeft.z ;
		//	currentPosition.z += delta.z;
		}
		
		if(posTopRight.x < topRightChild.x   )
		{
		//	currentPosition.x = planeTopRight.x ;
			currentPosition.x += delta.x;
		}
		
		if(posTopRight.z < topRightChild.z   )
		{
		//	currentPosition.z = planeTopRight.z ;
		//	currentPosition.z += delta.z;
		}

		transform.position = currentPosition;
	}

}
