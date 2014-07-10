using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Costume
{
	public Costume(string name)
	{
		this.name = name;
	}

	public Costume(string name, int price, Texture2D image)
	{
		this.name = name;
		this.price = price;
		this.image = image;
	}

	public string name { get; set; }
	public int price { get; set; }
	public Texture2D image { get; set; }
}

public class StoreGuiScript : MonoBehaviour
{
	private int STOREWINDOWID = 0;
	private int SELECTEDWINDOWID = 1;
	public GUISkin skin;
	private List<Costume> items = new List<Costume>();

	void Start()
	{
		// Init items
		for(int i = 0; i < 35; i++)
			items.Add (new Costume("" + i));
	}

	void OnGUI()
	{
		GUI.skin = skin;
		GUILayout.Window(STOREWINDOWID, new Rect(Screen.width/3, 0, Screen.width - Screen.width/3, Screen.height), storeWindow, "");
		GUILayout.Window(SELECTEDWINDOWID, new Rect(0, Screen.height-60, Screen.width/3, 50), SelectedWindow, "");
	}

	Vector2 scrollpos = Vector2.zero;
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
		if(GUILayout.Button("Back"))
		{}
		GUILayout.EndHorizontal();

		// List All Items
		GUILayout.BeginHorizontal();
		scrollpos = GUILayout.BeginScrollView(scrollpos, false, true);
		GUILayout.BeginHorizontal();
		if(items.Count > 0)
		{
			int counter = 1;
			int maxInline = ((Screen.width/3) - Screen.width) / 35 + 2;
			foreach(Costume item in items)
			{
				GUILayout.Button (item.name, GUI.skin.GetStyle("StoreItem"));

				if(counter % maxInline == 0)
				{
					GUILayout.EndHorizontal();
					GUILayout.BeginHorizontal();
				}
				counter++;
			}
		}
		else
		{
			GUILayout.Label("No Items", GUILayout.Height(25));
		}
		GUILayout.EndHorizontal();
		GUILayout.EndScrollView();
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
