using UnityEngine;
using System.Collections;

public class WorldMapMove : MonoBehaviour {
	
	private float distance;
	private Vector3 lastRayPoint;
	private Vector3 planeBotLeft;
	private Vector3 planeTopRight;
	Vector3 posBotLeft, posTopRight;
	void Start()
	{
		planeBotLeft = transform.position;
		planeTopRight = transform.position;

		posBotLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, 0));
		posTopRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
		
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

		Vector3 botLeftChild = transform.FindChild("BotLeft").transform.position;
		Vector3 topRightChild = transform.FindChild("TopRight").transform.position;

		lastRayPoint = rayPoint;

		if(botLeftChild.x + delta.x < posBotLeft.x && delta.x > 0)
			currentPosition.x += delta.x;
		else if(topRightChild.x + delta.x > posTopRight.x && delta.x < 0)
			currentPosition.x += delta.x;

		if (botLeftChild.z + delta.z < posBotLeft.z && delta.z > 0)
			currentPosition.z += delta.z;
		else if (topRightChild.z + delta.z > posTopRight.z && delta.z < 0)
			currentPosition.z += delta.z;
//		if (delta.y != 0)
//		{
//			if(currentPosition.z + delta.z < posBotLeft.z)
//				currentPosition.z += delta.z;
//			else if(currentPosition.z - delta.z > posBotLeft.z)
//				currentPosition.z += delta.z;
//		}

		//currentPosition += delta;
		/*
		if(posBotLeft.x < botLeftChild.x)
		{
			currentPosition.x += posBotLeft.x - botLeftChild.x;
		//	currentPosition.x += delta.x;
		}

		if(posBotLeft.z < botLeftChild.z)
		{
			currentPosition.z += posBotLeft.z - botLeftChild.z ;
		//	currentPosition.z += delta.z;
		}

		if(posTopRight.x > topRightChild.x   )
		{
			currentPosition.x += posTopRight.x - topRightChild.x ;
		//	currentPosition.x += delta.x;
		}
		
		if(posTopRight.z > topRightChild.z   )
		{
			currentPosition.z += posTopRight.z - topRightChild.z ;
		//	currentPosition.z += delta.z;
		}*/


		transform.position = currentPosition;
	}

}
