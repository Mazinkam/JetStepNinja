using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LeaderboardGuiScript : MonoBehaviour {
	int LEADERBOARDWINDOWID = 0;
	public GUISkin skin;

	void Start()
	{
		GetComponent<HighScoreController>().UpdateHighscores();
	}

	// Use this for initialization
	void OnGUI () {
		GUI.skin = skin;
		GUILayout.Window(LEADERBOARDWINDOWID, new Rect(0,0,Screen.width, Screen.height), LeaderBoardMenu, "");
	}
	
	Vector2 scrollpos = Vector2.zero;
	void LeaderBoardMenu(int windowID)
	{
		List<HighScore> scores = GetComponent<HighScoreController>().scores;
		scrollpos = GUILayout.BeginScrollView(scrollpos);
		GUILayout.BeginVertical();
		if(scores.Count > 0)
		{
			foreach(HighScore score in scores)
			{
				GUILayout.BeginHorizontal();
				GUILayout.Label(score.player, GUILayout.Height(25));
				GUILayout.FlexibleSpace();
				GUILayout.Label("" + score.score, GUILayout.Height(25));
				
				GUILayout.EndHorizontal();
			}
		}
		else
		{
			GUILayout.BeginHorizontal();
			GUILayout.Label("No Records", GUILayout.Height(25));
			GUILayout.EndHorizontal();
		}
		GUILayout.EndVertical();
		GUILayout.EndScrollView();
		GUILayout.FlexibleSpace();
		GUILayout.BeginHorizontal();
		if(GUILayout.Button("Back", GUILayout.Height(50)))
		{
			Application.LoadLevel("Menu");
		}
		GUILayout.EndHorizontal();
	}
}
