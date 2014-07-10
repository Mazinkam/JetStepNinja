using UnityEngine;
using System.Collections;

public class GameMapGUI : MonoBehaviour {
	public GUISkin skin;
	
	private float aspect;
	private float bottomBarHeight;

	public Vector2 buttonSize;

	public float bottomBarOffset;

	public Vector2 shopButtonOffset;
	public Vector2 settingsButtonOffset;
	
	void Start()
	{
		aspect = 1024f/128f;
		bottomBarHeight = Screen.width/aspect;
	}

	void OnGUI()
	{
		GUI.skin = skin;
		GUILayout.Window(1, new Rect(0, Screen.height-bottomBarHeight-bottomBarOffset, Screen.width, bottomBarHeight), BottomBar, "", "BottomBar");

		GUI.Button(new Rect(Screen.width-buttonSize.x-shopButtonOffset.x, shopButtonOffset.y, buttonSize.x,buttonSize.y), "", "ShopButton");
		GUI.Button(new Rect(Screen.width-buttonSize.x-settingsButtonOffset.x, settingsButtonOffset.y, buttonSize.x,buttonSize.y), "", "SettingsButton");
	}

	void BottomBar(int windowID)
	{
		GUILayout.BeginVertical();
		
		GUILayout.EndVertical();
	}
}
