using UnityEngine;
using System.Collections;

public class QuitButtonScript : MonoBehaviour
{
	void OnMouseDown()
	{
		Application.Quit ();
	}
}
