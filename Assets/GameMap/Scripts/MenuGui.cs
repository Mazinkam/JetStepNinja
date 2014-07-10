using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuGui : MonoBehaviour {
	int MAINWINDOWID = 0;
	int GAMEWINDOWID = 1;
	int STOREWINDOWID = 2;
	int LEADERBOARDWINDOWID = 3;

	public enum MenuState
	{
		NONE,
		MAIN,
		GAME,
		STORE,
		LEADERBOARD
	}

	private MenuState currentState = MenuState.NONE;
	private MenuState nextState = MenuState.MAIN;
	public GUISkin skin;
	private static int windowSize = Screen.width / 3;
	private static int windowPos = Screen.width - windowSize;
	private int currentX = windowPos;
	private int nextX = windowSize + Screen.width + 100;

	void Start()
	{
		GetComponent<HighScoreController>().UpdateHighscores();
	}

	int getPos(bool current)
	{
		return (current ? currentX : nextX);
	}

	void OnGUI()
	{
		if(currentState != nextState)
		{
			currentX += 4;
			nextX -= 4;

			if(nextX <= windowPos)
			{
				currentState = nextState;
				nextX = windowSize + Screen.width + 100;
				currentX = windowPos;
			}
		}

		GUI.skin = skin;
		if(currentState == MenuState.MAIN || nextState == MenuState.MAIN)
		{
			GUILayout.Window(MAINWINDOWID, new Rect(getPos(currentState == MenuState.MAIN),0,windowSize, Screen.height), MainMenu, "");
		}

		if(currentState == MenuState.GAME || nextState == MenuState.GAME)
		{
			GUILayout.Window(GAMEWINDOWID, new Rect(getPos(currentState == MenuState.GAME),0,windowSize, Screen.height), GameMenu, "");
		}

		if(currentState == MenuState.STORE || nextState == MenuState.STORE)
		{
			GUILayout.Window(STOREWINDOWID, new Rect(getPos(currentState == MenuState.STORE),0,windowSize, Screen.height), StoreMenu, "");
		}

		if(currentState == MenuState.LEADERBOARD || nextState == MenuState.LEADERBOARD)
		{
			GUILayout.Window(LEADERBOARDWINDOWID, new Rect(getPos(currentState == MenuState.LEADERBOARD),0,windowSize, Screen.height), LeaderBoardMenu, "");
		}
	}

	void MainMenu(int windowID)
	{
		GUILayout.BeginHorizontal();
		if(GUILayout.Button("Play", GUILayout.Height(50)))
		{
			nextState = MenuState.GAME;
		}
		GUILayout.EndHorizontal();
		
		GUILayout.BeginHorizontal();
		if(GUILayout.Button("Store", GUILayout.Height(50)))
		{
			nextState = MenuState.STORE;
		}
		GUILayout.EndHorizontal();
		
		GUILayout.BeginHorizontal();
		if(GUILayout.Button("Leaderboard", GUILayout.Height(50)))
		{
			nextState = MenuState.LEADERBOARD;
		}
		GUILayout.EndHorizontal();
		
		GUILayout.BeginHorizontal();
		if(GUILayout.Button("Quit", GUILayout.Height(50)))
		{
			Application.Quit();
		}
		GUILayout.EndHorizontal();
	}

	void GameMenu(int windowID)
	{
		GUILayout.FlexibleSpace();
		GUILayout.BeginHorizontal();
		if(GUILayout.Button("Back", GUILayout.Height(50)))
		{
			nextState = MenuState.MAIN;
		}
		GUILayout.EndHorizontal();
	}

	void StoreMenu(int windowID)
	{
		if(currentX > Screen.width)
			Application.LoadLevel("Store");

		GUILayout.FlexibleSpace();
		GUILayout.BeginHorizontal();
		if(GUILayout.Button("Back", GUILayout.Height(50)))
		{
			nextState = MenuState.MAIN;
		}
		GUILayout.EndHorizontal();
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
			nextState = MenuState.MAIN;
		}
		GUILayout.EndHorizontal();
	}
}
