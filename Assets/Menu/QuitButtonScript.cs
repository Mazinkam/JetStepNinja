using UnityEngine;
using System.Collections;

public class QuitButtonScript : MonoBehaviour
{
	void OnMouseDown()
	{
		RunnerMainMenu.jump = true;
		Application.Quit ();
	}
}
