using UnityEngine;
using System.Collections;

public class StoreGui : MonoBehaviour
{
	private int STOREWINDOWID = 0;
	private int SELECTEDWINDOWID = 1;
	public GUISkin skin;

	void OnGUI()
	{
		GUI.skin = skin;
		GUILayout.Window(STOREWINDOWID, new Rect(Screen.width/3, 0, Screen.width - Screen.width/3, Screen.height), storeWindow, "");
		GUILayout.Window(SELECTEDWINDOWID, new Rect(0, Screen.height-50, Screen.width/3, 50), SelectedWindow, "");
	}

	void storeWindow(int windowID)
	{
		GUILayout.BeginVertical();
		// Top
		GUILayout.BeginHorizontal();
		GUILayout.Label("Costume");
		GUILayout.FlexibleSpace();
		if(GUILayout.Button("Back"))
		{
			Application.LoadLevel("Menu");
		}
		GUILayout.EndHorizontal();

		// Currency
		GUILayout.BeginHorizontal();
		GUILayout.Label("H:");
		GUILayout.Label("247");
		GUILayout.Label("S:");
		GUILayout.Label("30");
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();

		// Cats
		GUILayout.BeginHorizontal();
		if(GUILayout.Button("All"))
		{}
		if(GUILayout.Button("Head"))
		{}
		if(GUILayout.Button("Body"))
		{}
		GUILayout.EndHorizontal();

		// List All Items
		GUILayout.BeginHorizontal();
		GUILayout.Label("Store Items");
		GUILayout.EndHorizontal();

		// Filters
		GUILayout.FlexibleSpace();
		GUILayout.BeginHorizontal();
		if(GUILayout.Button("All"))
		{}
		if(GUILayout.Button("Owned"))
		{}
		if(GUILayout.Button("Unbought"))
		{}
		GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();
		GUILayout.EndVertical();
	}

	void SelectedWindow(int windowID)
	{
		GUILayout.BeginHorizontal();
		if(GUILayout.Button("1"))
		{}
		if(GUILayout.Button("2"))
		{}
		if(GUILayout.Button("3"))
		{}
		if(GUILayout.Button("4"))
		{}
		GUILayout.EndHorizontal();
	}
}
