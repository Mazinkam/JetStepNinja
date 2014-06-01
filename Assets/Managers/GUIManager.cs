using UnityEngine;

public class GUIManager : MonoBehaviour {
	
	private static GUIManager instance;
	
	public GUIText distanceText, gameOverText, instructionsText, highscoreText;
	public Texture2D vmLogo, gameHeader;
	private float highscoreNum;
	private static float currentScore;
	public GameObject highscore;
	private HSController hs;
	
	void Start () {
		instance = this;
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		highscoreNum = PlayerPrefs.GetInt("High Score");
		gameOverText.enabled = false;
		hs = highscore.GetComponent ("HSController") as HSController;
		
		
	}
	
	void Update () {
		if (Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0))
		{
			GameEventManager.TriggerGameStart();
		}
		if (Input.GetKey(KeyCode.Escape))
		{
			Application.Quit();
		}
	}
	
	private void GameStart () {
		gameOverText.enabled = false;
		instructionsText.enabled = false;
		highscoreText.enabled = false;
		hs.highscoreList.enabled = false;
		
		
		enabled = false;
	}
	
	void OnGUI()
	{
		GUI.DrawTexture(new Rect((Screen.width / 2) - (gameHeader.width/1.7f), Screen.height / 8, 319, 159), gameHeader);
		GUI.DrawTexture(new Rect(Screen.width * 0.05f, Screen.height *0.08f, 70, 109), vmLogo);
	}
	
	private void GameOver () {
		gameOverText.enabled = true;
		instructionsText.enabled = true;
		highscoreText.enabled = true;
		if (currentScore > highscoreNum)
			PlayerPrefs.SetInt("High Score", (int)currentScore);
		
		highscoreNum = PlayerPrefs.GetInt("High Score");
		
		enabled = true;
		SetScore();
		hs.GetHighscores();
	}
	
	public void SetScore()
	{
		instance.highscoreText.text = "Highscore: " + currentScore.ToString("f0");
		hs.SetHighscore ("markus", currentScore);
	}
	
	public static void SetDistance(float distance){
		instance.distanceText.text = "Distance: "  + distance.ToString("f0");
		currentScore = distance;
	}
}