using UnityEngine;

public class GUIManager : MonoBehaviour {
	
	private static GUIManager instance;
	
	public GUIText boostsText, distanceText, gameOverText, instructionsText, runnerText;
	
	void Start () {
		instance = this;
		GameEventManager.GameStart += GameStart;
		GameEventManager.GameOver += GameOver;
		gameOverText.enabled = false;
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
		runnerText.enabled = false;
		enabled = false;
	}
	
	private void GameOver () {
		gameOverText.enabled = true;
		instructionsText.enabled = true;
		enabled = true;
	}
	
	public static void SetSpeed(float boosts){
        instance.boostsText.text = "Speed: " + boosts.ToString("f0");
	}

	public static void SetDistance(float distance){
		instance.distanceText.text = "Points: "  + distance.ToString("f0");
	}
}